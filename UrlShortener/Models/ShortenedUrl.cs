using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Models
{
    [Index(nameof(ShortenedKey), IsUnique = true)]
    public class ShortenedUrl
    {
        public Guid ID { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Shortened url")]
        public string ShortenedKey { get; set; }

        public DateTime Created { get; set; }
    }
}
