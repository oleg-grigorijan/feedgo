using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace FeedControl.Integration.FeedMailer
{
    public interface IFeedMailerClient
    {
        Task Mail(FeedMailingRequestDto request);
    }

    public class FeedMailerClient : IFeedMailerClient
    {
        private readonly HttpClient _client;
        private readonly FeedMailerOptions _options;

        public FeedMailerClient(IOptions<FeedMailerOptions> options)
        {
            _options = options.Value;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_options.Api)
            };
        }

        public Task Mail(FeedMailingRequestDto request)
        {
            return _client.PostAsJsonAsync("/mailer", request);
        }
    }
}