using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.Extensions.Logging;

namespace FeedLoader
{
    public interface IFeedLoader
    {
        IEnumerable<FeedItem> Load(FeedRequest request);
    }

    public class FeedLoader : IFeedLoader
    {
        private readonly ILogger<FeedLoader> _logger;

        public FeedLoader(ILogger<FeedLoader> logger)
        {
            _logger = logger;
        }

        public IEnumerable<FeedItem> Load(FeedRequest request)
        {
            using var reader = XmlReader.Create(request.Uri.ToString());
            var feed = SyndicationFeed.Load(reader);
            return feed.Items
                .Select(item => item.ToFeedItem())
                .Where(item => request.Filter.IsSatisfiedBy(item));
        }
    }

    public static class SyndicationExtensions
    {
        public static FeedItem ToFeedItem(this SyndicationItem @this)
        {
            return new FeedItem
            {
                Title = @this.Title.Text,
                Description = @this.Summary.Text,
                Categories = @this.Categories.Select(c => c.Name),
                Link = @this.Links.First().Uri,
                PublishedDate = @this.PublishDate
            };
        }
    }
}