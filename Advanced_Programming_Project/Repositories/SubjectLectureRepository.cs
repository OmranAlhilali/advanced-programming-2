using Data;
using Models;

namespace Repository
{

    public class SubjectLectureRepository : Repository<SubjectLecture>
    {

        readonly EFContext _context;

        public SubjectLectureRepository(EFContext context) : base(context)
        {
            _context = context;

        }

        public IEnumerable<SubjectLecture> GetSubjectLecture(int PageSize, int PageIndex, int SubjectId)
        {
            return _context.SubjectLectures.Where(e => e.SubjectId == SubjectId).Skip(PageSize*(PageIndex-1)).Take(PageSize);
        }
    }
}
