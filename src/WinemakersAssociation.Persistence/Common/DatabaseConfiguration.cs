using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WinemakersAssociation.Data.Entities;

namespace WinemakersAssociation.Persistence.Common
{
    public static class DatabaseConfiguration
    {
        public static void AddDatatabase(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<MainContext>(options =>
                options.UseNpgsql(connectionString));
        }
    }
}