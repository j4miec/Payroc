namespace UrlShortener.Shortener
{
    public interface IShortenerService
    {
        public Task<bool> Shorten(string url);

        public Task<string> GetUrl(string shortKey);
    }
}
