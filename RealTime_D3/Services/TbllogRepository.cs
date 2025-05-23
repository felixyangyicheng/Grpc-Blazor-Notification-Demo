using AutoMapper;
using RealTime_D3.Contracts;
using RealTime_D3.Models;

namespace RealTime_D3.Services
{
    public class TbllogRepository : BaseRepository<Tbllog>, ITbllogRepository
    {

        private readonly RealtimeDbContext _db;
        private readonly IMapper _mapper;
        public TbllogRepository(RealtimeDbContext db, IMapper mapper) : base(db, mapper)
        {
            _db = db;
            _mapper = mapper;
        }

    }
}
