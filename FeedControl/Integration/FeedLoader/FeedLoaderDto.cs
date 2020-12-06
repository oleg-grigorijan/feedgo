using System;
using System.Collections.Generic;

namespace FeedControl.Integration.FeedLoader
{
    public class FeedItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public DateTimeOffset PublishedDate { get; set; }
        public Uri Link { get; set; }
    }

    public class FeedRequestDto
    {
        public string Uri { get; set; }
        public FeedFilterDto Filter { get; set; }
    }
}