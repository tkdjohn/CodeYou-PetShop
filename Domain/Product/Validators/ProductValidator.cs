using FluentValidation;

namespace Domain.Product.Validators {
    public class ProductValidator : AbstractValidator<IProduct> {
        public ProductValidator() {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Product name cannot be empty");
            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");
            RuleFor(p => p.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity cannot be a negative value.");
            RuleFor(p => p.Description.Length)
                .GreaterThanOrEqualTo(10)
                .When(p => !string.IsNullOrEmpty(p.Description))
                .WithMessage("Description must be at least 10 characters.");
        }
    }
}
