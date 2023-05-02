using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{

    public class StudentRepository : Repository<Student>
    {
       readonly EFContext _context;
        public StudentRepository(EFContext context) : base(context)
        { 
            _context = context;
        }

        public int StudentsCount()
        {
            return _context.Students.Count();
        }
        public IEnumerable<Student> GetStudentsByDepartmentId(int PageSize,int PageIndex,int DepartmentID) { 
        return _context.Students.Where(e=>e.DepartmentId == DepartmentID).Skip(PageSize*(PageIndex-1)).Take(PageSize);  
        }

        public Student GetStudentsNotTakeExam( int Id)
        {
            return _context.Students.Where(e => e.Id != Id).First();
        }


    }
}
