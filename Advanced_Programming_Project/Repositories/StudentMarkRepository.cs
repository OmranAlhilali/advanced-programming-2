using Data;
using Models;

namespace Repository
{

    public class StudentMarkRepository : Repository<StudentMark>
    {
        readonly EFContext _context;

        public StudentMarkRepository(EFContext context) : base(context)
        {
            _context = context;

        }
        public IEnumerable<StudentMark> GetStudentsNotExam(int PageSize, int PageIndex, int ExamId)
        {
            return _context.StudentMarks.Where(e => e.ExamId != ExamId);
        }
    }
}
