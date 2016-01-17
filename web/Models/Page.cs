using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Page
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required, Range(typeof(DateTime), "1/1/2015", "1/1/2020")]
        public DateTime Timestamp { get; set; }

        [Required, Column("Live")]
        public bool Aggregate { get; set; }

        [Required]
        public bool Partial { get; set; }

        [Required]
        public bool Crawl { get; set; }

        public string TwitterStatus { get; set; }
    }
}