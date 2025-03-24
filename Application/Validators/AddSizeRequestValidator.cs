using Application.Models.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Repositories;
using Application.Extensions;

namespace Application.Validators {
    public class AddSizeRequestValidator : AbstractValidator<AddSizeRequest>{
        private readonly SizeRepository _sizeRepository;
        private readonly UserRepository _userRepository;

        public AddSizeRequestValidator(UserRepository userRepository, SizeRepository sizeRepository) {
            this._userRepository = userRepository;
            this._sizeRepository = sizeRepository;

            RuleFor(x => x.SizeValue)
                .NotEmpty()
                .WithMessage("Name is required");
            RuleFor(x => x.SizeValue)
                .MaximumLength(10)
                .WithMessage("Name must not exceed 50 characters")
                .Custom(BeUniqueSize);
            RuleFor(x => x.AddedBy)
                .NotEmpty()
                .WithMessage("User is required")
                .MustBeAdmin(_userRepository);
        }
        private void BeUniqueSize (string value, ValidationContext<AddSizeRequest> context) {
            var size = _sizeRepository.GetByName(value).Result;
            if (size != null) {
                context.AddFailure("Size already exists");
            }
        }
    }
}
