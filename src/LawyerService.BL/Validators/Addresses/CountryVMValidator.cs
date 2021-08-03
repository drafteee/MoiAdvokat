using FluentValidation;
using LawyerService.ViewModel.Address;

namespace LawyerService.BL.Validators.Addresses
{
    public class CountryVMValidator : AbstractValidator<CountryVM>
    {
        public CountryVMValidator() { }
    }
}
