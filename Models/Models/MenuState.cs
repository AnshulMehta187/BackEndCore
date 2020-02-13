using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public partial class MenuState
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }
        public bool Status { get; set; }
        public int CountryId { get; set; }

        public virtual MenuCountry Country { get; set; }
    }
}
