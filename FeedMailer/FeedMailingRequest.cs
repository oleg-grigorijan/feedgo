using System;
using System.Collections;
using System.Collections.Generic;

namespace FeedMailer
{
    public class FeedMailingRequest
    {
        public IEnumerable<string> RecipientEmails { get; set; }
        public IEnumerable<FeedItem> Items { get; set; }
    }

    public class FeedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri Link { get; set; }
    }
}