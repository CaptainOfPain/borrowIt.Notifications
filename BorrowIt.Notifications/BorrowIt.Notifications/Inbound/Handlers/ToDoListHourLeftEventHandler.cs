using System;
using System.Threading;
using System.Threading.Tasks;
using BorrowIt.Common.Rabbit.Abstractions;
using BorrowIt.Notifications.Inbound.Messages;
using BorrowIt.Notifications.Push;
using BorrowIt.Notifications.Services;

namespace BorrowIt.Notifications.Inbound.Handlers
{
    public class ToDoListHourLeftEventHandler : IMessageHandler<ToDoListHourLeftEvent>
    {
        private readonly INotificationsSenderService _notificationsSenderService;

        public ToDoListHourLeftEventHandler(INotificationsSenderService notificationsSenderService)
        {
            _notificationsSenderService = notificationsSenderService ?? throw new ArgumentNullException(nameof(notificationsSenderService));
        }
        public async Task HandleMessageAsync(ToDoListHourLeftEvent message, CancellationToken token)
        {
            var notification = new AngularPushNotification()
            {
                Title = @"Your list is about to end!",
                Body = $@"List: {message.ToDoListName}"
            };

            await _notificationsSenderService.SendMessage(message.UserId, notification);
        }
    }
}
