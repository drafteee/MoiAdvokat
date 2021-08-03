using FluentValidation;
using LawyerService.ViewModel.Lawyers;

namespace LawyerService.BL.Validators
{
    public class LawyerVMValidator : AbstractValidator<LawyerVM>
    {
        public LawyerVMValidator()
        {
        }
    }
}
