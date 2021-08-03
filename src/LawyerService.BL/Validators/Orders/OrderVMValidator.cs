using FluentValidation;
using LawyerService.ViewModel.Orders;

namespace LawyerService.BL.Validators.Orders
{
    public class OrderVMValidator : AbstractValidator<OrderVM>
    {
        public OrderVMValidator() { }
    }
}
