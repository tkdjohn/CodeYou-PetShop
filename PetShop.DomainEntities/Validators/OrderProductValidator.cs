using FluentValidation;
using PetShop.DomainEntities;

namespace PetShop.DomainEntities.Validators {
    public class OrderProductValidator : AbstractValidator<OrderProduct> {
        public OrderProductValidator() {
            RuleFor(o => o.OrderQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Order Quantity must be greater than zero.");
            RuleFor(o => o.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit Price must be greater than Zero.");
        }
    }
}
