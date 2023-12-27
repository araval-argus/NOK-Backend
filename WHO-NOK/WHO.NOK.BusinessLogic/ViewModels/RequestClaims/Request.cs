// <copyright file="Request.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.RequestClaims
{
    using WHO.NOK.BusinessLogic.ViewModels.User;
    /// <summary>
    /// Request model to save the user claims data.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Gets or sets <see cref="UserViewModel"/> model.
        /// </summary>
        public CurrentUserViewModel? User { get; set; } = null;
    }
}