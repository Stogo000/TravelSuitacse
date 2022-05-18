﻿using Microsoft.Extensions.DependencyInjection;
using TravelSuitcase.Application.Common;
using TravelSuitcase.Domain.Repositories.Users;
using TravelSuitcase.Domain.Services;
using TravelSuitcase.Infrastructure.Persistence;
using TravelSuitcase.Infrastructure.Persistence.Repositories.Users;
using TravelSuitcase.Infrastructure.Services;

namespace TravelSuitcase.Tests
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPasswordHashService, PasswordHashServices>();
            services.AddTransient<ISecurityService, SecurityServices>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}