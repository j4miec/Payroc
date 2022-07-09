using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class UrlViewModel
    {
        [Required]
        public string Url { get; set; }
    }
}
