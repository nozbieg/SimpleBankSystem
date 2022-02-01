using System;
using System.Linq;
using Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Repositories;

namespace Persistance;

public static class PersistanceDIContainer
{

    public static IServiceCollection AddPersistanceServices(this IServiceCollection services)
    {
        //Rejestracja usług
        services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
        services.AddEntityFrameworkSqlite().AddDbContext<BankDbContext>();


        return services;
    }
}
