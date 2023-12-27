// <copyright file="UserViewModelValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.User
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="UserViewModel"/> .
    /// </summary>
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModelValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public UserViewModelValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.FirstName).NotEmpty().WithMessage(localizer["FirstNameValidation"]);
            this.RuleFor(x => x.LastName).NotEmpty().WithMessage(localizer["LastNameValidation"]);
            this.RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage(localizer["EmailValidation"]);
            this.RuleFor(x => x.RoleId).NotNull().WithMessage(localizer["RoleValidation"]);
        }
    }
}