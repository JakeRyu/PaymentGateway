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
               // builder.Property(x => x.CardNumber).HasMaxLength(20);
               builder.Property(x => x.Cvv).HasMaxLength(3);
               
               builder.OwnsOne(x => x.CardExpiryDate,
                   owned =>
                   {
                       owned.Property(p => p.Date).HasColumnName("CardExpiryDate");
                   });
               
               builder.OwnsOne(x => x.Money,
                   owned =>
                   {
                       owned.Property(p => p.Amount).HasColumnName("Amount");
                       owned.Property(p => p.Currency).HasColumnName("Currency");
                   });

               builder.OwnsOne(x => x.CardNumber,
                   owned =>
                   {
                       owned.Property(p => p.OriginalValue).HasColumnName("CardNumber");
                       owned.Ignore(p => p.Value);
                   });
           }
       }
   }