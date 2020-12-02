using AutoMapper;
using BorrowIt.Common.Mongo.Models;
using BorrowIt.Common.Mongo.Repositories;
using BorrowIt.Notifications.Domain.Users;
using BorrowIt.Notifications.Entities;
using MongoDB.Driver;

namespace BorrowIt.Notifications.Repositories
{
    public class UserRepository : GenericMongoRepository<User, UserEntity>, IUserRepository
    {
        public UserRepository(IMongoClient mongoClient, IMapper mapper, MongoDbSettings mongoDbSettings) : base(mongoClient, mapper, mongoDbSettings)
        {
        }
    }
}
