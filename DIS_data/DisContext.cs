using DIS_data.Entity;
using DIS_data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DIS_data
{
    public class DisContext: DbContext
    {
        public DisContext()
        {
            
        }

        public DisContext(DbContextOptions<DisContext> options): base(options)
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //if (!optionsBuilder.IsConfigured)
        //    //{
        //    //    optionsBuilder.UseNpgsql("host=ec2-44-195-169-163.compute-1.amazonaws.com;port=5432;database=d7guu018uguu2t;user id=qkbuogcjpsjrsw; password=b8b7c1ed75f6476bf783f7e380d56672781e1dbbaa5daa2d322a00bf341d469b; Pooling=true;SSLMode=Require; TrustServerCertificate=True;");
        //    //}
        //}

        public virtual DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
