using System;

namespace DiContainer.DependencyInjection
{
    public class ServiceDescriptor
    {
        public Type ServiceType { get; private set; }

        public Type ImplementationType { get; private set; }

        public object Implementation { get; internal set; }

        public ServiceLifetime LifeTime { get; private set; }

        public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            LifeTime = lifetime;
        }
    }
}