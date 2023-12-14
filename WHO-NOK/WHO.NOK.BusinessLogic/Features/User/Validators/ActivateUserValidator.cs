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

    /// <summary>
    /// Fluent validator for <see cref="ActivateUserCommand"/>.
    /// </summary>
    public class ActivateUserValidator : AbstractValidator<ActivateUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateUserValidator"/> class.
        /// </summary>
        public ActivateUserValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateUserValidator"/> class.
        /// </summary>
        /// <param name="commonService">Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer">Localizer.</param>
        public ActivateUserValidator(IStringLocalizer<Resources> localizer)
        {
            // this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.UserId).GreaterThan(0).WithMessage(localizer["UserIdValidation"]);
            // this.RuleFor(x => x).MustAsync(async (x, context) =>
            // {
            //     var isAuthorized = await commonService.IsUserAuthorizedAsync(
            //         (int)x.User?.UserId!,
            //         ActivityTypes.ActivateOrDeactivateUser,
            //         userId: x.UserId);

            //     return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException();
            // });
        }
    }
}
