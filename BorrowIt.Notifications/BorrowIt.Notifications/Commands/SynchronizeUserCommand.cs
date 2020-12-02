using System;
using System.Collections.Generic;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.Notifications.Commands
{
    public class SynchronizeUserCommand : ICommand
    {
        public Guid Id { get; }
        public string UserName { get; }

        public SynchronizeUserCommand(Guid id, string userName)
        {
            Id = id;
            UserName = userName;
        }
    }
}
