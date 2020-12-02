using BorrowIt.Common.Domain.Repositories;
using BorrowIt.Notifications.Domain.Users;
using BorrowIt.Notifications.Entities;

namespace BorrowIt.Notifications.Repositories
{
    public interface IUserRepository : IGenericRepository<User, UserEntity>, IRepository
    {

    }
}