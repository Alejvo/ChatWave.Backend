using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultString") ??
                                        throw new ApplicationException("Connection string is null");
                return new SqlConnectionFactory(connectionString);
            });

            services.AddScoped(
                typeof(IRepository<>),
                typeof(Repository<>));

            services.AddScoped<IUserRepository,UserRepository>();
            return services;
        }
    }
}
