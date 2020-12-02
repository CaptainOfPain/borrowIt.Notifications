using System;
using System.Threading;
using System.Threading.Tasks;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.Common.Rabbit.Abstractions;
using BorrowIt.Notifications.Commands;
using BorrowIt.Notifications.Inbound.Messages;

namespace BorrowIt.Notifications.Inbound.Handlers
{
    public class UserRemovedEventHandler : IMessageHandler<UserRemovedEvent>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public UserRemovedEventHandler(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
        }

        public async Task HandleMessageAsync(UserRemovedEvent message, CancellationToken token)
        {
            await _commandDispatcher.DispatchAsync(new RemoveUserCommand(message.Id));
        }
    }
}