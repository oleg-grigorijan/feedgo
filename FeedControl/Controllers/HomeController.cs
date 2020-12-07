using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FeedControl.Models;
using FeedControl.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FeedControl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeedService _service;

        public HomeController(ILogger<HomeController> logger, IFeedService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index(FeedRequestForm form)
        {
            if (form.IsEmpty())
            {
                ViewBag.FeedItems = ImmutableArray<FeedItem>.Empty;
            }
            else
            {
                IEnumerable<string> urls = form.Urls.SplitLines();
                var filter = new FeedFilter
                {
                    ContentKeyWords = form.ContentKeyWords.SplitLines(),
                    Categories = form.Categories.SplitLines(),
                    DateFrom = null,
                    DateTo = null
                };
                IEnumerable<string> emails = form.Emails.SplitLines();
                var feedItems = await _service.LoadAsync(urls, filter);
                _service.MailAsync(emails, feedItems);
                ViewBag.FeedItems = feedItems;
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }

    public static class Extensions
    {
        public static IEnumerable<string> SplitLines(this string? @this)
        {
            return @this?
                       .Split("\n")
                       .Select(str => str.Trim())
                       .Where(str => !str.Equals(""))
                   ?? ArraySegment<string>.Empty;
        }
    }
}