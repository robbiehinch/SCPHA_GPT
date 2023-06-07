using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SCPHA_GPT
{
    public class GeneratedPortAuthorityImage
    {
        [Key]
        public DateTime RequestTime { get; set; }
        public Uri ImageUri { get; set; }
        public string Prompt { get; set; }
        public string Error { get; set; }
    }
}