// <copyright file="AzureAuthenticationProvider.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Helper.EmailHelper
{
    using Microsoft.Kiota.Abstractions;
    using Microsoft.Kiota.Abstractions.Authentication;

    /// <summary>
    /// Graph claims types.
    /// </summary>
    public class AzureAuthenticationProvider : IAuthenticationProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureAuthenticationProvider"/> class.
        /// </summary>
        /// <param name="graphToken">Access token of currently logged in user.</param>
        public AzureAuthenticationProvider(string graphToken)
        {
            this.GraphToken = graphToken;
        }

        /// <summary>
        /// Gets Access token of logged in user.
        /// </summary>
        public string GraphToken { get; private set; }

        /// <inheritdoc/>
        public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
        {
            request.Headers.Add("Authorization", "Bearer " + this.GraphToken);
            return Task.FromResult(0);
        }
    }
}
