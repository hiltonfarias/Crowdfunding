using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Crowdfunding.Domain;
using Crowdfunding.Repository;
using Crowdfunding.Repository.Context;
using Crowdfunding.Service;
using Crowdfunding.Service.AutoMapper;

namespace Crowdfunding.MVC.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            return services.AddDbContext<CrowdfundingOnLineDBContext>(
                options => options.UseInMemoryDatabase("CrowdfundingOnLine")
            );
        }
        public static IServiceCollection AddIocConfiguration(
            this IServiceCollection services, 
            IConfiguration configuration
        )
        {
            services.AddScoped<ICauseService, CauseService>();
            services.AddScoped<IHomeInfoService, HomeInfoService>();

            services.AddScoped<IDomainNotificationService, DomainNotificationService>();
            services.AddScoped<IDonationService, DonationService>();
            services.AddScoped<IDonationRepository, DonationRepository>();

            services.AddScoped<ICauseRepository, CauseRepository>();
            services.AddScoped<IHomeInfoRepository, HomeInfoRepository>();

            return services;
        }
        public static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.Bind("General configuration application", new GloballAppConfig());
            services.AddSingleton(new MapperConfiguration(
                configuration => configuration.AddProfile<CrowdfundingOnLineMappingProfile>()
            ).CreateMapper());
            return services;
        }
        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.Bind("General configuration application", new GloballAppConfig());
            services.AddSingleton(new GloballAppConfig());
            return services;
        }
    }
}