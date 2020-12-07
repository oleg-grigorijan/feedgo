using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace FeedControl.Models
{
    public struct FeedFilter
    {
        public IEnumerable<string> ContentKeyWords { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public DateTimeOffset? DateFrom { get; set; }
        public DateTimeOffset? DateTo { get; set; }

        public static FeedFilter Empty()
        {
            return new FeedFilter
            {
                Categories = ImmutableArray<string>.Empty,
                ContentKeyWords = ImmutableArray<string>.Empty,
                DateFrom = null,
                DateTo = null
            };
        }
    }
}