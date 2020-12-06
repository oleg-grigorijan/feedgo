using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FeedLoader
{
    [ApiController]
    [Route("/feed")]
    public class FeedLoaderController : ControllerBase
    {
        private readonly IFeedLoader _feedLoader;
        private readonly ILogger<FeedLoaderController> _logger;

        public FeedLoaderController(ILogger<FeedLoaderController> logger, IFeedLoader feedLoader)
        {
            _logger = logger;
            _feedLoader = feedLoader;
        }

        [HttpPut]
        public IEnumerable<FeedItem> Load([FromBody] FeedRequest request)
        {
            return _feedLoader.Load(request);
        }
    }
}