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
    public class AddBrandRequestValidator : AbstractValidator<AddBrandRequest> {
        private readonly BrandRepository _brandRepository;
        private readonly UserRepository _userRepository;

        public AddBrandRequestValidator(BrandRepository brandRepository, UserRepository userRepository) {
            this._brandRepository = brandRepository;
            this._userRepository = userRepository;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");
            RuleFor(x => x.Name)
                .MaximumLength(50)
                .WithMessage("Name must not exceed 50 characters")
                .MustBeUniqueName(async name =>
                {
                    var brand = await _brandRepository.GetByName(name);
                    return brand.Name != name;
                });
            RuleFor(x => x.AddedBy)
                .NotEmpty()
                .WithMessage("AddedBy is required")
                .MustBeAdmin(_userRepository);
        }
    }
}
