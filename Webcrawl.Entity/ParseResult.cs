using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebCrawl.Logic.Models;

namespace Webcrawl.Entity
{
    public class ParseResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CheckDate { get; set; }

        public virtual ICollection<ResponseParsedUrl> Results { get; set; }
    }
}
