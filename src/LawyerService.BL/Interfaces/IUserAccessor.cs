using System.Security.Claims;

namespace LawyerService.BL.Interfaces
{
    public interface IUserAccessor
    {
        string GetCurrentUsername();
    }
}
