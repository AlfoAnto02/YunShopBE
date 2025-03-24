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
    public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequest> {
        private readonly CategoryRepository _categoryRepository;
        private readonly UserRepository _userRepository;
        public DeleteCategoryRequestValidator(CategoryRepository categoryRepository, UserRepository _userRepository) {
            this._categoryRepository = categoryRepository;
            this._userRepository = _userRepository;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .Custom(CategoryNotReachable);
            RuleFor(x => x.DeletedBy)
                .NotEmpty()
                .WithMessage("AddedBy is required")
                .MustBeAdmin(_userRepository);
        }
        private void CategoryNotReachable(string name, ValidationContext<DeleteCategoryRequest> context) {
            var category = _categoryRepository.GetByName(name).Result;
            if (category.Name != name) {
                context.AddFailure("Category " + name + " does not exist");
            }
        }
    }
}
