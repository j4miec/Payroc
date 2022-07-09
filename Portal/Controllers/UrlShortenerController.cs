using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using UrlShortener.Shortener;

namespace Portal.Controllers
{
    public class UrlShortenerController : Controller
    {
        private readonly IShortenerService _shortenerService;

        public UrlShortenerController(IShortenerService shortenerService)
        {
            _shortenerService = shortenerService;
        }

        public IActionResult Shorten()
        {
            return View();
        }

        public async Task<IActionResult> Shorten(ShortenedUrl su)
        {
            if (ModelState.IsValid)
            {
                var result = await _shortenerService.Shorten(su.Url);

                if (result)
                {
                    return RedirectToAction(nameof(Created), su);
                }

                TempData["CreationDenied"] ??= "Something went wrong with the creation.";
            }

            TempData["CreationDenied"] ??= "A validation error occurred. Please check your input";
            return View(su);
        }

        public IActionResult Created(ShortenedUrl su)
        {
            return View(su);
        }

        public async Task<IActionResult> Follow(string urlKey)
        {
            var result = await _shortenerService.GetUrl(urlKey);

            if (!string.IsNullOrEmpty(result))
            {
                return Redirect(result);
            }

            TempData["RedirectFailed"] ??= "The redirect failed. The shortening may not exist, or may have expired";
            return View(result);
        }
    }
}
