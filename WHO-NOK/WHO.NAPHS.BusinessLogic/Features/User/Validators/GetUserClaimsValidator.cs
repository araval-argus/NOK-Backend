// <copyright file="GetUserClaimsValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

#pragma warning disable CS8618

namespace WHO.NOK.BusinessLogic.Features.User.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="GetUserCommand"/>.
    /// </summary>
    public class GetUserClaimsValidator : AbstractValidator<GetUserClaimsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserClaimsValidator"/> class.
        /// </summary>
        public GetUserClaimsValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserClaimsValidator"/> class.
        /// </summary>
        /// <param name="localizer">Localizer.</param>
        /// <param name="commonService"> <see cref="ICommonService"/> Common service.</param>
        public GetUserClaimsValidator(IStringLocalizer<Resources> localizer, ICommonService commonService)
        {
            this.RuleFor(x => x.User).Must((x, user) => commonService.CheckLoggedInUserAuthorizationAsync(x.User));
        }
    }
}
