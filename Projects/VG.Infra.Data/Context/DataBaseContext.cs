using Microsoft.EntityFrameworkCore;
using VG.Infra.Data.Entities;
using VG.Infra.Data.Entities.Config;

namespace VG.Infra.Data.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
                : base(options)
        {
            // Cria a database caso não exista
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ModelEntityConfig());
            modelBuilder.ApplyConfiguration(new TruckEntityConfig());
        }

        public virtual DbSet<ModelEntity> Models { get; set; }
        public virtual DbSet<TruckEntity> Trucks { get; set; }

    }
}
