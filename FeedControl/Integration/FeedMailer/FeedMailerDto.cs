using System.Collections.Generic;

namespace FeedControl.Integration.FeedMailer
{
    public class FeedMailingRequestDto
    {
        public IEnumerable<string> RecipientEmails { get; set; }
        public IEnumerable<FeedMailingItemDto> Items { get; set; }
    }

    public class FeedMailingItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}