using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Repository.Mapping
{
    public class DonationMapping : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder.HasKey(id => id.Id);

            builder.Property(value => value.Value)
                .IsRequired()
                .HasColumnType("decimal(9,2)");
            
            builder.Property(dateTime => dateTime.DateTime)
                .IsRequired();
            
            builder.HasOne(personalData => personalData.PersonalData)
                .WithMany(donations => donations.Donation)
                .HasForeignKey(personalDataId => personalDataId.PersonalDataId);
            
            builder.Ignore(formPayment => formPayment.FormPayment);
            builder.Ignore(validationResult => validationResult.ValidationResult);
            builder.Ignore(errorMessage => errorMessage.ErrorsMessages);

            builder.ToTable("Donation");
        }
    }
}