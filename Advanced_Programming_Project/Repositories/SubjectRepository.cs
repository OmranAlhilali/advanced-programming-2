using Data;
using Models;

namespace Repository
{

    public class SubjectRepository : Repository<Subject>
    {
        readonly EFContext _context;
        public SubjectRepository(EFContext context) : base(context)
        {
            _context = context;

        }


    }
}
