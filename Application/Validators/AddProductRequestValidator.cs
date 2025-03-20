using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Models.Request;
using FluentValidation;
using Model.Entities;
using Model.Repositories;

namespace Application.Validators {
    public class AddProductRequestValidator : AbstractValidator<AddProductsRequest> {
        private readonly SizeRepository _sizeRepository;
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly BrandRepository _brandRepository;
        private readonly UserRepository _userRepository;

        public AddProductRequestValidator(SizeRepository sizeRepository, ProductRepository productRepository, 
            CategoryRepository categoryRepository, BrandRepository brandRepository, UserRepository _repository) {
            _sizeRepository = sizeRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _userRepository = _repository;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(100)
                .WithMessage("Name can't be longer than 100 characters")
                .MustBeUniqueName(name => {
                    var product = _productRepository.GetByName(name).Result;
                    if (product == null) return true;
                    return product.Name != name;
                });

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(1000)
                .WithMessage("Description can't be longer than 1000 characters");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Category is required")
                .Custom(CategoryExists);

            RuleFor(x => x.BrandId)
                .NotEmpty()
                .WithMessage("Brand is required")
                .Custom(BrandExists);

            RuleFor(x => x.Sizes)
                .NotEmpty()
                .WithMessage("Sizes are required")
                .Custom(SizeExists);

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User is required")
                .MustBeAdmin(_userRepository);
        }

        private void CategoryExists(int id, ValidationContext<AddProductsRequest> context)
        {
            var category = _categoryRepository.GetAsync(id).Result;
            if (category == null)
            {
                context.AddFailure("Category doesn't exist");
            }
        }

        private void BrandExists(int id, ValidationContext<AddProductsRequest> context)
        {
            var brand = _brandRepository.GetAsync(id).Result;
            if (brand == null)
            {
                context.AddFailure("Brand doesn't exist");
            }
        }

        private void SizeExists(List<AddProductSizeRequest> productSizeRequests, ValidationContext<AddProductsRequest> context)
        {
            foreach (var productSize in productSizeRequests)
            {
                var size = _sizeRepository.GetAsync(productSize.SizeId).Result;
                if (size == null)
                {
                    context.AddFailure("Size doesn't exist");
                }
                if (productSize.Stock < 0)
                {
                    context.AddFailure("Stock must be greater than 0");
                }

                if (productSize.Price < 0)
                {
                    context.AddFailure("Price must be greater than 0");
                }
            }
        }
    }
}
