using System;
using BorrowIt.Common.Rabbit.Abstractions;
using BorrowIt.Common.Rabbit.Attributes;

namespace BorrowIt.Notifications.Inbound.Messages
{
    [RabbitMessage("ToDo")]
    public class ToDoListHourLeftEvent : IMessage
    {
        public Guid ToDoListId { get; set; }
        public string ToDoListName { get; set; }
        public Guid UserId { get; set; }
    }
}
