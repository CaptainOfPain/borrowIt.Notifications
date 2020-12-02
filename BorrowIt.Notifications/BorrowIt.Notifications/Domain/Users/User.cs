using System;
using System.Collections.Generic;
using System.Linq;
using BorrowIt.Common.Domain;
using BorrowIt.Common.Extensions;
using Lib.Net.Http.WebPush;

namespace BorrowIt.Notifications.Domain.Users
{
    public class User : DomainModel
    {
        private List<PushSubscription> _pushSubscriptions = new List<PushSubscription>();

        public string UserName { get; private set; }
        public IEnumerable<PushSubscription> PushSubscriptions
        {
            get => _pushSubscriptions;
            set => _pushSubscriptions = value.ToList();
        }

        public User(Guid id, string userName)
        {
            Id = id;
            SetUserName(userName);

        }

        public User()
        {

        }

        public void Update(string userName)
        {
            SetUserName(userName);
        }

        public void AddPushSubscription(PushSubscription pushSubscription)
        {
            if (pushSubscription == null)
            {
                throw new ArgumentNullException(nameof(pushSubscription));
            }

            _pushSubscriptions.Add(pushSubscription);
        }

        private void SetUserName(string userName)
        {
            userName.ValidateNullOrEmptyString();

            UserName = userName;
        }

    }
}
