using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCrawl.Entity.Models
{
    public class CrawlingResult
    {
        [Key]
        public int Id { get; set; }

        public string BasePage { get; set; }

        public DateTime Date { get; set; }

        public virtual IEnumerable<CheckedPage> Pages { get; set; }
    }
}
