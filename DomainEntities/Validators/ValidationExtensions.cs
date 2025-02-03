using DomainEntities.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace DomainEntities.Validators {
    public static class ValidatorExtensions {

        public static ValidationResult Validate(this Product product) {
            return new ProductValidator().Validate(product);
        }

        public static ValidationResult Validate(this Order order) {
            return new OrderValidator().Validate(order);
        }
        public static ValidationResult Validate(this OrderProduct orderProduct) {
            return new OrderProductValidator().Validate(orderProduct);
        }

        public static ValidationResult Validate<T>(this BaseEntity entity) where T : BaseEntity {
            // really this is a bit awkward because we'll have to add code for every type of product
            // but... THAT is a much larger issue stemming form the fact that DogLeash and CatFood
            // aren't great objects (They are too specific/concrete. DurableProduct and PershibleProduct
            // might be better.
            if (entity is Product product) {
                return product.Validate();
            }

            if (entity is Order order) {
                return order.Validate();
            }

            if (entity is OrderProduct orderProduct) {
                return orderProduct.Validate();
            }

            return new ValidationResult([new ValidationFailure("T", "Unknown type of Product.")]);
        }


        public static void ValidateAndThrow(this Product product) {
            new ProductValidator().ValidateAndThrow(product);
        }

        public static void ValidateAndThrow(this Order order) {
            new OrderValidator().ValidateAndThrow(order);
        }


        public static void ValidateAndThrow(this OrderProduct orderProduct) {
            new OrderProductValidator().ValidateAndThrow(orderProduct);
        }

        public static void ValidateAndThrow<T>(this BaseEntity entity) where T : BaseEntity {
            // really this is a bit awkward because we'll have to add code for every type of product
            // but... THAT is a much larger issue stemming form the fact that DogLeash and CatFood
            // aren't great objects (They are too specific/concrete. DurableProduct and PershibleProduct
            // might be better.
            if (entity is Product product) {
                product.ValidateAndThrow();
            }

            if (entity is Order order) {
                order.ValidateAndThrow();
            }

            if (entity is OrderProduct orderProduct) {
                orderProduct.ValidateAndThrow();
            }

            throw new ValidationException([new ValidationFailure("T", "Unknown type of Product.")]);
        }

    }
}
