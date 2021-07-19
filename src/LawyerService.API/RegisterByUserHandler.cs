using LawyerService.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class RegisterByUserHandler : AuthorizationHandler<RegisterByUserRequirement>
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public RegisterByUserHandler(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RegisterByUserRequirement requirement)
    {
        var isUser = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization");
        if (!isUser || context.User.Identity.IsAuthenticated)
            context.Succeed(requirement);
        else
            context.Fail();
        return Task.CompletedTask;
    }
}