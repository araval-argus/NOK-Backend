// <copyright file="CreateUserValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.User.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Common.Resources;
    using WHO.NOK.Core.Constant;

    /// <summary>
    /// Fluent validator for <see cref="CreateUserCommand"/>.
    /// </summary>
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserValidator"/> class.
        /// </summary>
        public CreateUserValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserValidator"/> class.
        /// </summary>
        /// <param name="commonService">Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer">Localizer.</param>
        public CreateUserValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.FirstName).NotEmpty().NotNull().Matches(Values.NameRegex).WithMessage(localizer["FirstNameValidation"]);
            this.RuleFor(x => x.LastName).NotEmpty().NotNull().Matches(Values.NameRegex).WithMessage(localizer["LastNameValidation"]);
            this.When(x => x.RoleId == Roles.SystemAdmin || x.RoleId == Roles.RegionalAdmin, () =>
            {
                this.RuleFor(x => x.Email).Matches(Values.WhoEmailDomainRegex).WithMessage(localizer["WhoEmailDomainValidation"]);
            })
            .Otherwise(() =>
            {
                this.RuleFor(x => x.Email).Matches(Values.EmailRegex).WithMessage(localizer["EmailValidation"]);
            });
            this.RuleFor(x => x.RoleId).NotNull().IsInEnum().WithMessage(localizer["RoleValidation"]);
            this.RuleFor(x => x.PreferredLanguageId).NotNull().NotEmpty().WithMessage(localizer["LanguageNotSelected"]);
            this.RuleFor(x => x).MustAsync(async (x, context) =>
            {
                bool isAuthorized = false;
                if (x.RoleId == Roles.RegionalAdmin)
                {
                    isAuthorized = await commonService.IsUserAuthorizedAsync(
                        (int)x.User?.UserId!,
                        ActivityTypes.InviteUser,
                        region: x.Region);
                }
                else
                {
                    isAuthorized = await commonService.IsUserAuthorizedAsync(
                        (int)x.User?.UserId!,
                        ActivityTypes.InviteUser,
                        countryId: x.Country?.CountryId);
                }

                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException();
            });
        }
    }
}
