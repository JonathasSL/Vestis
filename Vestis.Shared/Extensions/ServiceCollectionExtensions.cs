using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Vestis.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    [Obsolete]
    public static void RegisterAllScopedDependenciestemp(this IServiceCollection services, ILogger logger)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var vestisAssembly = assemblies.Where(assembly => assembly.GetName().Name.Contains("Vestis")).ToList();

        var types = vestisAssembly.SelectMany(assemblies => assemblies.GetTypes());
        var interfaces = types.Where(type => type.IsInterface && type.Name.StartsWith("I")).ToList();

        foreach (var interfaceType in interfaces)
        {
            var implementationType = types.FirstOrDefault(type => type.IsClass && !type.IsAbstract && type.Name.Equals(interfaceType.Name.Substring(1)));
            if (implementationType == null)
            {
                logger.LogWarning($"[DI] No implementation found for interface {interfaceType.Name}");
                continue;
            }
            services.AddScoped(interfaceType, implementationType);
            logger.LogInformation($"[DI] Registrando: {interfaceType}, {implementationType}");
        }
    }

    public static void RegisterAllScopedDependencies(this IServiceCollection services, ILogger logger)
    {
        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        var referencedPaths = Directory.GetFiles(AppContext.BaseDirectory, "Vestis.*.dll");

        foreach (var path in referencedPaths)
        {
            try
            {
                var assemblyName = AssemblyName.GetAssemblyName(path);
                if (!loadedAssemblies.Any(a => a.FullName == assemblyName.FullName))
                {
                    loadedAssemblies.Add(Assembly.Load(assemblyName));
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning($"[DI] Erro ao carregar assembly {path}: {ex.Message}");
            }
        }

        var vestisAssemblies = loadedAssemblies.Where(assembly => assembly.GetName().Name.StartsWith("Vestis")).ToList();
        var types = vestisAssemblies.SelectMany(assembly => assembly.GetTypes());

        var interfaces = types.Where(type => type.IsInterface && type.Name.StartsWith("I")).ToList();

        foreach (var interfaceType in interfaces)
        {
            var implementationType = types.FirstOrDefault(type => type.IsClass && !type.IsAbstract && type.Name.Equals(interfaceType.Name.Substring(1)));
            if (implementationType == null)
            {
                logger.LogWarning($"[DI] Nenhuma implementação encontrada para {interfaceType.Name}");
                continue;
            }
            services.AddScoped(interfaceType, implementationType);
            logger.LogInformation($"[DI] Registrando: {interfaceType}, {implementationType}");
        }
    }
}
