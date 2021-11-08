using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webcrawl.Entity
{
    public class ParseResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MyProperty { get; set; }
    }
}
