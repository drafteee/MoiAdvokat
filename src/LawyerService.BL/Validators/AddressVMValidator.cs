﻿using FluentValidation;
using LawyerService.ViewModel.Address;

namespace LawyerService.BL.Validators
{
    public class AddressVMValidator : AbstractValidator<AddressVM>
    {
        public AddressVMValidator()
        {
        }
    }
}
