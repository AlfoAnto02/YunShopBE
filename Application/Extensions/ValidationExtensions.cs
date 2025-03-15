using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Repositories;

namespace Application.Extensions {
    public static class ValidationExtensions
    {
        public static void RegEx<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, string regex,
            string message)
        {
            ruleBuilder.Custom((value, context) =>
            {
                var _regex = new System.Text.RegularExpressions.Regex(regex);
                if (_regex.IsMatch(value.ToString()) == false)
                {
                    context.AddFailure(message);
                }
            });
        }

        public static IRuleBuilderOptions<T, string> MustBeUniqueName<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            Func<string, bool> isUniqueFunc) {
            return ruleBuilder
                .Must(name => isUniqueFunc(name))
                .WithMessage("Name already exists");
        }

        public static IRuleBuilderOptions<T, int> MustBeAdmin<T>(
            this IRuleBuilder<T, int> ruleBuilder,
            UserRepository userRepository) {
            return (IRuleBuilderOptions<T, int>)ruleBuilder.Custom((userId, context) => {
                var user = userRepository.GetAsync(userId).Result;
                if (user.Role != "Admin") {
                    context.AddFailure("UnAuthorized");
                }
            });
        }
    }
}
