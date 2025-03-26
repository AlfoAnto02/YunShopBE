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
    public class AddCategoryRequestValidator : AbstractValidator<AddCategoryRequest> {
        private readonly CategoryRepository _categoryRepository;
        private readonly UserRepository _userRepository;

        public AddCategoryRequestValidator(CategoryRepository categoryRepository, UserRepository userRepository) {
            this._categoryRepository = categoryRepository;
            this._userRepository = userRepository;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");
            RuleFor(x => x.Name)
                .MaximumLength(50)
                .WithMessage("Name must not exceed 50 characters")
                .MustBeUniqueName(name =>
                {
                    var category = _categoryRepository.GetByName(name).Result;
                    if (category == null) return true;
                    return category.Name != name;
                });
            RuleFor(x => x.AddedBy)
                .NotEmpty()
                .WithMessage("AddedBy is required")
                .MustBeAdmin(_userRepository);
        }
    }
}
