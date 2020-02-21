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

        public IQueryable<Student> GetStudentList()
        {
            var student = from data in testContext.Student.AsQueryable() select data;
            return student;
        }
        Student IStudentRepository.GetStudentInformation(int studentId)
        {
            var studentInfo =  testContext.Student.Where(x => x.Id == studentId).FirstOrDefault();

            return studentInfo;
        }
    }
}
