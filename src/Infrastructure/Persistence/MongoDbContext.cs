using Application.Common.Interfaces;

using Domain.Entities;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

namespace Infrastructure.Persistence
{
    public class MongoDbContext : IApplicationDbContext
    {
        public MongoDbContext(IOptions<MongoOptions> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);

            this.Users = mongoDatabase.GetCollection<User>(nameof(User));
            this.Messages = mongoDatabase.GetCollection<Message>(nameof(Message));
        }
        public IMongoCollection<User> Users { get; init; }
        public IMongoCollection<Message> Messages { get; init; }
    }
}
