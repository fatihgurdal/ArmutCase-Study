using Application.Common.Interfaces;

using Domain.Entities;
using Domain.Events;

using MediatR;

namespace Application.Users.Commands.RegisterUser
{
    public record RegisterUserCommand : IRequest<Guid>
    {
        public string UserName { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public RegisterUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            entity.AddDomainEvent(new UserCreatedEvent(entity));

            await _context.Users.InsertOneAsync(entity);

            return entity.Id;
        }
    }
}
