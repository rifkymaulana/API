using API.Contracts;
using API.Data;

namespace API.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    
    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public ICollection<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }
    
    public T? GetByGuid(Guid guid)
    {
        return _context.Set<T>().Find(guid);
    }
    
    public T Create(T entity)
    {
        try
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        catch
        {
            return null;
        }
    }
    
    public bool Update(T entity)
    {
        try
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            Console.WriteLine("Update success");
            return true;
        }
        catch
        {
            Console.WriteLine("Update failed");
            return false;
        }
    }
    
    public bool Delete(T entity)
    {
        try
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool IsExist(Guid guid)
    {
        return _context.Set<T>().Find(guid) != null;
    }
}