using FluentValidation;

namespace DomainEntities.Validators {
    public class CatFoodValidator : AbstractValidator<CatFood> {
        public CatFoodValidator() {
            Include(new ProductValidator());
            RuleFor(c => c.WeightPounds)
                .GreaterThan(0)
                .WithMessage("Weight must be greater than zero.");
        }
    }
}
