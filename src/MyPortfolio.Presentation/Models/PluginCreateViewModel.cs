using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Technologies (comma separated)")]
        public string Technologies { get; set; }

        [Display(Name = "Features (comma separated)")]
        public string Features { get; set; }
    }
} 