// <copyright file="DeleteUserValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.User.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for delete user command.
    /// </summary>
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserValidator"/> class.
        /// </summary>
        public DeleteUserValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserValidator"/> class.
        /// </summary>
        /// <param name="localizer"><see cref="StringLocalizer"/> localizer.</param>
        public DeleteUserValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.UserId).GreaterThan(0).WithMessage(localizer["UserIdValidation"]);
        }
    }
}