using Application.Common.Interfaces;

using Domain.Entities;

using MediatR;

using MongoDB.Driver;

namespace Application.Users.Queries.UserActivities;

public record UserActivitiesQuery : IRequest<IEnumerable<UserActivityVm>>
{
}

public class UserActivitiesQueryHandler : IRequestHandler<UserActivitiesQuery, IEnumerable<UserActivityVm>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;


    public UserActivitiesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<UserActivityVm>> Handle(UserActivitiesQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<User>.Filter.Eq("_id", _currentUserService.UserId);

        var user = await _context.Users.Find(filter)
                             .FirstOrDefaultAsync() ?? throw new Exception("TODO: custom exception current user not found :D");

        return user.Activities?.Select(x => new UserActivityVm()
        {
            ActivityDescription = x.ActivityDescription,
            ActivityType = x.Type.ToString(), //Veya dirke enumda dönebilir 
            Date = x.Date,
            IpAddress = x.IpAddress,
        }) ?? new List<UserActivityVm>();

    }
}