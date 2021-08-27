using FluentValidation;
using LawyerService.ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.BL.Validators.Orders
{
    public class OrderResponseVMValidator : AbstractValidator<OrderResponseVM>
    {
        public OrderResponseVMValidator() { }
    }
}
