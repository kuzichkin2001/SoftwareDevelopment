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
            services.RegisterSingleton<IRandomGuidProvider, RandomGuidProvider>();
            services.RegisterTransient<ISomeService, FirstService>();

            var container = services.GenerateContainer();

            var singletonFirst = container.GetService<IA>();
            var singletonSecond = container.GetService<IA>();

            var transientFirst = container.GetService<IB>();
            var transientSecond = container.GetService<IB>();

            var singletonThird = container.GetService<IRandomGuidProvider>();
            var singletonFourth = container.GetService<IRandomGuidProvider>();

            var transientThird = container.GetService<ISomeService>();
            var transientFourth = container.GetService<ISomeService>();

            Console.WriteLine(singletonThird.RandomGuid);
            Console.WriteLine(singletonFourth.RandomGuid);

            transientThird.Print();
            transientFourth.Print();
        }
    }
}