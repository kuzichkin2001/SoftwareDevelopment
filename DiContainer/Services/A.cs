using System;

namespace DiContainer.Services
{
    public class A : IA
    {
        public A(IA a)
        {
            
        }
        public void print()
        {
            Console.WriteLine("A : IA");
        }
    }
}