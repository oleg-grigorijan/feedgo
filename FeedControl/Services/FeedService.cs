using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedControl.Integration.FeedLoader;
using FeedControl.Integration.FeedMailer;
using FeedControl.Models;

namespace FeedControl.Services
{
    public interface IFeedService
    {
        Task<IEnumerable<FeedItem>> LoadAsync(IEnumerable<string> urls, FeedFilter filter);

        Task MailAsync(IEnumerable<string> emails, IEnumerable<FeedItem> items);
    }

    public class FeedService : IFeedService
    {
        private readonly IFeedLoaderClient _loaderClient;
        private readonly IFeedMailerClient _mailerClient;

        public FeedService(IFeedLoaderClient loaderClient, IFeedMailerClient mailerClient)
        {
            _loaderClient = loaderClient;
            _mailerClient = mailerClient;
        }

        public async Task<IEnumerable<FeedItem>> LoadAsync(IEnumerable<string> urls, FeedFilter filter)
        {
            var filterDto = filter.ToDto();

            var tasks = new List<Task<IEnumerable<FeedItemDto>>>();
            foreach (var url in urls)
                tasks.Add(_loaderClient.LoadAsync(new FeedRequestDto
                {
                    Uri = url,
                    Filter = filterDto
                }));

            return (await Task.WhenAll(tasks))
                .SelectMany(it => it)
                .Select(dto => dto.ToModel())
                .OrderByDescending(item => item.PublishedDate);
        }

        public Task MailAsync(IEnumerable<string> emails, IEnumerable<FeedItem> items)
        {
            return _mailerClient.Mail(new FeedMailingRequestDto
            {
                RecipientEmails = emails,
                Items = items.Select(item => item.ToDto())
            });
        }
    }

    public static class Extensions
    {
        public static FeedFilterDto ToDto(this FeedFilter @this)
        {
            return new FeedFilterDto
            {
                Categories = @this.Categories,
                ContentKeyWords = @this.ContentKeyWords,
                DateFrom = @this.DateFrom,
                DateTo = @this.DateTo
            };
        }

        public static FeedMailingItemDto ToDto(this FeedItem @this)
        {
            return new FeedMailingItemDto
            {
                Title = @this.Title,
                Description = @this.Description,
                Link = @this.Link.ToString()
            };
        }

        public static FeedItem ToModel(this FeedItemDto @this)
        {
            return new FeedItem
            {
                Title = @this.Title,
                Description = @this.Description,
                Categories = @this.Categories,
                Link = @this.Link,
                PublishedDate = @this.PublishedDate
            };
        }
    }
}