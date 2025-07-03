
using Realtime_D3.GRPC.Data;
using RealTime_D3.GRPC.Repositroy;
using Notification;
namespace RealTime_D3.Grpc.Repository;

    public class TbllogRepository : BaseRepository<Tbllog>, ITbllogRepository
    {

        private readonly GrpcDbContext _db;
      
        public TbllogRepository(GrpcDbContext db) : base(db)
        {
            _db = db;
           
        }

    }

