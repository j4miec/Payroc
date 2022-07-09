using System.Web;
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

        [HttpPost]
        public async Task<IActionResult> Shorten(string url)
        {
            if (ModelState.IsValid)
            {
                var result = await _shortenerService.Shorten(url);

                if (result != null)
                {
                    return RedirectToAction(nameof(Created), result);
                }

                TempData["CreationDenied"] ??= "Something went wrong with the creation.";
            }

            TempData["CreationDenied"] ??= "A validation error occurred. Please check your input";
            return View(url);
        }

        public IActionResult Created(ShortenedUrl su)
        {
            var request = HttpContext.Request;
            var host = request.Host.ToUriComponent();
            su.ShortenedKey = $"{request.Scheme}://{host}/Go/{su.ShortenedKey}";
            return View(su);
        }

        [Route("go/{urlKey}")]
        public async Task<IActionResult> Follow(string urlKey)
        {
            var result = await _shortenerService.GetUrl(urlKey);

            if (!string.IsNullOrEmpty(result))
            {
                var decoded = HttpUtility.UrlDecode(result);
                return Redirect(decoded);
            }

            return View();
        }
    }
}
