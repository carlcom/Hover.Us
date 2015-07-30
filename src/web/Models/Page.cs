using System;

namespace web.Models
{
    public class Page
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Live { get; set; }
        public string TwitterStatus { get; set; }
    }
}
