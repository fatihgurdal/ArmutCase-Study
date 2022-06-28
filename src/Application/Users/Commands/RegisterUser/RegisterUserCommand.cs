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
        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public RegisterUserCommandHandler(IApplicationDbContext context,IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password, //TODO: md5 ile hash yapılabilir. Login doğrularkende hashlenmiş hali kontrol edilebilir.
                BlockUsers = new List<Guid>()
            };

            await _mediator.Publish(new RegisterUserEvent(entity, "1.2.3.4"));

            await _context.Users.InsertOneAsync(entity, cancellationToken: cancellationToken);

            return entity.Id;
        }
    }
}
