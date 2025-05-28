using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Realtime_D3.Shared.Data
{
    public class GrpcDbContext : DbContext
    {

        public virtual DbSet<Coffre> Coffres { get; set; }
        public virtual DbSet<ApiUser> Apiusers { get; set; }
        public virtual DbSet<ApiUserCoffre> ApiUserCoffres { get; set; }

        public virtual DbSet<Entree> Entrees { get; set; }
        public virtual DbSet<CoffreLog> CoffreLogs { get; set; }
        public virtual DbSet<EntreeHistory> EntreeLogs { get; set; }

        public GrpcDbContext()
        {
        }
        public GrpcDbContext(DbContextOptions<GrpcDbContext> options)
        : base(options)
        {
        }

        //// The following configures EF to create a Sqlite database file in the
        // //// special "local" folder for your platform.
        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite($"Data Source={DbPath}");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder
                .Entity<CoffreLog>()
                .Property(e => e.Operation)
                .HasConversion(
                    v => v.ToString(),
                    v => (Operation)Enum.Parse(typeof(Operation), v));
            modelBuilder
                .Entity<EntreeHistory>()
                .Property(e => e.Operation)
                .HasConversion(
                    v => v.ToString(),
                    v => (Operation)Enum.Parse(typeof(Operation), v));
            modelBuilder
                .Entity<ApiUserCoffre>()
                .Property(e => e.Access)
                .HasConversion(
                    v => v.ToString(),
                    v => (Access)Enum.Parse(typeof(Access), v));
            #region Relation N-N

            modelBuilder.Entity<ApiUserCoffre>()
                .HasKey(t => new { t.CoffreId, t.UserId });
            modelBuilder.Entity<ApiUserCoffre>()
                .HasOne(pt => pt.Coffre)
                .WithMany(p => p.ApiUserCoffres)
                .HasForeignKey(pt => pt.CoffreId);
            modelBuilder.Entity<ApiUserCoffre>()
                .HasOne(pt => pt.ApiUser)
                .WithMany(t => t.ApiUserCoffres)
                .HasForeignKey(pt => pt.UserId);
            #endregion Relation N-N

            #region Init User


            #endregion Init User

            #region exemplaire coffre

            #endregion exemplaire coffre

        }
    }
}
