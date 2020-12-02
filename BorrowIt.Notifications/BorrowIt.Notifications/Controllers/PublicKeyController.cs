using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BorrowIt.Notifications.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PublicKeyController : ControllerBase
    {
        private readonly PushNotificationsOptions _options;

        public PublicKeyController(IOptions<PushNotificationsOptions> options)
        {
            _options = options.Value;
        }

        public ContentResult Get()
        {
            return Content(_options.PublicKey, "text/plain");
        }

    }
}