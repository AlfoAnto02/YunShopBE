using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using FluentValidation;
using Model.Repositories;

namespace Application.Validators {
    public class DeleteSizeRequestValidator : AbstractValidator<DeleteSizeRequest> {
        private readonly SizeRepository _sizeRepository;
        private readonly ProductSizeRepository _productSizeRepository;
        public DeleteSizeRequestValidator (SizeRepository sizeRepository, ProductSizeRepository productSizeRepository) {
            this._sizeRepository = sizeRepository;
            this._productSizeRepository = productSizeRepository;

            RuleFor(x => x.SizeId)
                .NotEmpty()
                .WithMessage("Size is required")
                .Custom(SizeNotReachable);
        }
        private void SizeNotReachable(int id, ValidationContext<DeleteSizeRequest> context) {
            var size = _sizeRepository.GetAsync(id).Result;
            if (size==null) {
                context.AddFailure("Size " + size.SizeValue + " does not exist");
            }
            if (_productSizeRepository.GetBySizeId(id).Result.Count > 0) {
                context.AddFailure("Size " + size.SizeValue + " is still used in product");
            }
        }
    }
}
