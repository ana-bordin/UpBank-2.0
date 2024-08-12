namespace UPBank.Utils.CommonsFiles.Contracts.Services
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<TEntity?> AddAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(TEntity entity);
        Task<TEntity?> GetOneAsync(string key);
    }
}