// <copyright file="AssessmentImportService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8604, CS8602, CS8600, CS8603

namespace WHO.NOK.Infrastructure.ServiceImplementation
{
    using System.Net.Http.Headers;
    using Microsoft.Extensions.Localization;
    using Newtonsoft.Json;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.BusinessLogic.ViewModels.Assessments;
    using WHO.NOK.BusinessLogic.ViewModels.ClaimsRequest;
    using WHO.NOK.Core.Common.Resources;
    using WHO.NOK.Core.Constant;
    using WHO.NOK.Core.Wrappers;

    /// <summary>
    /// Implement <see cref="IAssessmentImportService"/> interface.
    /// </summary>
    public class AssessmentImportService : IAssessmentImportService
    {
        private readonly IStringLocalizer<Resources> localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentImportService"/> class.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/> string localizer.</param>
        public AssessmentImportService(IStringLocalizer<Resources> localizer)
        {
            this.localizer = localizer;
        }

        /// <inheritdoc/>
        public async Task<string> GetJEEAuthTokenAsync()
        {
            string authToken = null;

            // Create an instance of HttpClient
            using (HttpClient httpClient = new ())
            {
                // Define the request URL
                string requestUrl = CommonSettings.ApplicationSettings.AuthTokenURL;

                // Create a dictionary to store the request body.
                var formData = new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" },
                    { "client_id", CommonSettings.ApplicationSettings.JEEClientId },
                    { "client_secret", CommonSettings.ApplicationSettings.JEEClientSecret },
                };

                // Create the request content using FormUrlEncodedContent
                var content = new FormUrlEncodedContent(formData);

                // Send the POST request and get the response
                HttpResponseMessage response = await httpClient.PostAsync(requestUrl, content);

                // Check the response status code
                if (response.IsSuccessStatusCode)
                {
                    var jeeResponse = JsonConvert.DeserializeObject<AuthTokenViewModel>(await response.Content.ReadAsStringAsync());
                    authToken = jeeResponse.AccessToken;
                }
                else
                {
                    throw new ApiException(this.localizer["WentWrongWhileFetchingToken"]);
                }
            }

            return authToken;
        }

        /// <inheritdoc/>
        public async Task<JEEAssessmentViewModel> GetJEERecommendationAsync(string countryISOCode)
        {
            JEEAssessmentViewModel model = new ();

            string authToken = await this.GetJEEAuthTokenAsync();

            if (string.IsNullOrEmpty(authToken))
            {
                throw new ApiException(this.localizer["InvalidToken"]);
            }

            string url = $"{CommonSettings.ApplicationSettings.JEERecommendationUrl}?country_iso={countryISOCode}";

            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization header with the bearer token
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                // Send the GET request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    model = JsonConvert.DeserializeObject<JEEAssessmentViewModel>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new ApiException(this.localizer["WentWrongWhileFetchingRecommendations"]);
                }
            }

            return model;
        }

        /// <inheritdoc/>
        public async Task<JEEAssessmentViewModel> GetJEEScoreAsync(string countryISOCode)
        {
            JEEAssessmentViewModel model = new ();

            string authToken = await this.GetJEEAuthTokenAsync();

            if (string.IsNullOrEmpty(authToken))
            {
                throw new ApiException(this.localizer["InvalidToken"]);
            }

            string url = $"{CommonSettings.ApplicationSettings.JEEScoreUrl}?country_iso={countryISOCode}";

            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization header with the bearer token
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                // Send the GET request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    model = JsonConvert.DeserializeObject<JEEAssessmentViewModel>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new ApiException(this.localizer["WentWrongWhileFetchingScore"]);
                }
            }

            return model;
          }

        /// <inheritdoc/>
        public async Task<string> GetSPARAuthTokenAsync()
        {
            string authToken = null;

            // Create an instance of HttpClient
            using (HttpClient httpClient = new ())
            {
                // Define the request URL
                string requestUrl = CommonSettings.ApplicationSettings.AuthTokenURL;

                // Create a dictionary to store the request body.
                var formData = new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" },
                    { "client_id", CommonSettings.ApplicationSettings.SPARClientId },
                    { "client_secret", CommonSettings.ApplicationSettings.SPARClientSecret },
                    { "scope:", CommonSettings.ApplicationSettings.SPARScope },
                };

                // Create the request content using FormUrlEncodedContent
                var content = new FormUrlEncodedContent(formData);

                // Send the POST request and get the response
                HttpResponseMessage response = await httpClient.PostAsync(requestUrl, content);

                // Check the response status code
                if (response.IsSuccessStatusCode)
                {
                    var jeeResponse = JsonConvert.DeserializeObject<AuthTokenViewModel>(await response.Content.ReadAsStringAsync());
                    authToken = jeeResponse.AccessToken;
                }
                else
                {
                    throw new ApiException(this.localizer["InvalidToken"]);
                }
            }

            return authToken;
        }

        /// <inheritdoc/>
        public async Task<SPARAssessmentViewModel> GetSPARAssessmentAsync(string countryISO3Code)
        {
            SPARAssessmentViewModel model = new ();

            string authToken = await this.GetSPARAuthTokenAsync();

            if (string.IsNullOrEmpty(authToken))
            {
                throw new ApiException(this.localizer["InvalidToken"]);
            }

            string url = $"{CommonSettings.ApplicationSettings.SPARSubmissionURL}?iso3code={countryISO3Code}&year={2022}";

            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization header with the bearer token
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                httpClient.DefaultRequestHeaders.Add("x-api-key", CommonSettings.ApplicationSettings.SPARXAPIKey);

                // Send the GET request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    model = JsonConvert.DeserializeObject<SPARAssessmentViewModel>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new ApiException(this.localizer["WentWrongWhileFetchingScore"]);
                }
            }

            return model;
        }
    }
}