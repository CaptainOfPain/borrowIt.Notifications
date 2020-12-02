using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BorrowIt.Common.Exceptions;
using BorrowIt.Notifications.Domain.Users;
using BorrowIt.Notifications.Push;
using BorrowIt.Notifications.Repositories;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Microsoft.Extensions.Options;

namespace BorrowIt.Notifications.Services
{
    public interface INotificationsSenderService
    {
        Task SendMessage(Guid userId, AngularPushNotification message);
    }

    public class NotificationsSender : INotificationsSenderService
    {
        private readonly PushServiceClient _pushClient;
        private readonly IUserRepository _userRepository;

        public NotificationsSender(IOptions<PushNotificationsOptions> options, PushServiceClient pushClient, IUserRepository userRepository)
        {
            _pushClient = pushClient ?? throw new ArgumentNullException(nameof(pushClient));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _pushClient.DefaultAuthentication = new VapidAuthentication(options.Value.PublicKey, options.Value.PrivateKey);
            _pushClient.DefaultAuthentication.Subject = options.Value.Subject;
        }
        public async Task SendMessage(Guid userId, AngularPushNotification message)
        {
            var user = await GetUserOrThrow(userId);

            foreach (var userPushSubscription in user.PushSubscriptions)
            {
                await _pushClient.RequestPushMessageDeliveryAsync(userPushSubscription, message.ToPushMessage());
            }
        }

        private async Task<User> GetUserOrThrow(Guid id)
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
