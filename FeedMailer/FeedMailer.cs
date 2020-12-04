using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FeedMailer
{
    public interface IFeedMailer
    {
        void Mail(FeedMailingRequest request);
    }

    public class FeedMailer : IFeedMailer
    {
        private readonly ILogger<FeedMailer> _logger;
        private readonly EmailSenderOptions _senderOptions;
        private readonly SmtpClient _client;

        public FeedMailer(ILogger<FeedMailer> logger, IOptions<EmailSenderOptions> senderOptions)
        {
            _logger = logger;
            _senderOptions = senderOptions.Value;
            _client = new SmtpClient
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_senderOptions.Address, _senderOptions.Password),
                Host = _senderOptions.SmtpHost,
                Port = _senderOptions.SmtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
        }

        public void Mail(FeedMailingRequest request)
        {
            var body = ToBody(request.Items);
            foreach (var email in request.RecipientEmails)
            {
                var message = new MailMessage
                {
                    From = new MailAddress(_senderOptions.Address),
                    To = {email},
                    Subject = "FeedGO",
                    IsBodyHtml = true,
                    Body = body,
                };
                _client.Send(message);
            }
        }

        private string ToBody(IEnumerable<FeedItem> items)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in items)
            {
                sb
                    .Append("<h3><a href=\"").Append(item.Link).Append("\">")
                    .Append(WebUtility.HtmlDecode(item.Title))
                    .Append("</a></h3><p>")
                    .Append(WebUtility.HtmlDecode(item.Description)).Append("</p>");
            }

            return sb.ToString();
        }
    }
}