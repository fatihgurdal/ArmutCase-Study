using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.Entities;
using Domain.Events;

using MediatR;

using MongoDB.Driver;

namespace Application.Users.Commands.BlockUser
{
    public record BlockUserCommand : IRequest
    {
        public Guid UserId { get; init; }
        //public string UserName { get; init; } //TODO: case açıklamasında özellikle kullanıcı adını bildiği taktirde engelleyebilir demediği için direk id kullanmayı seçtim
        public bool BlockOn { get; init; }
    }
    public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public BlockUserCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMediator mediator)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq("_id", _currentUserService.UserId);

            var user = await _context.Users.Find(filter)
                                 .FirstOrDefaultAsync() ?? throw new NotFoundException("Not Found Current User");
            if (user.BlockUsers == null) user.BlockUsers = new List<Guid>();

            if (request.BlockOn && !user.BlockUsers.Contains(request.UserId))
            {
                user.BlockUsers.Add(request.UserId);
            }
            else if (!request.BlockOn && user.BlockUsers.Contains(request.UserId))
            {
                user.BlockUsers.Remove(request.UserId);
            }
            await _context.Users.ReplaceOneAsync(filter, user, cancellationToken: cancellationToken);
            await _mediator.Publish(new BlockUserEvent(user, request.UserId));
            return Unit.Value;
        }
    }
}