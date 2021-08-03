using FluentValidation;
using LawyerService.ViewModel.Address;

namespace LawyerService.BL.Validators.Addresses
{
    public class AddressVMValidator : AbstractValidator<AddressVM>
    {
        public AddressVMValidator()
        {
        }
    }
}
