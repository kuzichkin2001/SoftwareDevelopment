using System;

namespace DiContainer.Services
{
    public class B : IB
    {
        public B(IA a) {}

        public void newPrint()
        {
            Console.WriteLine("B : IB");
        }
    }
}