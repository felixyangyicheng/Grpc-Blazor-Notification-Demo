using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealTime_D3.Contracts;
using RealTime_D3.Models;

namespace RealTime_D3.Services
{
    public class BaseRepository<T> : IRepositoryBase<T> where T : class
    {

        private readonly RealtimeDbContext _db;
        private readonly IMapper _mapper;
        public BaseRepository(RealtimeDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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
            var entity = await GetAsync(id);
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


