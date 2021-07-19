using Microsoft.AspNetCore.Authorization;

namespace LawyerService.API
{
    public class RegisterByUserRequirement : IAuthorizationRequirement
    {
        public RegisterByUserRequirement()
        {
        }
    }
}
