using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using FluentValidation;
using Model.Repositories;

namespace Application.Validators {
    public class DeleteBrandRequestValidator : AbstractValidator<DeleteBrandRequest> {
        private readonly BrandRepository _brandRepository;
        private readonly ProductRepository _productRepository;
        public DeleteBrandRequestValidator (BrandRepository _brandRepository, ProductRepository _productRepository) {
            this._brandRepository = _brandRepository;
            this._productRepository = _productRepository;
            RuleFor(x => x.BrandId)
                .NotEmpty()
                .WithMessage("Brand is required")
                .Custom(BrandNotReachable);
        }
        private void BrandNotReachable(int id, ValidationContext<DeleteBrandRequest> context) {
            var brand = _brandRepository.GetAsync(id).Result;
            if (brand == null) {
                context.AddFailure("Brand with id " + id + " does not exist");
            }

            if (_productRepository.GetByBrandId(id).Result.Count>0) {
                context.AddFailure("Brand is still used in product");
            }
        }
    }
}
