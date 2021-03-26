using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Repository.Mapping
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(id => id.Id);

            builder.Property(cep => cep.CEP)
                .IsRequired()
                .HasMaxLength(15);
            
            builder.Property(textAddress => textAddress.TextAddress)
                .IsRequired()
                .HasMaxLength(250);
            
            builder.Property(complement => complement.Complement)
                .IsRequired(false)
                .HasMaxLength(250);
            
            builder.Property(city => city.City)
                .IsRequired()
                .HasMaxLength(150);
            
            builder.Property(state => state.State)
                .IsRequired()
                .HasMaxLength(2);
            
            builder.Property(phone => phone.Phone)
                .IsRequired()
                .HasMaxLength(15);
            
            builder.HasMany(donation => donation.Donation)
                .WithOne(billingAddress => billingAddress.BillingAddress)
                .HasForeignKey(billingAddressId => billingAddressId.BillingAddressId);
            
            builder.Ignore(validationResult => validationResult.ValidationResult);

            builder.Ignore(errorMessage => errorMessage.ErrorsMessages);

            builder.ToTable("Address");
        }
    }
}