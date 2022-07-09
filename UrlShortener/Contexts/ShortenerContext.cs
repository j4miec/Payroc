using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Contexts
{
    public class ShortenerContext : DbContext
    {
        public DbSet<ShortenedUrl> ShortenedUrls => Set<ShortenedUrl>();

        public ShortenerContext(DbContextOptions<ShortenerContext> options) : base(options)
        {
        }
    }
}
