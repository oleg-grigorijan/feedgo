using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace FeedControl.Integration.FeedLoader
{
    public struct FeedFilterDto
    {
        public IEnumerable<string> ContentKeyWords { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public DateTimeOffset? DateFrom { get; set; }
        public DateTimeOffset? DateTo { get; set; }
    }

    public interface IFeedLoaderClient
    {
        Task<IEnumerable<FeedItemDto>> LoadAsync(FeedRequestDto request);
    }

    public class FeedLoaderClient : IFeedLoaderClient
    {
        private readonly HttpClient _client;
        private readonly FeedLoaderOptions _options;

        public FeedLoaderClient(IOptions<FeedLoaderOptions> options)
        {
            _options = options.Value;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_options.Api)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
        }

        public async Task<IEnumerable<FeedItemDto>> LoadAsync(FeedRequestDto request)
        {
            var resp = await _client.PutAsJsonAsync("/feed", request);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<IEnumerable<FeedItemDto>>();
        }
    }
}