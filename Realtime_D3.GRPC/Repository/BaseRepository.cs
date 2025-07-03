using Microsoft.EntityFrameworkCore;
using Realtime_D3.GRPC.Data;
using Realtime_D3.GRPC.Repository;


namespace RealTime_D3.GRPC.Repositroy
{
    public class BaseRepository<T> : IRepositoryBase<T> where T : class
    {

        private readonly GrpcDbContext _db;
      
        public BaseRepository(GrpcDbContext db)
        {
            _db = db;
        
        }

        public async Task<T> AddAsync(T entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await _db.Set<T>().FindAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id == null)
            {
                throw new NullReferenceException(" null reference ");
            }
            else { 
                return await _db.Set<T>().FindAsync(id)?? throw new NullReferenceException(" null reference ");
            }
        }

        //public async Task<VirtualizeResponse<TResult>> GetAllAsync<TResult>(QueryParameters queryParam)
        //    where TResult : class
        //{
        //    var totalSize = await context.Set<T>().CountAsync();
        //    var items = await context.Set<T>()
        //        .Skip(queryParam.StartIndex)
        //        .Take(queryParam.PageSize)
        //        .ProjectTo<TResult>(mapper.ConfigurationProvider)
        //        .ToListAsync();

        //    return new VirtualizeResponse<TResult> { Items = items, TotalSize = totalSize };
        //}

        public async Task UpdateAsync(T entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}


