using System;
using System.Linq;
using Application.Contracts;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureDIContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        //Rejestracja usług
        services.AddTransient<IGeneratorService, GeneratorService>();

        return services;
    }
}
