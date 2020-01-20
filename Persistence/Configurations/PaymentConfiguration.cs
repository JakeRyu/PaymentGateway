using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(x => x.Id).HasColumnName("PaymentId");

            builder.Property(x => x.MerchantId).HasColumnName("MerchantId");

            builder.Property(x => x.CardHolderName).HasMaxLength(60);

            builder.Property(x => x.CardNumber).HasMaxLength(20);
        }
    }
}