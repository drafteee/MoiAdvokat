using FluentValidation;
using LawyerService.ViewModel.Lawyers;

namespace LawyerService.BL.Validators.Lawyers
{
    public class LawyerVMValidator : AbstractValidator<LawyerVM>
    {
        public LawyerVMValidator()
        {
        }
    }
}
