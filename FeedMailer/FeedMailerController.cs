using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FeedMailer
{
    [ApiController]
    [Route("/mailer")]
    public class FeedMailerController : ControllerBase
    {
        private readonly ILogger<FeedMailerController> _logger;
        private readonly IFeedMailer _mailer;

        public FeedMailerController(ILogger<FeedMailerController> logger, IFeedMailer mailer)
        {
            _logger = logger;
            _mailer = mailer;
        }

        [HttpPost]
        public void Mail([FromBody] FeedMailingRequest request)
        {
            _mailer.Mail(request);
        }
    }
}