namespace UPBank.Utils.CommonsFiles
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<(TEntity? entity, string message)> AddAsync(TEntity entity);
        Task<(bool ok, string message)> DeleteAsync<TKey>(TKey key);
        Task<(TEntity? entity, string message)> UpdateAsync(TEntity entity);
        Task<(TEntity? entity, string message)> GetOneAsync<TKey>(TKey key);
        Task<(IEnumerable<TEntity>? entities, string message)> GetAllAsync();
    }
}
