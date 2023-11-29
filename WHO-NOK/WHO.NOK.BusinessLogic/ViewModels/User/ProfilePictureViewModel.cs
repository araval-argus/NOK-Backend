// <copyright file="ProfilePictureViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.User
{
    /// <summary>
    /// Profile picture response view model.
    /// </summary>
    public class ProfilePictureViewModel
    {
        /// <summary>
        /// Gets or sets fle bytes.
        /// </summary>
        public byte[] FileBytes { get; set; } = null!;

        /// <summary>
        /// Gets or sets content type of the file.
        /// </summary>
        public string? ContentType { get; set; }
    }
}