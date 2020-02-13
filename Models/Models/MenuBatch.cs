using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public partial class MenuBatch
    {
        [Key]
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public bool Status { get; set; }
    }
}
