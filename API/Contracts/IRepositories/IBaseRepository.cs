namespace API.Contracts;

public interface IBaseRepository<T>
{
    ICollection<T> GetAll();
    T? GetByGuid(Guid guid);
    T Create(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    bool IsExist(Guid guid);
}