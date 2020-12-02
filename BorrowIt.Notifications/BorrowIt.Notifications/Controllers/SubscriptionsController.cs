using System;
using System.Threading.Tasks;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.Notifications.Commands;
using Lib.Net.Http.WebPush;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BorrowIt.Notifications.Controllers
{
    [Route("api/[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SubscriptionsController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
        }

        [Authorize]
        [Route("AddSubscription")]
        public async Task<IActionResult> AddSubscription([FromBody] PushSubscription pushSubscription)
        {
            var userId = new Guid(User.Identity.Name);
            await _commandDispatcher.DispatchAsync(new AddPushSubscriptionToUserCommand(userId, pushSubscription));

            return Ok();
        }
    }
}
