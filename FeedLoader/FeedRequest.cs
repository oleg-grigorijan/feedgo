using System;
using System.Collections.Generic;
using System.Linq;

namespace FeedLoader
{
    public class FeedRequest
    {
        public Uri Uri { get; set; }
        public FeedFilter Filter { get; set; }
    }

    public struct FeedFilter
    {
        public IEnumerable<string> ContentKeyWords { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public DateTimeOffset? DateFrom { get; set; }
        public DateTimeOffset? DateTo { get; set; }

        public bool IsSatisfiedBy(FeedItem item)
        {
            return IsSatisfiedByContentKeyWords(item) &&
                   IsSatisfiedByCategories(item) &&
                   IsSatisfiedByDate(item);
        }

        private bool IsSatisfiedByContentKeyWords(FeedItem item)
        {
            return !ContentKeyWords.Any() ||
                   ContentKeyWords.All(keyWord => item.Title.Contains(keyWord) || item.Description.Contains(keyWord));
        }

        private bool IsSatisfiedByCategories(FeedItem item)
        {
            return !Categories.Any() ||
                   Categories.All(requiredCategory => item.Categories.Contains(requiredCategory));
        }

        private bool IsSatisfiedByDate(FeedItem item)
        {
            return (DateFrom == null || item.PublishedDate >= DateFrom) &&
                   (DateTo == null || item.PublishedDate <= DateTo);
        }
    }
}