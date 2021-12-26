using System;

namespace DiContainer.Services
{
    public class FirstService : ISomeService
    {
        private readonly IRandomGuidProvider _randomGuidProvider;

        public FirstService(IRandomGuidProvider randomGuidProvider)
        {
            _randomGuidProvider = randomGuidProvider;
        }

        public void Print()
        {
            Console.WriteLine(_randomGuidProvider.RandomGuid);
        }
    }
}