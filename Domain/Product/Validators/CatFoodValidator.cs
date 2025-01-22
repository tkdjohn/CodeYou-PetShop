using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Domain.Product.Validators {
    public class CatFoodValidator :AbstractValidator<CatFood> {
        public CatFoodValidator() {
            Include(new ProductValidator());
            RuleFor(c => c.WeightPounds)
                .GreaterThan(0)
                .WithMessage("Weight must be greater than zero.");
        }
    }
}
