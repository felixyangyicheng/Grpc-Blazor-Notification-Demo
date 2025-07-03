using Microsoft.EntityFrameworkCore;
using Notification;

namespace Realtime_D3.GRPC.Data
{
    public class GrpcDbContext:DbContext
    {
        public DbSet<Tbllog> Tbllogs { get; set; }
        public GrpcDbContext(DbContextOptions<GrpcDbContext> options)
   : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}

