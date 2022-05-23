using DIS_data.Entity;
using DIS_data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DIS_data
{
    public class DisContext:DbContext
    {
        public DisContext()
        {
            
        }

        public DisContext(DbContextOptions<DisContext> options):base(options)
        {
            
        }

        public virtual DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
