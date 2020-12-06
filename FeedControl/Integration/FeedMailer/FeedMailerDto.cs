using System.Collections.Generic;
using FeedControl.Integration.FeedLoader;

namespace FeedControl.Integration.FeedMailer
{
    public class FeedMailingRequestDto
    {
        public IEnumerable<string> RecipientEmails { get; set; }
        public IEnumerable<FeedItemDto> Items { get; set; }
    }
}