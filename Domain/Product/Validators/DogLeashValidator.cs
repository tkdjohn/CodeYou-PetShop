using FluentValidation;

namespace Domain.Product.Validators {
    public class DogLeashValidator : AbstractValidator<DogLeash> {
        public DogLeashValidator() {
            Include(new ProductValidator());
            RuleFor(dl => dl.LengthInches)
                .GreaterThan(0)
                .WithMessage("Length must be greater than zero.");
        }
    }
}
