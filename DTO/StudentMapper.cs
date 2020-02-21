using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    class StudentMapper : IMapper<Student, StudentDetailsDto>
    {
        public StudentDetailsDto ToDto(Student entity)
        {
            return new StudentDetailsDto
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateofBirth = entity.DateofBirth,
                Gender = entity.Gender,
                StudentId = entity.Id,
                StudentAdditionalInformation = new StudentAdditionalInformationDto
                {
                    BatchName = entity.StudentDetails.BatchName,
                    IsBatchHolder = entity.StudentDetails.IsBatchHolder,
                    StudentAdditionalId = entity.StudentDetails.Id,
                    StudentId = entity.Id
                }
            };
        }

        public IEnumerable<StudentDetailsDto> ToDto(IEnumerable<Student> entities)
        {
            return entities.Select(ToDto).ToList();
        }

        public Student ToEntity(StudentDetailsDto dto)
        {
            return new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateofBirth = dto.DateofBirth,
                Gender = dto.Gender,
                StudentDetails = new StudentDetails
                {
                    Grade = dto.StudentAdditionalInformation.Grade,
                    IsBatchHolder = dto.StudentAdditionalInformation.IsBatchHolder,
                }
            };
        }

        public void ToEntity(StudentDetailsDto dto, Student entity)
        {
            if (entity == null)
                entity = new Student();
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.DateofBirth = dto.DateofBirth;
            entity.Gender = dto.Gender;
            if (entity.StudentDetails == null)
                entity.StudentDetails = new StudentDetails();
            entity.StudentDetails.Grade = dto.StudentAdditionalInformation.Grade;
            entity.StudentDetails.IsBatchHolder = dto.StudentAdditionalInformation.IsBatchHolder;
        }
    }
}

