using UrlShortener.Models;

namespace UrlShortener.Shortener
{
    public interface IShortenerService
    {
        public Task<ShortenedUrl?> Shorten(string url);

        public Task<string?> GetUrl(string shortKey);
    }
}
