using Domain.Entities;

using MongoDB.Driver;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        IMongoCollection<User> Users { get; }
    }
}
