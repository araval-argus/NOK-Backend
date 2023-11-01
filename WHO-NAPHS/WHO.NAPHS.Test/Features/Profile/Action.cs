// <copyright file="Action.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WHO.NAPHS.API.Tests.Features.Profile
{
    using FluentValidation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using NSubstitute;
    using Shouldly;
    using WHO.NAPHS.API.Controllers;
    using WHO.NAPHS.API.Test.Fixture;
    using WHO.NAPHS.BusinessLogic.Features.User;
    using WHO.NAPHS.BusinessLogic.Helper;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.BusinessLogic.ViewModels.Country;
    using WHO.NAPHS.BusinessLogic.ViewModels.PaginatedResponses;
    using WHO.NAPHS.BusinessLogic.ViewModels.User;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.Common.Resources;
    using Xunit;

    /// <summary>
    /// Action test for Profile Management.
    /// </summary>
    public class Action : TestFixture
    {
        private readonly IUserService userService;
        private readonly IStringLocalizer<Resources> localizer;
        private readonly IValidator<CreateUserCommand> createUserValidator;
        private readonly IValidator<UpdateUserDetailsCommand> updateUserDetailsValidator;
        private readonly IValidator<GetUserCommand> getUserValidator;
        private readonly IValidator<ActivateUserCommand> activateUserValidator;
        private readonly IValidator<DeactivateUserCommand> deactivateUserCommandValidator;
        private readonly IValidator<UploadUserProfilePicCommand> uploadProfilePicValidator;
        private readonly IValidator<GetProfilePictureCommand> profilePictureValidator;
        private readonly IValidator<UpdateMyProfileCommand> updateMyProfileValidator;
        private readonly IValidator<GetUserClaimsCommand> getUserClaimsValidator;
        private readonly IValidator<DeleteUserCommand> deleteUserValidator;
        private IHttpContextAccessor contextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Action"/> class.
        /// </summary>
        public Action()
        {
            this.userService = Substitute.For<IUserService>();
            this.localizer = Substitute.For<IStringLocalizer<Resources>>();
            this.createUserValidator = Substitute.For<IValidator<CreateUserCommand>>();
            this.updateUserDetailsValidator = Substitute.For<IValidator<UpdateUserDetailsCommand>>();
            this.activateUserValidator = Substitute.For<IValidator<ActivateUserCommand>>();
            this.deactivateUserCommandValidator = Substitute.For<IValidator<DeactivateUserCommand>>();
            this.getUserValidator = Substitute.For<IValidator<GetUserCommand>>();
            this.contextAccessor = Substitute.For<HttpContextAccessor>();
            this.uploadProfilePicValidator = Substitute.For<IValidator<UploadUserProfilePicCommand>>();
            this.profilePictureValidator = Substitute.For<IValidator<GetProfilePictureCommand>>();
            this.updateMyProfileValidator = Substitute.For<IValidator<UpdateMyProfileCommand>>();
            this.getUserClaimsValidator = Substitute.For<IValidator<GetUserClaimsCommand>>();
            this.deleteUserValidator = Substitute.For<IValidator<DeleteUserCommand>>();
        }

        /// <summary>
        /// Test for checking the database context is accessible or not.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task TestForDatabaseContext()
        {
            // Ensuring that database connection established successfully.
            var user = await this.ExecuteDbContextAsync(context => context.Users.ToListAsync());
        }

        /// <summary>
        /// Test for GetUser method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetUser_ReturnsUserDetailsSuccess()
        {
            // Arrange
            this.contextAccessor = await this.GetService<IHttpContextAccessor>();
            this.contextAccessor.HttpContext = new DefaultHttpContext()
            {
                User = this.GetUserClaims(Roles.SystemAdmin, false),
            };
            GetUserCommand getUserCommand = new ();
            getUserCommand.UserId = 1;
            this.userService.GetByIdAsync(getUserCommand).Returns(GetUserViewModel(Roles.SystemAdmin, false));
            var userController = new UserController(
                this.userService,
                this.localizer,
                this.contextAccessor,
                this.createUserValidator,
                this.updateUserDetailsValidator,
                this.getUserValidator,
                this.activateUserValidator,
                this.deactivateUserCommandValidator,
                this.uploadProfilePicValidator,
                this.profilePictureValidator,
                this.updateMyProfileValidator,
                this.getUserClaimsValidator,
                this.deleteUserValidator);

            // Act
            var result = await userController.GetUser(getUserCommand);
            var convertResult = result.Result as OkObjectResult;

            // Assert
            convertResult!.Value.ShouldNotBeNull();
            convertResult!.Value.ShouldBeOfType(typeof(UserViewModel));
            var userViewModel = convertResult!.Value as UserViewModel;
            userViewModel!.UserId.ShouldNotBeNull();
            userViewModel!.RoleId.ShouldBe(Roles.SystemAdmin);
        }

        /// <summary>
        /// Failing test for GetUser method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetUser_ReturnsUserDetailsFailure()
        {
            // Arrange
            this.contextAccessor = await this.GetService<IHttpContextAccessor>();
            this.contextAccessor.HttpContext = new DefaultHttpContext()
            {
                User = this.GetUserClaims(Roles.SystemAdmin, false),
            };
            GetUserCommand getUserCommand = new ()
            {
                UserId = 0,
            };

            this.userService.When(x => x.GetByIdAsync(getUserCommand)).Do(x => { throw new Exception(this.localizer["UserDoesNotExist"]); });
            var userController = new UserController(
                this.userService,
                this.localizer,
                this.contextAccessor,
                this.createUserValidator,
                this.updateUserDetailsValidator,
                this.getUserValidator,
                this.activateUserValidator,
                this.deactivateUserCommandValidator,
                this.uploadProfilePicValidator,
                this.profilePictureValidator,
                this.updateMyProfileValidator,
                this.getUserClaimsValidator,
                this.deleteUserValidator);

            // Act
            var action = () => userController.GetUser(getUserCommand);

            // Assert
            action.ShouldThrow<Exception>(this.localizer["UserDoesNotExist"]);
        }

        /// <summary>
        /// Success test for GetUsers method by System admin and regional admin.
        /// </summary>
        /// <param name="role">Role.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Theory]
        [InlineData(Roles.SystemAdmin)]
        [InlineData(Roles.RegionalAdmin)]
        [InlineData(Roles.CountryAdmin)]
        public async Task GetUsers_ReturnsPaginatedResponseSuccess(Roles role)
        {
            // Arrange
            this.contextAccessor = await this.GetService<IHttpContextAccessor>();
            this.contextAccessor.HttpContext = new DefaultHttpContext()
            {
                User = this.GetUserClaims(role, false),
            };

            GetUsersCommand command = new ()
            {
                PaginationFilter = new PaginationFilter(),
            };
            List<UserViewModel> users;
            if (role == Roles.CountryAdmin)
            {
                users = GenerateUserViewModels().Where(x => x.Country!.CountryId == 1).ToList();
            }
            else if (role == Roles.RegionalAdmin)
            {
                users = GenerateUserViewModels().Where(x => x.Region == "SEARO").ToList();
            }
            else
            {
                users = GenerateUserViewModels();
            }

            this.userService.GetAllPaginatedAndFilteredUsers(command).ReturnsForAnyArgs(new PaginatedResponse<UserViewModel>()
                {
                    Data = users.Take(10).ToList(),
                    TotalRows = users.Count,
                });

            var userController = new UserController(
                this.userService,
                this.localizer,
                this.contextAccessor,
                this.createUserValidator,
                this.updateUserDetailsValidator,
                this.getUserValidator,
                this.activateUserValidator,
                this.deactivateUserCommandValidator,
                this.uploadProfilePicValidator,
                this.profilePictureValidator,
                this.updateMyProfileValidator,
                this.getUserClaimsValidator,
                this.deleteUserValidator);

            // Act
            var result = await userController.GetUsers(command);
            var convertedResult = result.Result as OkObjectResult;

            // Assert
            convertedResult!.Value.ShouldNotBeNull();
            convertedResult!.Value.ShouldBeAssignableTo(typeof(PaginatedResponse<UserViewModel>));

            var response = convertedResult.Value as PaginatedResponse<UserViewModel>;
            response.ShouldNotBeNull();
            response.Data.Count.ShouldBeLessThanOrEqualTo(10);
            if (role == Roles.RegionalAdmin)
            {
                foreach (var user in response.Data)
                {
                    user.Region.ShouldBe("SEARO");
                }
            }
            else if (role == Roles.CountryAdmin)
            {
                foreach (var user in response.Data)
                {
                    user.Country!.CountryId.ShouldBe(1);
                }
            }
        }

        /// <summary>
        /// Failure test for GetUsers method by Secretariat, CountryUser, GlobalViewer and CountryViewer.
        /// </summary>
        /// <param name="role">Role.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Theory]
        [InlineData(Roles.Secretariat)]
        [InlineData(Roles.CountryUser)]
        [InlineData(Roles.GlobalViewer)]
        [InlineData(Roles.CountryViewer)]
        public async Task GetUsers_ReturnsPaginatedResponseFailure(Roles role)
        {
            // Arrange
            this.contextAccessor = await this.GetService<IHttpContextAccessor>();
            this.contextAccessor.HttpContext = new DefaultHttpContext()
            {
                User = this.GetUserClaims(role, false),
            };

            GetUsersCommand command = new ()
            {
                PaginationFilter = new PaginationFilter(),
            };
            this.userService.When(x => x.GetAllPaginatedAndFilteredUsers(command)).Do(x => throw new Exception(this.localizer["CantPerformOperation"]));
            var userController = new UserController(
                this.userService,
                this.localizer,
                this.contextAccessor,
                this.createUserValidator,
                this.updateUserDetailsValidator,
                this.getUserValidator,
                this.activateUserValidator,
                this.deactivateUserCommandValidator,
                this.uploadProfilePicValidator,
                this.profilePictureValidator,
                this.updateMyProfileValidator,
                this.getUserClaimsValidator,
                this.deleteUserValidator);

            // Act
            var action = async () => { await userController.GetUsers(command); };

            // Assert
            action.ShouldThrow<Exception>(this.localizer["CantPerformOperation"]);
        }

        private static List<UserViewModel> GenerateUserViewModels()
        {
            var userViewModels = new List<UserViewModel>();

            // Sample countries for the CountryViewModel
            var countries = new List<CountryDropdownViewModel>
            {
                new CountryDropdownViewModel { CountryId = 1, Name = "United States", },
                new CountryDropdownViewModel { CountryId = 2, Name = "Canada", },
                new CountryDropdownViewModel { CountryId = 3, Name = "United Kingdom", },
                new CountryDropdownViewModel { CountryId = 4, Name = "Australia", },
            };

            var regions = new List<string>() { "AFRO", "AMRO", "EMRO", "EURO", "SEARO", "WPRO", };

            for (int i = 1; i <= 100; i++)
            {
                var userViewModel = new UserViewModel
                {
                    UserId = i,
                    RoleId = (Roles)(i % Enum.GetValues(typeof(Roles)).Length), // Cycling through Roles enum values
                    FirstName = $"UserFirstName{i}",
                    LastName = $"UserLastName{i}",
                    Email = $"user{i}@example.com",
                    ProfilePicture = $"profile_{i}.jpg",
                    Country = countries[i % countries.Count], // Cycling through sample countries
                    Region = regions[i % regions.Count],
                    PreferredLanguageId = (Languages)(i % Enum.GetValues(typeof(Languages)).Length), // Cycling through Languages enum values
                    Institution = $"Institution{i}",
                    Affiliation = $"Affiliation{i}",
                    IsReadOnly = i % 2 == 0, // Alternating between true and false
                    Status = (ProfileStatus)(i % Enum.GetValues(typeof(ProfileStatus)).Length), // Cycling through ProfileStatus enum values
                    IsActive = i % 3 == 0, // Alternating between true and false
                };

                userViewModels.Add(userViewModel);
            }

            return userViewModels;
        }

        private static UserViewModel GetUserViewModel(Roles role, bool isReadonly)
        {
            return new UserViewModel
            {
                FirstName = "test",
                LastName = "test",
                Country = new CountryDropdownViewModel { CountryId = 1, Name = "India" },
                Email = "tnaphs@gmail.com",
                IsActive = true,
                IsReadOnly = isReadonly,
                RoleId = role,
                PreferredLanguageId = Languages.English,
                Region = "SEARO",
                Status = null,
                UserId = 1,
            };
        }
    }
}