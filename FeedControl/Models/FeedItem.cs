﻿using System;
using System.Collections.Generic;

namespace FeedControl.Models
{
    public class FeedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public DateTimeOffset PublishedDate { get; set; }
        public Uri Link { get; set; }
    }
}