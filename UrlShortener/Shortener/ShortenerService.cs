using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Contexts;

namespace UrlShortener.Shortener
{
    public class ShortenerService : IShortenerService
    {
        private readonly ShortenerContext _context;

        public ShortenerService(ShortenerContext context)
        {
            _context = context;
        }

        public Task<bool> Shorten(string url)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUrl(string shortKey)
        {
            throw new NotImplementedException();
        }
    }
}
