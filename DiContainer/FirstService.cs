using System;

namespace DiContainer
{
    public class FirstService : IService
    {
        private readonly IRandomGuidProvider _randomGuidProvider;

        public FirstService(IRandomGuidProvider randomGuidProvider)
        {
            _randomGuidProvider = randomGuidProvider;
        }

        public void PrintSomething()
        {
            Console.WriteLine(_randomGuidProvider.RandomGuid);
        }
    }
}