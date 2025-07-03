namespace Realtime_D3.GRPC.Repository
{
    public interface IRepositoryBase<T> where T : class
    {

        Task<T> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> Exists(int id);
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int? id);
        Task UpdateAsync(T entity);
    }
}
