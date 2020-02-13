using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class StudentAdditionalInformationDto
    {
        public int StudentAdditionalId { get; set; }
        public int StudentId { get; set; }
        public string Grade { get; set; }
        public bool? IsBatchHolder { get; set; }
        public string BatchName { get; set; }

        
    }
}
