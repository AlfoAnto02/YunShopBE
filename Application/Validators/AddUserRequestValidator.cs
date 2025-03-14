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
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest> {
        private readonly UserRepository _userRepository;
        public AddUserRequestValidator(UserRepository userRepository) {
            this._userRepository= userRepository;

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username is required");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .RegEx("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", "Email insert with wrong format");
            RuleFor(x => x.Email)
                .Custom(AlreadyRegisteredRule);
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .RegEx("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]).{8,}$", "Password isn't strong ");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required");
        }

        private void AlreadyRegisteredRule(string value, ValidationContext<AddUserRequest> context) {
            var user = _userRepository.GetByEmail(value).Result;
            if (user.Email==value) {
                context.AddFailure("Email already registered");
            }
        }
    }
}
