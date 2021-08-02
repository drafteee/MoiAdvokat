using FluentValidation;
using LawyerService.ViewModel;

namespace LawyerService.BL.Validators
{
    public class LawyerVMValidator : AbstractValidator<LawyerVM>
    {
        public LawyerVMValidator()
        {
        }
    }
}
