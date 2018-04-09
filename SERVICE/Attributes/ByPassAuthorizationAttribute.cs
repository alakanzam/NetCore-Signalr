using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace SignalrCore.Attributes
{
    public class ByPassAuthorizationAttribute : AuthorizeAttribute
    {
        
    }
}