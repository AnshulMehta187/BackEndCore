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

        public StudentDetailsDto GetStudentInformation(int studentId)
        {
            return _studentRepository.GetStudentInformation(studentId);
        }

        public Response SaveStudent(StudentDetailsDto studentDetailsDto)
        {
            var studentModel = _studentDetailMapper.ToEntity(studentDetailsDto);
            _studentRepository.Add(studentModel);
            var result = _studentRepository.Save();
            return result > 0 ?
                new Response(StatusCode.Success, _studentDetailMapper.ToDto(studentModel)) :
                new Response(StatusCode.Failure);

        }
    }
}
