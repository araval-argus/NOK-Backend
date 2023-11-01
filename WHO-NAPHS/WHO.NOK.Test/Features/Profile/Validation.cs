// <copyright file="Validation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WHO.NAPHS.API.Tests.Features.Profile
{
    using FluentValidation.TestHelper;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.API.Test.Fixture;
    using WHO.NAPHS.BusinessLogic.Features.User;
    using WHO.NAPHS.BusinessLogic.Features.User.Validators;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.Core.Common.Resources;
    using Xunit;

    /// <summary>
    /// Validation test for Profile Management.
    /// </summary>
    public class Validation : TestFixture
    {        
        private readonly IStringLocalizer<Resources> localizer;
        private readonly ICommonService commonService;

        public Validation()
        {
            this.localizer = this.GetService<IStringLocalizer<Resources>>().Result;
            this.commonService = this.GetService<ICommonService>().Result;
        }

        /// <summary>
        /// Success validation test for valid email address.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Theory]
        [InlineData("abc@gmail.com")]
        [InlineData("john.doe@example.com")]
        [InlineData("mary.smith@emailprovider.net")]
        [InlineData("info@company123.org")]
        [InlineData("user12345@gmail.com")]
        [InlineData("support@website.com")]
        [InlineData("jane.doe@email.co.uk")]
        [InlineData("sales@mycompany.biz")]
        [InlineData("contact_us@domain.name")]
        [InlineData("webmaster@example.org")]
        [InlineData("your.name123@service.io")]
        public void ValidationCreateUserCommand_Email_Success(string email)
        {
            CreateUserValidator validator = new (this.commonService, this.localizer);
            CreateUserCommand command = new ()
            {
                Email = email,
            };
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        /// <summary>
        /// Failure validation test case for invalid email address.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        // [InlineData(".@website.org")]
        // [InlineData("mary.smith@.com")]
        // [InlineData("user12345@.net")]
        [Theory]
        [InlineData("")]
        [InlineData("john.doe")]
        [InlineData("@example.com")]
        public void ValidationCreateUserCommand_Email_Failure(string email)
        {
            CreateUserValidator validator = new (this.commonService, this.localizer);
            CreateUserCommand command = new ()
            {
                Email = email,
            };
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        /// <summary>
        /// Success validation test for valid firstname and lastname.
        /// </summary>
        /// <param name="firstname">Firstname.</param>
        /// <param name="lastname">Lastname.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Theory]
        [InlineData("john", "doe")]
        public void ValidationCreateUserCommand_Firstname_Lastname_Success(string firstname, string lastname)
        {
            CreateUserValidator validator = new (this.commonService, this.localizer);
            CreateUserCommand command = new ()
            {
                FirstName = firstname,
                LastName = lastname,
            };
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
            result.ShouldNotHaveValidationErrorFor(x => x.LastName);
        }
    }
}