// <copyright file="GetUserValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.User.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="GetUserCommand"/>.
    /// </summary>
    public class GetUserValidator : AbstractValidator<GetUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserValidator"/> class.
        /// </summary>
        public GetUserValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserValidator"/> class.
        /// </summary>
        /// <param name="localizer">Localizer.</param>
        public GetUserValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.UserId).GreaterThan(0).WithMessage(localizer["UserIdValidation"]);

            // TODO: Add in resx file.
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
        }
    }
}
