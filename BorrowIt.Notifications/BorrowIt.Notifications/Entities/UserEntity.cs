using System.Collections.Generic;
using BorrowIt.Common.Mongo.Attributes;
using BorrowIt.Common.Mongo.Models;
using Lib.Net.Http.WebPush;

namespace BorrowIt.Notifications.Entities
{

    [MongoEntity("Users")]
    public class UserEntity : MongoEntity
    {
        public string UserName { get; set; }
        public List<PushSubscription> PushSubscriptions { get; set; }
    }
}
