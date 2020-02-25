using DTO;
using DTO.Wrapper;
using Models.Models;

namespace Service.Interfaces
{
    public interface IStudentService : IService
    {
        Response GetStudentInformation(int studentId);

        Response SaveStudent(StudentDetailsDto studentDetailsDto);

        Response GetStudentList();

        Response GetNothing();

    }
}
