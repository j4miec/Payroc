using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Contexts;
using UrlShortener.Models;

namespace UrlShortener.Shortener
{
    public class ShortenerService : IShortenerService
    {
        private readonly ShortenerContext _context;

        public ShortenerService(ShortenerContext context)
        {
            _context = context;
        }

        public async Task<ShortenedUrl?> Shorten(string url)
        {
            string key;
            do
            {
                var salt = new byte[128 / 8];

                using (var rngCsp = RandomNumberGenerator.Create())
                {
                    rngCsp.GetBytes(salt);
                }

                var hash = Convert.ToBase64String(salt);

                key = hash[..10].Replace('/', '+');
            } while (await _context.ShortenedUrls.AnyAsync(su => su.ShortenedKey == key));

            var shortened = new ShortenedUrl
            {
                Url = url,
                ShortenedKey = key,
                Created = DateTime.Now
            };

            try
            {
                await _context.ShortenedUrls.AddAsync(shortened);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return null;
            }

            return shortened;
        }

        public async Task<string?> GetUrl(string shortKey)
        {
            var su = await _context.ShortenedUrls.SingleOrDefaultAsync(su => su.ShortenedKey == shortKey);
            return su?.Url;
        }
    }
}
