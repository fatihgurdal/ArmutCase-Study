using Application.Common.Interfaces;

using Domain.Entities;
using Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

using MongoDB.Driver;

namespace Application.Users.Commands.EventHandlers
{
    public class BlockUserEventHandler : INotificationHandler<BlockUserEvent>
    {
        private readonly ILogger<BlockUserEventHandler> _logger;
        private readonly IApplicationDbContext _context;


        public BlockUserEventHandler(ILogger<BlockUserEventHandler> logger, IApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Handle(BlockUserEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
            if (notification.User.Activities == null) notification.User.Activities = new List<UserActivity>();

            var filterUser = Builders<User>.Filter.Eq("_id", notification.User.Id);
            var user = await _context.Users.Find(filterUser).FirstOrDefaultAsync();

            var description = notification.User.BlockUsers.Contains(notification.BlockedUserId) ?
                                    $"You blocked {user.FirstName} {user.LastName} ({user.UserName})"
                                    : $"You unblocked {user.FirstName} {user.LastName} ({user.UserName})"; //Fazlalık bir string alan olsun diye eklendi.

            notification.User.Activities.Add(new UserActivity()
            {
                IpAddress = string.Empty,
                ActivityDescription = description,
                Date = DateTime.UtcNow,
                Type = Domain.Enums.UserActivityType.UserBlock
            });

            var filter = Builders<User>.Filter.Eq("_id", notification.User.Id);
            await _context.Users.ReplaceOneAsync(filter, notification.User, cancellationToken: cancellationToken);
        }
    }
}