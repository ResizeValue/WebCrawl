using System;
using System.ComponentModel.DataAnnotations;

namespace WebCrawl.Entity.Models
{
    public class CheckedPage
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        public TimeSpan ResponseTime { get; set; }
    }
}
