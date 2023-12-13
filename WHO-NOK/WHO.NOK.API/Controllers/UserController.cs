// <copyright file="UserController.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.API.Controllers
{
    using DocumentFormat.OpenXml.EMMA;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Graph.Models;
    using WHO.NOK.API.Helper;
    using WHO.NOK.BusinessLogic.Features.User;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.BusinessLogic.ViewModels;
    using WHO.NOK.BusinessLogic.ViewModels.Country;
    using WHO.NOK.BusinessLogic.ViewModels.Language;
    using WHO.NOK.BusinessLogic.ViewModels.PaginatedResponses;
    using WHO.NOK.BusinessLogic.ViewModels.User;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Wrappers;
    using WHO.NOKS.BusinessLogic.Features.User;

    /// <summary>
    /// Controller to get the common values for the drop-downs.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService"><see cref="IUserService"/> Service.</param>
        public UserController(IUserService userService) {
            this.userService = userService;
        }

        /// <summary>
        /// Get all the users.
        /// </summary>
        /// <param name="request"><see cref="GetUsersCommand"/> command to get users.</param>
        /// <returns>Returns paginated list of <see cref="UserViewModel"/> model.</returns>
        // GET: User/GetUsers
        [HttpPost("GetUsers")]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResponse<UserViewModel>>), 200)]
        public async Task<ActionResult<PaginatedResponse<UserViewModel>>> GetUsers([FromBody] GetUsersCommand request)
        {
            if (request == null)
            {
                throw new ApiException("Request is null");
            }

            return this.Ok(await this.userService.GetAllPaginatedAndFilteredUsers(request));
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="request"><see cref="CreateUserCommand"/> command request to create a new user.</param>
        /// <returns>Returns the newly created <see cref="UserViewModel"/> model.</returns>
        // POST: User
        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(ApiResponse<CurrentUserViewModel>), 200)]
        public async Task<ActionResult<int>> CreateUser([FromForm] CreateUserCommand request)
        {
            if (request == null)
            {
                throw new ApiException("Request is null");
            }

            return this.Ok(await this.userService.CreateUserAsync(request));
        }
        
        /// <summary>
        /// Update the details of the user.
        /// </summary>
        /// <param name="request"> <see cref="UpdateUserDetailsCommand"/> model to update user details.</param>
        /// <returns>Returns updated <see cref="UserViewModel"/> model.</returns>
        // PUT: User
        [HttpPut("UpdateUser")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), 200)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDetailsCommand request)
        {
            if (request == null)
            {
                throw new ApiException("Request is null");
            }

            return this.Ok(await this.userService.UpdateUserDetailsAsync(request));
        }
        
        /// <summary>
        /// Deactivates a user's profile.
        /// </summary>
        /// <param name="request"><see cref="DeactivateUserCommand"/> command request to deactivate a user.</param>
        /// <returns>Returns a boolean representing the result of the asynchronous operation.</returns>
        [HttpPut("DeactivateUser")]
        [ProducesResponseType(typeof(ApiResponse<int>), 200)]
        public async Task<IActionResult> DeactivateUser([FromBody] DeactivateUserCommand request)
        {
            if (request == null)
            {
                throw new ApiException("Request is null");
            }

            return this.Ok(await this.userService.DeactivateUserAsync(request));
        }

        /// <summary>
        /// Activates a user's profile.
        /// </summary>
        /// <param name="request"><see cref="ActivateUserCommand"/> command request to activate a user.</param>
        /// <returns>Returns a boolean representing the result of the asynchronous operation.</returns>
        [HttpPut("ActivateUser")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<IActionResult> ActivateUser([FromBody] ActivateUserCommand request)
        {
            if (request == null)
            {
                throw new ApiException("Request is null");
            }

            return this.Ok(await this.userService.ActivateUserAsync(request));
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="request"> <see cref="DeleteUserCommand"/> command request to delete a user.</param>
        /// <returns>Returns the <see cref="UserViewModel"/> object which has been deleted successfully.</returns>
        [HttpDelete("DeleteUser")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), 200)]
        public async Task<IActionResult> DeleteUser(DeleteUserCommand request)
        {
            // request.User = this.user;
            // var result = await this.deleteUserValidator.ValidateAsync(request);
            // if (!result.IsValid)
            // {
            //     throw new ApiException(result.GetAllValidationErrors());
            // }

            return this.Ok(await this.userService.DeleteUserAsync(request));
        }
    }
}
