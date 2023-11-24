// <copyright file="UpdateUserDetailsValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace Namespace
{
    using FluentValidation;

    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.Features.User;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="UpdateUserDetailsCommand"/> class.
    /// </summary>
    public class UpdateUserDetailsValidator : AbstractValidator<UpdateUserDetailsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserDetailsValidator"/> class.
        /// Initializes a new instance of the <see cref="UpdateUserDetailsValidator"/> class.
        /// </summary>
        public UpdateUserDetailsValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserDetailsValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer"> Localizer.</param>
        public UpdateUserDetailsValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage(localizer["UserIdValidation"]);
            this.RuleFor(x => x.FirstName).NotEmpty().WithMessage(localizer["FirstNameValidation"]);
            this.RuleFor(x => x.LastName).NotEmpty().WithMessage(localizer["LastNameValidation"]);
            this.RuleFor(x => x.RoleId).NotNull().NotEmpty().IsInEnum().WithMessage(localizer["RoleValidation"]);
            this.RuleFor(x => x.PreferredLanguageId).NotNull().NotEmpty().IsInEnum().WithMessage(localizer["LanguageNotSelected"]);
            this.RuleFor(x => x).MustAsync(async (x, context) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    (int)x.User?.UserId!,
                    ActivityTypes.UpdateUserAccount,
                    userId: x.UserId);

                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException();
            });
        }
    }
}
