using System;

namespace DiContainer.Services
{
    public interface IRandomGuidProvider
    {
        Guid RandomGuid { get; }
    }
}