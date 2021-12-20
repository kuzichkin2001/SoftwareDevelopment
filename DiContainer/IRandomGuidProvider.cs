using System;

namespace DiContainer
{
    public interface IRandomGuidProvider
    {
        Guid RandomGuid { get; }
    }
}