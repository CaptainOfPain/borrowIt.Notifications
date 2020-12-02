using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.Notifications.Commands
{
    public class RemoveUserCommand : ICommand
    {
        public Guid Id { get; }

        public RemoveUserCommand(Guid id)
        {
            Id = id;
        }
    }
}