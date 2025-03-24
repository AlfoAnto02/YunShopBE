using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Models.Request;
using FluentValidation;
using Model.Repositories;

namespace Application.Validators {
    public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest> {
        private readonly ProductRepository _productRepository;
        private readonly UserRepository _userRepository;

        public DeleteProductRequestValidator(ProductRepository productRepository, UserRepository userRepository) {
            this._productRepository = productRepository;
            this._userRepository = userRepository;

            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("ProductId is required")
                .Custom(ProductNotReachable);
            RuleFor(x => x.DeletedBy)
                .NotEmpty()
                .WithMessage("AddedBy is required")
                .MustBeAdmin(_userRepository);
        }

        private void ProductNotReachable(int productId, ValidationContext<DeleteProductRequest> context) {
            var product = _productRepository.GetAsync(productId).Result;
            if (product.Id==0) {
                context.AddFailure("Product with id " + productId + " does not exist");
            }
        }
    }
}
