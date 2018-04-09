using SignalrCore.Interfaces;
using SignalrCore.Models;

namespace SignalrCore.Services
{
    public class ConnectionCacheService: ValueCacheBaseService<string, User>, IConnectionCacheService
    {
        
    }
}