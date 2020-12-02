using System;
using System.Threading.Tasks;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.Notifications.Commands;
using BorrowIt.Notifications.Services;

namespace BorrowIt.Notifications.Handlers
{
    public class AddPushSubscriptionToUserCommandHandler : ICommandHandler<AddPushSubscriptionToUserCommand>
    {
        private readonly IUsersService _usersService;

        public AddPushSubscriptionToUserCommandHandler(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        public async Task HandleAsync(AddPushSubscriptionToUserCommand command)
        {
            await _usersService.AddPushSubscriptionToUser(command.UserId, command.PushSubscription);
        }
    }
}