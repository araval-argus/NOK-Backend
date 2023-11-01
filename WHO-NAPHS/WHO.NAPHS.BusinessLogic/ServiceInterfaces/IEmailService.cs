// <copyright file="IEmailService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ServiceInterfaces
{
    using WHO.NAPHS.BusinessLogic.Helper.EmailHelper;

    /// <summary>
    /// Email service interface.
    /// </summary>
    public interface IEmailService : IBaseService
    {
        /// <summary>
        /// Sends an invitation email to the newly created user.
        /// </summary>
        /// <param name="email">Email of an invited user.</param>
        /// <param name="graphToken">Access token of logged in user.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SendMicrosoftInvitationMail(string email, string graphToken);
    }
}