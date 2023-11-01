// <copyright file="AuthTokenViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.ClaimsRequest
{
    using Newtonsoft.Json;

    /// <summary>
    /// JEE Token view model for authorize the country wise technical areas and indicators.
    /// </summary>
    public class AuthTokenViewModel
    {
        /// <summary>
        /// Gets or sets token type.
        /// </summary>
        [JsonProperty("token_type")]
        public string? TokenType { get; set; }

        /// <summary>
        /// Gets or sets access token.
        /// </summary>
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        /// <summary>
        /// Gets or sets expires in.
        /// </summary>
        [JsonProperty("expires_in")]
        public string? ExpiresIn { get; set; }

        /// <summary>
        /// Gets or sets ext expires in.
        /// </summary>
        [JsonProperty("ext_expires_in")]
        public string? ExtExpiresIn { get; set; }

        /// <summary>
        /// Gets or sets expires on.
        /// </summary>
        [JsonProperty("expires_on")]
        public string? ExpiresOn { get; set; }

        /// <summary>
        /// Gets or  sets not before.
        /// </summary>
        [JsonProperty("not_before")]
        public string? NotBefore { get; set; }

        /// <summary>
        /// Gets or sets resource.
        /// </summary>
        [JsonProperty("resource")]
        public string? Resource { get; set; }
    }
}