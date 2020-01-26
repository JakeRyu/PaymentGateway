using Domain.Entities;
using Microsoft.EntityFrameworkCore;
   using Microsoft.EntityFrameworkCore.Metadata.Builders;
   
   namespace Persistence.Configurations
   {
       public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
       {
           public void Configure(EntityTypeBuilder<Payment> builder)
           {
               builder.Property(x => x.CardHolderName).HasMaxLength(60);
               builder.Property(x => x.CardNumber).HasMaxLength(20);
               builder.Property(x => x.Cvv).HasMaxLength(3);
               builder.OwnsOne(
                   x => x.CardExpiryDate,
                   owned =>
                   {
                       owned.Property(p => p.Date).HasColumnName("CardExpiryDate");
                   });
           }
       }
   }