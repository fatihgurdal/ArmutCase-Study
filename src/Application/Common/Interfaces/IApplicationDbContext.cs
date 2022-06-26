using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        IEntityCollection<User> Users { get; }
    }
}
