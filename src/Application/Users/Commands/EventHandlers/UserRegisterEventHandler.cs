using Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.Users.Commands.EventHandlers
{
    public class UserRegisterEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<UserRegisterEventHandler> _logger;
        public UserRegisterEventHandler(ILogger<UserRegisterEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
            //TODO: user create after custom business
            return Task.CompletedTask;
        }
    }
}