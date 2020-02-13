using DTO;
using DTO.Wrapper;
using Models.Models;
using Repository.Interfaces;
using System.Linq;
namespace Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(TestContext testContext)
            : base(testContext)
        {
        }
        StudentDetailsDto IStudentRepository.GetStudentInformation(int studentId)
        {
            var studentInfo = (from student in testContext.Student
                               where student.Id == studentId
                               select new StudentDetailsDto
                               {
                                   DateofBirth = student.DateofBirth,
                                   FirstName = student.FirstName,
                                   LastName = student.LastName,
                                   Gender = student.Gender,
                                   StudentAdditionalInformation = new StudentAdditionalInformationDto
                                   {
                                       BatchName = student.StudentDetails != null ? student.StudentDetails.BatchName : string.Empty,
                                       IsBatchHolder = student.StudentDetails != null ? student.StudentDetails.IsBatchHolder : false,
                                       Grade = student.StudentDetails != null ? student.StudentDetails.Grade : string.Empty,
                                   }
                               }).FirstOrDefault();
            return studentInfo;
        }
    }
}
