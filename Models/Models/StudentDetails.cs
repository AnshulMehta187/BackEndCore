using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    [Table("StudentDetails")]
    public partial class StudentDetails
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Grade { get; set; }
        public bool? IsBatchHolder { get; set; }
        public string BatchName { get; set; }
    }
}
