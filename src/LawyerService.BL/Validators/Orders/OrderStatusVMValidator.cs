using FluentValidation;
using LawyerService.ViewModel.Orders;

namespace LawyerService.BL.Validators.Orders
{
    public class OrderStatusVMValidator : AbstractValidator<OrderStatusVM>
    {
        public OrderStatusVMValidator() { }
    }
}
