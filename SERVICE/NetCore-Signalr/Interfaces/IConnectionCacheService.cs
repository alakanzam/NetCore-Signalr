using SignalrCore.Models;

namespace SignalrCore.Interfaces
{
    public interface IConnectionCacheService : IValueCacheService<string, User>
    {
        
    }
}