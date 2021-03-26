using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Repository.Mapping
{
    public class CauseMapping : IEntityTypeConfiguration<Cause>
    {
        public void Configure(EntityTypeBuilder<Cause> builder)
        {
            builder.HasKey(id => id.Id);

            builder.Property(name => name.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(city => city.City)
                .IsRequired()
                .HasMaxLength(150);
            
            builder.Property(state => state.State)
                .IsRequired()
                .HasMaxLength(2);
            
            builder.Ignore(validationResult => validationResult.ValidationResult);
            builder.Ignore(errorMessage => errorMessage.ErrorsMessages);

            builder.ToTable("Cause");
        }
    }
}