using ImagesApi.DAL.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace ImagesApi.DAL.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Format).IsRequired().HasMaxLength(6);
                entity.Property(e => e.Data).IsRequired();
            });
        }
    }
}
