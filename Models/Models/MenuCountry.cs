using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public partial class MenuCountry
    {
        public MenuCountry()
        {
            MenuState = new HashSet<MenuState>();
        }

        [Key]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<MenuState> MenuState { get; set; }
    }
}
