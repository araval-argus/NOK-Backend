// <copyright file="ActivateUserValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.User.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.Core.Common.Resources;
    using WHO.NOK.Core.Constant;
    using WHO.NOKS.BusinessLogic.Features.User;

    /// <summary>
    /// Fluent validator for <see cref="UpdateUserCommand"/>.
    /// </summary>
    public class UpdateUserValidator : AbstractValidator<UpdateUserDetailsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserValidator"/> class.
        /// </summary>
        public UpdateUserValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserValidator"/> class.
        /// </summary>
        /// <param name="commonService">Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer">Localizer.</param>
        public UpdateUserValidator(IStringLocalizer<Resources> localizer)
        {
            // this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.UserId).GreaterThan(0).WithMessage(localizer["UserIdValidation"]);
            this.RuleFor(x => x.FirstName).NotEmpty().NotNull().Matches(Values.NameRegex).WithMessage(localizer["FirstNameValidation"]);
            this.RuleFor(x => x.LastName).NotEmpty().NotNull().Matches(Values.NameRegex).WithMessage(localizer["LastNameValidation"]);
            this.RuleFor(x => x.RoleId).NotNull().IsInEnum().WithMessage(localizer["RoleValidation"]);
            this.RuleFor(x => x.PreferredLanguageId).NotEmpty().WithMessage(localizer["LanguageNotSelected"]);
        }
    }
}
