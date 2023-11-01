// <copyright file="GetProfilePictureCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8618

namespace WHO.NAPHS.BusinessLogic.Features.User
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to get user profile picture.
    /// </summary>
    public class GetProfilePictureCommand : Request
    {
        /// <summary>
        /// Gets or sets image path.
        /// </summary>
        public string ImagePath { get; set; }
    }
}