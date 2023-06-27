namespace API.Contracts.IServices;

public interface IBaseService<T>
{
    IEnumerable<T>? GetAll();
    IEnumerable<T>? GetByName(string name);
    T? GetByGuid(Guid guid);
    T? Create(T entityDto);
    int Update(T entityDto);
    int Delete(Guid guid);
}