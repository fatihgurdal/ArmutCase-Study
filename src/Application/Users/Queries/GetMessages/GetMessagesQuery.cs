using Application.Common.Interfaces;

using MediatR;

using MongoDB.Driver;

namespace Application.Users.Queries.GetMessages;

public record GetMessagesQuery : IRequest<IEnumerable<MessageVm>>
{
    public string UserName { get; init; }
}

public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IEnumerable<MessageVm>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public GetMessagesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<MessageVm>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserService.UserId;

        var user = await _context.Users.Find(x => x.UserName == request.UserName)
                                  .FirstOrDefaultAsync() ?? throw new Exception("TODO: custom exception not found username"); //Username kontrolü
        //TODO: kontrol etmek lazım bu sorgu performanssız ise veya kısımlarını iki ayrı sorgu ile çekilebilir. Benchmark yapmak lazım
        var messages = _context.Messages.Find(x => (x.Sender == currentUser && x.Receiver == user.Id) || (x.Sender == user.Id && x.Receiver == currentUser)).ToList().Select(x => new MessageVm()
        {
            Sender = x.Sender,
            Receiver = x.Receiver,
            Date = x.Date,
            Text = x.Text
        });

        return messages.OrderBy(x => x.Date);
    }

}
