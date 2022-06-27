using Application.Common.Interfaces;

using Domain.Entities;

using MediatR;

using MongoDB.Driver;

namespace Application.Users.Commands.SendMessage
{
    public record SendMessageCommand : IRequest
    {
        public string UserName { get; init; }
        public string Message { get; init; }
    }

    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public SendMessageCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Find(x => x.UserName == request.UserName)
                                    .FirstOrDefaultAsync() ?? throw new Exception("TODO: custom exception"); //Username kontrolü

            if (user.BlockUsers?.Any(x => x == _currentUserService.UserId) == true) //engel kontorlü
            {
                throw new Exception("TODO: custom exception");
            }
            var entity = new Message
            {
                Date = DateTime.UtcNow, //Client timezone bilgisine göre +/- eklemesi daha mantıklı
                Sender = _currentUserService.UserId,
                Receiver = user.Id,
                Text = request.Message,
            };

            await _context.Messages.InsertOneAsync(entity);

            return Unit.Value;
        }
    }
}
