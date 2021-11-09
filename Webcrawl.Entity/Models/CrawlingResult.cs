using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebCrawl.Entity.Models
{
    public class CrawlingResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string BasePage { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual IEnumerable<CheckedPage> Pages { get; set; }
    }
}
