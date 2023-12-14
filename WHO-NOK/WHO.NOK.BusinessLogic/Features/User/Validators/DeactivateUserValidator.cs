// <copyright file="DeactivateUserValidator.cs" company="WHO">
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
    /// Fluent validator for <see cref="DeactivateUserCommand"/>.
    /// </summary>
    public class DeactivateUserValidator : AbstractValidator<DeactivateUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeactivateUserValidator"/> class.
        /// </summary>
        public DeactivateUserValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeactivateUserValidator"/> class.
        /// </summary>
        /// <param name="commonService">Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer">Localizer.</param>
        public DeactivateUserValidator(IStringLocalizer<Resources> localizer)
        {
            // this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.UserId).GreaterThan(0).WithMessage(localizer["UserIdValidation"]);
        }
    }
}
