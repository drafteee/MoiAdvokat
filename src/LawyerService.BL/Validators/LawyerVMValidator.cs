using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
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
