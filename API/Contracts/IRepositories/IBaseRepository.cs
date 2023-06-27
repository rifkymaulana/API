namespace API.Contracts.IRepositories;

public interface IBaseRepository<T>
{
    ICollection<T> GetAll();
    T? GetByGuid(Guid guid);
    T Create(T entity);
    bool Update(T entity);
    bool Delete(Guid guid);
}