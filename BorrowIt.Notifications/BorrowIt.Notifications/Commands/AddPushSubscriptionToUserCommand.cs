using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;
using Lib.Net.Http.WebPush;

namespace BorrowIt.Notifications.Commands
{
    public class AddPushSubscriptionToUserCommand : ICommand
    {
        public Guid UserId { get; }
        public PushSubscription PushSubscription { get; }

        public AddPushSubscriptionToUserCommand(Guid userId, PushSubscription pushSubscription)
        {
            UserId = userId;
            PushSubscription = pushSubscription;
        }
    }
}