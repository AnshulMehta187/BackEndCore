using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    [Table("Student")]
    public partial class Student
    {
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Gender { get; set; }

        public virtual StudentDetails StudentDetails { get; set; }
    }
}
