using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MyPortfolio.Presentation.Models
{
    public class PluginCreateViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name = "GitHub URL")]
        [Url]
        public string GitHubUrl { get; set; }

        [Display(Name = "Live Demo URL")]
        [Url]
        public string LiveDemoUrl { get; set; }

        [Required]
        [Display(Name = "Technologies")]
        public List<string> Technologies { get; set; } = new List<string>();

        [Display(Name = "Features")]
        public List<string> Features { get; set; } = new List<string>();
    }
} 