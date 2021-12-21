using System;

namespace DiContainer.DependencyInjection
{
    public class CycleDependencyException : Exception
    {
        public CycleDependencyException() : base() {}
        
        public CycleDependencyException(string message) : base(message) {}
        
        public CycleDependencyException(string message, Exception inner) : base(message, inner) {}
    }
}