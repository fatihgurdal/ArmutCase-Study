using Application.Common.Interfaces;

using Domain.Entities;
using Domain.Events;

using MediatR;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string UserName { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            entity.AddDomainEvent(new UserCreatedEvent(entity));

            await _context.Users.Add(entity);

            return entity.Id;
        }
    }
}
