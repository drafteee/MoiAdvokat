using FluentValidation;
using LawyerService.ViewModel.Address;

namespace LawyerService.BL.Validators
{
    public class CountryVMValidator : AbstractValidator<CountryVM>
    {
        public CountryVMValidator() { }
    }
}
