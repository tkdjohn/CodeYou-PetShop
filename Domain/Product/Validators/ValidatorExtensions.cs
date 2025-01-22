using FluentValidation;
using FluentValidation.Results;

namespace Domain.Product.Validators {
    public static class ValidatorExtensions {
        public static ValidationResult Validate(this DogLeash dogLeash) {
            return new DogLeashValidator().Validate(dogLeash);
        }

        public static ValidationResult Validate(this CatFood catFood) {
            return new CatFoodValidator().Validate(catFood);
        }

        public static ValidationResult Validate(this Product product) {
            return new ProductValidator().Validate(product);
        }
        public static ValidationResult Validate<T>(this Product product) {
            // really this is a bit awkward because we'll have to add code for every type of product
            // but... THAT is a much larger issue stemming form the fact that DogLeash and CatFood
            // aren't great objects (They are too specific/concrete. DurableProduct and PershibleProduct
            // might be better.
            if (product is DogLeash dogleash) {
                return dogleash.Validate();
            }

            if (product is CatFood catfood) {
                return catfood.Validate();
            }

            if (product is not null) {
                return product.Validate();
            }
            return new ValidationResult([new ValidationFailure("T", "Unknown type of Product.")]);
        }

        public static void ValidateAndThrow(this DogLeash dogLeash) {
            new DogLeashValidator().ValidateAndThrow(dogLeash);
        }

        public static void ValidateAndThrow(this CatFood catFood) {
            new CatFoodValidator().ValidateAndThrow(catFood);
        }

        public static void ValidateAndThrow(this Product product) {
            new ProductValidator().ValidateAndThrow(product);
        }

        public static void ValidateAndThrow<T>(this Product product) where T : Product {           
            // really this is a bit awkward because we'll have to add code for every type of product
            // but... THAT is a much larger issue stemming form the fact that DogLeash and CatFood
            // aren't great objects (They are too specific/concrete. DurableProduct and PershibleProduct
            // might be better.
            if (product is DogLeash dogleash) {
                dogleash.ValidateAndThrow();
            }

            if (product is CatFood catfood) { 
                catfood.ValidateAndThrow(); 
            }

            if (product is not null) {
                product.ValidateAndThrow();
            }
        }
    }
}
