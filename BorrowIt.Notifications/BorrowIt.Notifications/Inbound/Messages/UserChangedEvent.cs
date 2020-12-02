using System;
using BorrowIt.Common.Rabbit.Abstractions;
using BorrowIt.Common.Rabbit.Attributes;

namespace BorrowIt.Notifications.Inbound.Messages
{
    [RabbitMessage("Auth")]
    public class UserChangedEvent : IMessage
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}