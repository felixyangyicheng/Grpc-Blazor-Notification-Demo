
using Realtime_D3.GRPC.Data;

using Notification;
using RealTime_D3.GRPC.Repositroy;
namespace RealTime_D3.GRPC.Repository;

    public class TbllogRepository : BaseRepository<Tbllog>, ITbllogRepository
    {

        private readonly GrpcDbContext _db;
      
        public TbllogRepository(GrpcDbContext db) : base(db)
        {
            _db = db;
           
        }

    }

