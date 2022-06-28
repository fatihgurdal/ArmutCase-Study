using Application.Common.Interfaces;

using Domain.Entities;
using Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

using MongoDB.Driver;

namespace Application.Users.Commands.EventHandlers
{
    public class UserLoginEventHandler : INotificationHandler<UserLoginEvent>
    {
        private readonly ILogger<UserLoginEventHandler> _logger;
        private readonly IApplicationDbContext _context;


        public UserLoginEventHandler(ILogger<UserLoginEventHandler> logger, IApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task Handle(UserLoginEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
            if (notification.User.Activities == null) notification.User.Activities = new List<UserActivity>();

            notification.User.Activities.Add(new UserActivity()
            {
                IpAddress = notification.IpAddress,
                ActivityDescription = $"Create token with login", //Fazlalık bir string alan olsun diye eklendi.
                Date = DateTime.UtcNow,
                Type = Domain.Enums.UserActivityType.Login
            });
            var filter = Builders<User>.Filter.Eq("_id", notification.User.Id);
            return _context.Users.ReplaceOneAsync(filter, notification.User, cancellationToken: cancellationToken);
        }
    }
}