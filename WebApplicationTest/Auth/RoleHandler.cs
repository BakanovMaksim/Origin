using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ApplicationOrigin.Auth
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        RoleRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            { 
                if (context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value == requirement.Role)  //TODO: Ошибка в запросе!
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
