using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FeedLoader
{
    [ApiController]
    [Route("/api/feed")]
    public class FeedLoaderController : ControllerBase
    {
        private readonly ILogger<FeedLoaderController> _logger;
        private readonly IFeedLoader _feedLoader;

        public FeedLoaderController(ILogger<FeedLoaderController> logger, IFeedLoader feedLoader)
        {
            _logger = logger;
            _feedLoader = feedLoader;
        }

        [HttpGet]
        public IEnumerable<FeedItem> Load([FromBody] FeedRequest request)
        {
            return _feedLoader.Load(request);
        }
    }
}