namespace ApiDomain.Data.Repository;

public interface IQueryRepository<T, in TKey>
{
    Task<T> Get(TKey index);
    Task<IEnumerable<T>> GetAll();
}