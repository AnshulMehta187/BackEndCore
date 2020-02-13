using System;

namespace DTO
{
    public class StudentDetailsDto
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Gender { get; set; }
        public StudentAdditionalInformationDto StudentAdditionalInformation { get; set; }
    }
}
