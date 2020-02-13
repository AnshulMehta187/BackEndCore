using DTO;
using DTO.Wrapper;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        StudentDetailsDto GetStudentInformation(int studentId);

    }
}
