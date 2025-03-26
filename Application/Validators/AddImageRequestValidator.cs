using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Models.Request;
using FluentValidation;

namespace Application.Validators {
    public class AddImageRequestValidator : AbstractValidator<AddImageRequest> {
        public AddImageRequestValidator() {
            RuleFor(x => x.Url)
                .NotEmpty()
                .WithMessage("Url is required")
                .RegEx("/^(https?:\\/\\/(?:www\\.)?[\\w\\-.]+\\.[a-z]{2,}(?:\\/[\\w\\-.~:?#\\[\\]@!$&'()*+,;=%]*)*\\.(?:jpg|jpeg|png|gif|bmp|svg)(?:\\?.*)?$/i;", "Image Format not valid");
        }
    }
}
