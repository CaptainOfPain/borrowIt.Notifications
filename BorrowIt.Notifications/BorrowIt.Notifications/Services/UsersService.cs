using System;
using System.Threading.Tasks;
using BorrowIt.Common.Exceptions;
using BorrowIt.Notifications.Domain.Users;
using BorrowIt.Notifications.Repositories;
using Lib.Net.Http.WebPush;

namespace BorrowIt.Notifications.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;

        public UsersService(IUserRepository userRepository, IUserFactory userFactory)
        {
            _userRepository = userRepository;
            _userFactory = userFactory;
        }

        public async Task AddUserAsync(UserDataStructure userDataStructure)
        {
            var user = await _userRepository.GetAsync(userDataStructure.Id);
            if (user != null)
            {
                throw new BusinessLogicException("user_exists");
            }

            user = _userFactory.Create(userDataStructure);
            await _userRepository.CreateAsync(user);
        }

        public async Task UpdateUserAsync(UserDataStructure userDataStructure)
        {
            var user = await GetUserOrThrowAsync(userDataStructure.Id);

            user.Update(userDataStructure.UserName);
            await _userRepository.UpdateAsync(user);
        }

        public async Task RemoveUserAsync(Guid id)
        {
            var user = await GetUserOrThrowAsync(id);
            await _userRepository.RemoveAsync(user);
        }

        public async Task<bool> UserExists(Guid id)
            => await _userRepository.GetAsync(id) != null;

        public async Task AddPushSubscriptionToUser(Guid userId, PushSubscription pushSubscription)
        {
            var user = await GetUserOrThrowAsync(userId);
            user.AddPushSubscription(pushSubscription);

            await _userRepository.UpdateAsync(user);
        }

        private async Task<User> GetUserOrThrowAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new BusinessLogicException("user_not_found");
            }

            return user;
        }
    }
}