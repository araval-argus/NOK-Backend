// <copyright file="UploadUserProfilePicCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8618

namespace WHO.NOK.BusinessLogic.Features.User
{
    using Microsoft.AspNetCore.Http;
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Upload User Profile Picture Command class.
    /// </summary>
    public class UploadUserProfilePicCommand : Request
    {
        /// <summary>
        /// Gets or sets profile picture of the user.
        /// </summary>
        public IFormFile ProfilePicture { get; set; }
    }
}