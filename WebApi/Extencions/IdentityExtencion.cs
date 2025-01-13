using DAL;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Extencions
{
    public static class IdentityExtencions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            //.AddRoles<IdentityRole<Guid>>() оно вообще что-то меняет?
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
