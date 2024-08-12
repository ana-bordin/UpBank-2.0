namespace UPBank.Utils.CommonsFiles.Contracts.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> AddAsync(TEntity entity);
        Task<bool> DeleteAsync(string key);
        Task<TEntity?> UpdateAsync(TEntity entity);
        Task<TEntity?> GetOneAsync(string key);
        Task<IEnumerable<TEntity>?> GetAllAsync();
    }
}