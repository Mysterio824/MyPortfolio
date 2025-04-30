using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyPortfolio.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string GitHubUrl { get; set; }
        public string LiveDemoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> Technologies { get; set; } = new List<string>();
        public List<string> Features { get; set; } = new List<string>();
        
        // Default constructor for serialization
        [JsonConstructor]
        public Project()
        {
        }
        
        // Constructor with properties
        public Project(string title, string description, string imageUrl, string gitHubUrl, string liveDemoUrl)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            GitHubUrl = gitHubUrl;
            LiveDemoUrl = liveDemoUrl;
            CreatedDate = DateTime.Now;
        }
    }
} 