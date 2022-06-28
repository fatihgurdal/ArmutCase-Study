using Application.Common.Interfaces;

using Domain.Entities;
using Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

using MongoDB.Driver;

namespace Application.Users.Commands.EventHandlers
{
    public class RegisterUserEventHandler : INotificationHandler<RegisterUserEvent>
    {
        private readonly ILogger<RegisterUserEventHandler> _logger;
        private readonly IApplicationDbContext _context;
        public RegisterUserEventHandler(ILogger<RegisterUserEventHandler> logger, IApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task Handle(RegisterUserEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
            if (notification.User.Activities == null) notification.User.Activities = new List<UserActivity>();

            notification.User.Activities.Add(new UserActivity()
            {
                IpAddress = notification.IpAddress,
                ActivityDescription = $"New account created", //Fazlalık bir string alan olsun diye eklendi.
                Date = DateTime.UtcNow,
                Type = Domain.Enums.UserActivityType.Register
            });
            var filter = Builders<User>.Filter.Eq("_id", notification.User.Id);
            return _context.Users.ReplaceOneAsync(filter, notification.User, cancellationToken: cancellationToken);
        }
    }
}