// <copyright file="EmailService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.ServiceImplementation
{
    using System.Threading.Tasks;
    using Microsoft.Graph;
    using Microsoft.Graph.Models;
    using WHO.NAPHS.BusinessLogic.Helper.EmailHelper;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.Infrastructure.Models.DatabaseContext;

    /// <summary>
    /// Initializes a new instance of <see cref="EmailService.cs"/> class.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public EmailService(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task SendMicrosoftInvitationMail(string email, string graphToken)
        {
            GraphServiceClient graphServiceClient = CreateGraphServiceClient(graphToken);
            try
            {
                var result = await graphServiceClient.Invitations.PostAsync(
                new Invitation
                {
                    // TODO : Remove below magic string.
                    // InviteRedirectUrl = Configuration.GetValue<string>("InviteRedirectUrl"),
                    InviteRedirectUrl = "https://naphs-d-webapp-angular-euw-01.azurewebsites.net/",
                    InvitedUserEmailAddress = email,
                    InvitedUserMessageInfo = new InvitedUserMessageInfo
                    {
                        CustomizedMessageBody = "Invite to NAPHS",
                    },
                    SendInvitationMessage = true,
                });
            }
            catch (Microsoft.Graph.ServiceException ex)
            {
                if (ex.Message.Contains("email address is a verified domain of this directory", StringComparison.InvariantCultureIgnoreCase) || ex.Message.Contains("the invited user already exists in the directory", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new Exception("User already exists");
                }

                // ErrorLog errorLog = new()
                // {
                //     CreatedAt = DateTime.UtcNow,
                //     Message = $"StatusCode = {ex.StatusCode}, Source - {ex.Source}, Message = {ex.Message}",
                //     StackTrace = ex.StackTrace,
                //     UserId = user.UserId,
                //     Module = $"{nameof(UsersController)} - {nameof(SendInvitationMail)}"
                // };
            }
            catch (Microsoft.Graph.Models.ODataErrors.ODataError ex)
            {
                _ = ex.Message;
            }
        }

        /// <summary>
        /// Creates a service client for graph.
        /// </summary>
        /// <param name="accessToken">Access token of currently logged in user.</param>
        /// <returns>Instance of <see cref="GraphServiceClient"/> class.</returns>
        private static GraphServiceClient CreateGraphServiceClient(string accessToken)
        {
            return new GraphServiceClient(new AzureAuthenticationProvider(accessToken));
        }
    }
}