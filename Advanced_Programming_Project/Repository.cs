using System.Linq.Expressions;
namespace Data
{
    public class Repository<T> where T : class
    {
        protected readonly EFContext _context;
        public Repository(EFContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }


        public IEnumerable<T> GetAll(int PageSize,int PageIndex)
        {
            return _context.Set<T>().Skip(PageSize*(PageIndex-1)).Take(PageSize);
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
