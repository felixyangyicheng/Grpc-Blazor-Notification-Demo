using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealTime_D3.Hubs;

namespace RealTime_D3.Models
{
    public class RealtimeDbContext : IdentityDbContext
    {
        public DbSet<Tbllog> Tbllogs { get; set; }
        public RealtimeDbContext(DbContextOptions<RealtimeDbContext> options)
   : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
