using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.Entities;

using MediatR;

using MongoDB.Driver;

namespace Application.Users.Queries.GetUser;

public record class GetUserQuery : IRequest<UserVm>
{
    public Guid UserId { get; set; }
}


public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserVm>
{
    private readonly IApplicationDbContext _context;


    public GetUserQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserVm> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<User>.Filter.Eq("_id", request.UserId);

        var user = await _context.Users.Find(filter)
                             .FirstOrDefaultAsync() ?? throw new NotFoundException("Not Found User");

        return new UserVm()
        {
            FirstName = user.FirstName,
            Id = user.Id,
            LastName = user.LastName,
            UserName = user.UserName,
        };
    }
}
