using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DependencyResolver
    {
        public static IServiceCollection RegisterAssemblies(this IServiceCollection serviceCollection, DependencyLifetime lifetime, string[] assemblies, params Type[] markerInterfaces)
        {
            foreach (var asmName in assemblies)
            {
                var asm = Assembly.Load(new AssemblyName(asmName));
                var types = asm.GetTypes()
                    .Where(x => {
                        var ti = x.GetTypeInfo();
                        var propertiesMatched = ti.IsClass && !ti.IsAbstract && !ti.IsGenericType;
                        var interfacesMatched = ti.ImplementedInterfaces.Select(i => i.Name).Intersect(markerInterfaces.Select(m => m.Name)).Any();

                        return propertiesMatched && interfacesMatched;
                    })
                    .ToList();
                foreach (var type in types)
                {
                    var interfaces = type.GetTypeInfo().ImplementedInterfaces.ToList();
                    foreach (var i in interfaces)
                        Register(serviceCollection, lifetime, i, type);
                }
            }

            return serviceCollection;
        }

        private static void Register(IServiceCollection serviceCollection, DependencyLifetime lifetime, Type interfaceType, Type concreteType)
        {
            switch (lifetime)
            {
                case DependencyLifetime.Transient:
                    serviceCollection.AddTransient(interfaceType, concreteType);
                    break;
                case DependencyLifetime.Scoped:
                    serviceCollection.AddScoped(interfaceType, concreteType);
                    break;
                case DependencyLifetime.Singleton:
                    serviceCollection.AddSingleton(interfaceType, concreteType);
                    break;
            }
        }
    }
}
