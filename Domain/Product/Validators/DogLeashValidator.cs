using FluentValidation;

namespace Domain.Product.Validators {
    public class DogLeashValidator : AbstractValidator<DogLeash> {
        public DogLeashValidator() {
            RuleFor(dl => dl.Name)
                .NotEmpty()
                .WithMessage("Product name cannot be empty");
            RuleFor(dl => dl.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");
            RuleFor(dl => dl.Qty)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity cannot be a negative value.");
            RuleFor(dl => dl.Description.Length)
                .GreaterThanOrEqualTo(10)
                .WithMessage("Description must be at least 10 characters.")
                .When(dl => dl.Description != null);
           
        }
    }
}
