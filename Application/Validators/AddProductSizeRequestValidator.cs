using Application.Models.Request;
using FluentValidation;
using FluentValidation.Internal;
using Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class AddProductSizeRequestValidator : AbstractValidator<AddProductSizeRequest>
    {
        private readonly ProductSizeRepository productSizeRepository;
        private readonly SizeRepository sizeRepository;
        public AddProductSizeRequestValidator(ProductSizeRepository productSizeRepository, SizeRepository sizeRepository) {
            this.productSizeRepository = productSizeRepository;
            this.sizeRepository = sizeRepository;

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price must be inserted")
                .GreaterThan(0)
                .WithMessage("Can't be inserted a negative price");
            RuleFor(x => x.Stock)
                .NotEmpty()
                .WithMessage("Stock must be inserted")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Can't be inserted a negative stock");
            RuleFor(x => x.SizeId)
                .NotEmpty()
                .WithMessage("Size id must be inserted")
                .Custom(MustExist);
        }

        private void MustExist(int sizeid, ValidationContext<AddProductSizeRequest> context) {
            var size = sizeRepository.GetAsync(sizeid);
            if (size == null) {
                context.AddFailure("Size with id : " + sizeid + " doesn't exist");
            }
        }
    }
}
