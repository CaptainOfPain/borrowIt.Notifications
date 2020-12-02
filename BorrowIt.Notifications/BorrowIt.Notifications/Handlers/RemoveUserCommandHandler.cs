using System;
using System.Threading.Tasks;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.Notifications.Commands;
using BorrowIt.Notifications.Services;

namespace BorrowIt.Notifications.Handlers
{
    public class RemoveUserCommandHandler : ICommandHandler<RemoveUserCommand>
    {
        private readonly IUsersService _usersService;

        public RemoveUserCommandHandler(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        public async Task HandleAsync(RemoveUserCommand command)
        {
            await _usersService.RemoveUserAsync(command.Id);
        }
    }
}