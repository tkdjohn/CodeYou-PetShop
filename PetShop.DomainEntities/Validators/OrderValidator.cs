using FluentValidation;
using PetShop.DomainEntities;

namespace PetShop.DomainEntities.Validators {
    public class OrderValidator : AbstractValidator<Order> {
        public OrderValidator() {
            RuleFor(o => o.OrderProducts)
                .NotEmpty()
                .WithMessage("An order must have at least one product.");
            RuleFor(o => o.OrderDate)
                .GreaterThanOrEqualTo(DateTime.Now.AddDays(-7))
                .WithMessage("Orders cannot be older than 1 week.");
        }
    }
}
