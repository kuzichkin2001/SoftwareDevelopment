using System;

namespace DiContainer.Services
{
    public class A : IA
    {
        public A(IB b) {}
        
        public void print()
        {
            Console.WriteLine("A : IA");
        }
    }
}