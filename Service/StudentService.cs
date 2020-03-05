using DTO;
using DTO.Wrapper;
using Models.Models;
using Repository;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Linq;

namespace Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper<Student, StudentDetailsDto> _studentDetailMapper;
        public StudentService(IStudentRepository studentRepository, IMapper<Student, StudentDetailsDto> studentDetailMapper)
        {
            _studentRepository = studentRepository;
            _studentDetailMapper = studentDetailMapper;
        }

        public Response GetStudentInformation(int studentId)
        {
            var student =  _studentRepository.GetStudentInformation(studentId);

            return student == null ?
                new Response(StatusCode.NotFound, $"Student  with  id {studentId} not found") :  new Response(StatusCode.Success, _studentDetailMapper.ToDto(student));
        }

        public Response GetStudentList()
        {
            var student = _studentRepository.GetStudentList();
            //System.Threading.Thread.Sleep(8000);
            return student == null ?
                new Response(StatusCode.NotFound, $"Students details not found") : new Response(StatusCode.Success, _studentDetailMapper.ToDto(student.ToList()));
        }

        public Response GetNothing()
        {
            var student = _studentRepository.GetStudentList();
            System.Threading.Thread.Sleep(1000);
            return student == null ?
                new Response(StatusCode.NotFound, $"Students details not found") : new Response(StatusCode.Success, _studentDetailMapper.ToDto(student.ToList()));
        }

        public Response SaveStudent(StudentDetailsDto studentDetailsDto)
        {
            var studentModel = _studentDetailMapper.ToEntity(studentDetailsDto);
            _studentRepository.Add(studentModel);
            var result = _studentRepository.Save();
            return result > 0 ?
                new Response(StatusCode.Success,"Data Saved successfully", _studentDetailMapper.ToDto(studentModel)) :
                new Response(StatusCode.Failure);

        }
    }
}
