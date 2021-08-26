using Common;
using Data;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Configuration
{
    public static class IdentityConfigurationExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings settings)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = settings.PasswordRequireDigit;
                options.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumeric;
                options.Password.RequiredLength = settings.PasswordRequiredLength;
                options.Password.RequireLowercase = settings.PasswordRequireLowercase;
                options.Password.RequireUppercase = settings.PasswordRequireUppercase;
                options.User.RequireUniqueEmail = settings.UserRequireUniqueEmail;
            })
             .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
