using DTO;
using DTO.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            
            _studentService = studentService;
            
        }
        [HttpGet("GetStudent/{studentId}")]
        public Response GetStudentDetails(int studentId)
        {
            return _studentService.GetStudentInformation(studentId);
        }

        [HttpPost]
        public Response Save([FromBody] StudentDetailsDto studentDetailsDto)
        {
            return  _studentService.SaveStudent(studentDetailsDto);
        }

        [HttpGet("GetStudents")]
        public Response GetStudentList()
        {
            return _studentService.GetStudentList();
        }

        [HttpGet("GetNothing")]
        public Response DoNothing()
        {
            return _studentService.GetNothing();
        }
    }
}