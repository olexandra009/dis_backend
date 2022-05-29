using DIS_data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DIS_data.EntityConfigurations
{
    public class HistoryEntityConfiguration : IEntityTypeConfiguration<HistoryTransactionEntity>
    {
        public void Configure(EntityTypeBuilder<HistoryTransactionEntity> builder)
        {
            builder.ToTable("History");
            builder.Property(h => h.TransactionId).HasColumnName("Id").IsRequired();
            builder.Property(h=>h.UserLogin).HasColumnName("UserLogin").IsRequired();
            builder.Property(h => h.TransactionTime).HasColumnName("TranDate").IsRequired();
            builder.Property(h => h.TransactionType).HasColumnName("TransactionType").IsRequired();
            builder.Property(h => h.Amount).HasColumnName("Amount").IsRequired();
            builder.Property(h => h.MoneyType).HasColumnName("MoneyType").IsRequired();
            builder.HasKey(e => e.TransactionId);
            builder.HasAlternateKey(e => new {e.UserLogin, e.TransactionTime});

        }
    }
}
