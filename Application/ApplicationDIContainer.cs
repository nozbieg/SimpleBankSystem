using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationDIContainer
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Rejestracja usług
        services.AddMediatR(Assembly.GetExecutingAssembly());



        return services;
    }
}
