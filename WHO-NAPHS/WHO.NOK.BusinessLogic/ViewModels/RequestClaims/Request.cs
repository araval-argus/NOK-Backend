// <copyright file="Request.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims
{
    using WHO.NAPHS.BusinessLogic.ViewModels.UserClaims;

    /// <summary>
    /// Request model to save the user claims data.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Gets or sets <see cref="UserClaimsViewModel"/> model.
        /// </summary>
        public UserClaimsViewModel? User { get; set; } = null;
    }
}