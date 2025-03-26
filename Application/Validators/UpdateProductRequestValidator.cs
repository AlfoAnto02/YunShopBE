using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Model.Repositories;

namespace Application.Validators {
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest> {
        private readonly ProductRepository _productRepository;

        public UpdateProductRequestValidator(ProductRepository productRepository)
        {
            this._productRepository = productRepository;
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Id is required")
                .Custom(FoundProduct);
        }

        private void FoundProduct(int id, ValidationContext<UpdateProductRequest> context) {
            var product = _productRepository.GetAsync(id).Result;
            if (product.Name.IsNullOrEmpty()) {
                context.AddFailure("Product not found");
            }
        }
    }
}
