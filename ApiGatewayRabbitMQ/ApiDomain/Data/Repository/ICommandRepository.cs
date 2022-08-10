namespace ApiDomain.Data.Repository;

public interface ICommandRepository<T, in TKey>
{
    Task<T> Save(T entity);
    Task Update(T entity);
    Task<bool> Delete(TKey id);
}