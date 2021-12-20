using System;
using DiContainer.DependencyInjection;


namespace DiContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new DiServiceCollection();

            services.RegisterSingleton<IRandomGuidProvider, RandomGuidProvider>();
            services.RegisterTransient<IService, FirstService>();

            var container = services.GenerateContainer();

            var singletonFirst = container.GetService<IRandomGuidProvider>();
            var singletonSecond = container.GetService<IRandomGuidProvider>();

            var transientFirst = container.GetService<IService>();
            var transientSecond = container.GetService<IService>();

            Console.WriteLine("Singletons:");
            Console.WriteLine(singletonFirst.RandomGuid);
            Console.WriteLine(singletonSecond.RandomGuid);

            Console.WriteLine("\nTransients:");
            transientFirst.PrintSomething();
            transientSecond.PrintSomething();
        }
    }
}