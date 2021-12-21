using System;
using DiContainer.DependencyInjection;
using DiContainer.Services;


namespace DiContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new DiServiceCollection();

            services.RegisterSingleton<IA, A>();
            services.RegisterTransient<IB, B>();

            var container = services.GenerateContainer();

            var singletonFirst = container.GetService<IA>();
            var singletonSecond = container.GetService<IA>();

            var transientFirst = container.GetService<IB>();
            var transientSecond = container.GetService<IB>();
        }
    }
}