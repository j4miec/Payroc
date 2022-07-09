using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Models
{
    [Index(nameof(ShortenedKey), IsUnique = true)]
    public class ShortenedUrl : IValidatableObject
    {
        public Guid ID { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [MaxLength(10)]
        public string ShortenedKey { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Url == "test")
            {
                yield return new ValidationResult("boo");
            }
        }
    }
}
