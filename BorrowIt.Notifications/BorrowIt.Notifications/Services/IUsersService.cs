using System;
using System.Threading.Tasks;
using BorrowIt.Common.Application.Services;
using BorrowIt.Notifications.Domain.Users;
using Lib.Net.Http.WebPush;

namespace BorrowIt.Notifications.Services
{
    public interface IUsersService : IService
    {
        Task AddUserAsync(UserDataStructure userDataStructure);
        Task UpdateUserAsync(UserDataStructure userDataStructure);
        Task RemoveUserAsync(Guid id);
        Task<bool> UserExists(Guid id);
        Task AddPushSubscriptionToUser(Guid userId, PushSubscription pushSubscription);
    }
}
