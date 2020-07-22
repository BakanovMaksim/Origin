using System;
using Microsoft.AspNetCore.Authorization;

namespace ApplicationOrigin.Auth
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        protected internal string Role { get; }

        public RoleRequirement(string role)
        {
            Role = role;
        }
    }
}
