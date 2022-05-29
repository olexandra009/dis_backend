using DIS_data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DIS_data.EntityConfigurations
{
    class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(n => n.Login);
            builder.Property(n => n.Login).HasColumnName("Login").IsRequired();
            builder.Property(n => n.Password).HasColumnName("Password").IsRequired();
            builder.Property(n => n.Role).HasColumnName("Role").IsRequired();
            builder.Property(n => n.IsUserConfitmed).HasColumnName("Confirmed").IsRequired();
        }
    }
}
