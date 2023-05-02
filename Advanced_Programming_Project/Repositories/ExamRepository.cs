using Data;
using Models;

namespace Repository
{

    public class ExamRepository : Repository<Exam>
    {
        readonly EFContext _context;

        public ExamRepository(EFContext context) : base(context)
        {
            _context = context;

        }


    }
}
