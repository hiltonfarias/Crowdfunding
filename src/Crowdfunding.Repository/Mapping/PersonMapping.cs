using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Repository.Mapping
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(id => id.Id);

            builder.Property(name => name.Name)
                .IsRequired()
                .HasMaxLength(150);
            
            builder.Property(email => email.Email)
                .IsRequired()
                .HasMaxLength(150);
            
            builder.Property(anonymous => anonymous.Anonymous)
                .IsRequired();
            
            builder.Property(supportMessage => supportMessage.SupportMessage)
                .IsRequired()
                .HasMaxLength(500);
            
            builder.HasMany(donation => donation.Donation)
                .WithOne(personalData => personalData.PersonalData)
                .HasForeignKey(personalDataId => personalDataId.PersonalDataId);
            
            builder.Ignore(validationResult => validationResult.ValidationResult);

            builder.Ignore(errorMessage => errorMessage.ErrorsMessages);
            
            builder.ToTable("Person");
        }
    }
}