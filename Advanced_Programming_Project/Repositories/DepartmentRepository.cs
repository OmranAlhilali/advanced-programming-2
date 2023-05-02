using Data;
using Models;

namespace Repository
{

    public class DepartmentRepository : Repository<Department>
    {
        public DepartmentRepository(EFContext context) : base(context)
        {
        }
    }
}
