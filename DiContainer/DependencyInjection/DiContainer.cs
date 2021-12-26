using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace DiContainer.DependencyInjection
{
    public class DiContainer
    {
        private List<ServiceDescriptor> _serviceDescriptors;
        public DiContainer(List<ServiceDescriptor> serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors;
        }

        public object GetService(Type serviceType)
        {
            List<Type> depsList = new List<Type>();

            return GetService(serviceType, ref depsList);
        }

        private object GetService(Type serviceType, ref List<Type> typesList)
        {
            var descriptor = _serviceDescriptors
                .SingleOrDefault(x => x.ServiceType == serviceType);

            if (descriptor == null)
            {
                throw new Exception($"Service of type {serviceType.Name} isn't registered");
            }

            if (descriptor.Implementation != null)
            {
                return descriptor.Implementation;
            }

            Type actualType = descriptor.ImplementationType ?? descriptor.ServiceType;

            if (actualType.IsAbstract || actualType.IsInterface)
            {
                throw new Exception("Cannot instantiate abstract or interface type");
            }

            var constructorInfo = actualType.GetConstructors().First();

            foreach (Type item in typesList)
            {
                Console.Write($"{item.Name} -> ");
            }

            Console.WriteLine();
            
            var parameters = constructorInfo.GetParameters();
            List<object?> newParameters = new List<object?>();
            foreach (var parameter in parameters)
            {
                if (typesList.Contains(serviceType))
                {
                    throw new CycleDependencyException($"The type {serviceType.Name} is already referenced. " +
                                                       $"Found cycle reference.");
                }
                typesList.Add(serviceType);
                var newParameter = GetService(parameter.ParameterType, ref typesList);
                newParameters.Add(newParameter);
            }

            var resultParams = newParameters.ToArray();

            var implementation = Activator.CreateInstance(actualType, resultParams);

            if (descriptor.LifeTime == ServiceLifetime.Singleton)
            {
                descriptor.Implementation = implementation;
            }

            return implementation;
        }

        public TService GetService<TService>()
        {
            return (TService) (GetService(typeof(TService)));
        }
    }
}