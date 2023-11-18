using HumanoSeguros.Domain.Ports;
using HumanoSeguros.Domain.Services;
using HumanoSeguros.Infraestructure.Adapters;
using HumanoSeguros.Infraestructure.Ports;
using HumanoSeguros.Infrastructure.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace HumanoSeguros.Infraestructure.Extensions;

public static class AutoLoadServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        // repositorio genérico
        services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
        
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        // todos los servicios con atributo de servicio de dominio, también podemos hacer esto "por convención",
        // nombrando servicios con sufijo "Service" si decidimos eliminar el decorador de servicio de dominio
        var _services = AppDomain.CurrentDomain.GetAssemblies()
              .Where(assembly =>
              {
                  return (assembly.FullName is null) || assembly.FullName.Contains("Domain", StringComparison.InvariantCulture);
              })
              .SelectMany(s => s.GetTypes())
              .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(DomainServiceAttribute)));

        // Igual, pero con repositorios
        var _repositories = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly =>
            {
                return (assembly.FullName is null) || assembly.FullName.Contains("Infraestructure", StringComparison.InvariantCulture);
            })
            .SelectMany(s => s.GetTypes())
            .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(RepositoryAttribute)));

        // servicios
        foreach (var service in _services)
        {
            services.AddTransient(service);
        }

        // repositorios
        foreach (var repo in _repositories)
        {
            Type? iface = repo.GetInterfaces().SingleOrDefault();
            if (iface == null)
            {
                throw new InvalidOperationException($"El tipo {repo.Name} no implementa ninguna interfaz o implementa más de una.");
            }
            services.AddTransient(iface, repo);
        }

        return services;
    }
}
