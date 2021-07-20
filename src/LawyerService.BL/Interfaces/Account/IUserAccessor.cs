using System.Security.Claims;

namespace LawyerService.BL.Interfaces.Account
{
    public interface IUserAccessor
    {
        string GetCurrentUsername();
    }
}
