using Microsoft.EntityFrameworkCore;

using Crowdfunding.Domain.Entities;
using Crowdfunding.Repository.Mapping;

namespace Crowdfunding.Repository.Context
{
    public class CrowdfundingOnLineDBContext : DbContext
    {
        public CrowdfundingOnLineDBContext(
            DbContextOptions<CrowdfundingOnLineDBContext> options
        ) : base(options) { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Cause> Causes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMapping());
            modelBuilder.ApplyConfiguration(new AddressMapping());
            modelBuilder.ApplyConfiguration(new DonationMapping());
            modelBuilder.ApplyConfiguration(new CauseMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}