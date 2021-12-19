using System;

namespace DiContainer.DependencyInjection
{
    public class ServiceDescriptor
    {
        public Type ServiceType { get; private set; }

        public Type ImplementationType { get; private set; }

        public object Implementation { get; internal set; }

        public ServiceLifetime LifeTime { get; private set; }

        public ServiceDescriptor(object implementation, ServiceLifetime lifetime)
        {
            ServiceType = implementation.GetType();
            Implementation = implementation;
            LifeTime = lifetime;
        }
        
        public ServiceDescriptor(Type serviceType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            LifeTime = lifetime;
        }

        public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            LifeTime = lifetime;
        }
    }
}