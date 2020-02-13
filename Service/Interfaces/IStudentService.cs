using DTO;
using DTO.Wrapper;
using Models.Models;

namespace Service.Interfaces
{
    public interface IStudentService : IService
    {
        StudentDetailsDto GetStudentInformation(int studentId);

        Response SaveStudent(StudentDetailsDto studentDetailsDto);
    }
}
