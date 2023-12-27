// <copyright file="ApplicationSettings.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Core.Constant
{
    using System.Text.Json;

    /// <summary>
    /// Defines the application settings.
    /// </summary>
    public class ApplicationSettings
    {
        /// <summary>
        /// Gets or sets Tenant Id.
        /// </summary>
        // public string TenantId { get; set; } = string.Empty;

        // /// <summary>
        // /// Gets or sets Application name.
        // /// </summary>
        // public string ApplicationVersion { get; set; } = "0.0.1";

        // /// <summary>
        // /// Gets or sets maximin strategic plan allowed.
        // /// </summary>
        // public int MaxStrategicPlanAllowed { get; set; } = 2;

        // /// <summary>
        // /// Gets or sets Technical Area Excel Column.
        // /// </summary>
        // public string? TechnicalAreaExcelColumn { get; set; }

        // /// <summary>
        // /// Gets or sets Indicator Excel Column.
        // /// </summary>
        // public string? IndicatorExcelColumn { get; set; }

        // /// <summary>
        // /// Gets or sets list of valid excel extensions.
        // /// </summary>
        // public string ValidExcelExtensions { get; set; } = ".xls,.xlsx";

        // /// <summary>
        // /// Gets or sets list of valid excel extensions.
        // /// </summary>
        // public string ExcelImportFilePath { get; set; } = "Resources/ExcelImport";

        // /// <summary>
        // /// Gets or sets auth token url.
        // /// </summary>
        // public string? AuthTokenURL { get; set; }

        // /// <summary>
        // /// Gets or sets client id for JEE.
        // /// </summary>
        // public string? JEEClientId { get; set; }

        // /// <summary>
        // /// Gets or sets client secret for JEE.
        // /// </summary>
        // public string? JEEClientSecret { get; set; }

        // /// <summary>
        // /// Gets or sets auth token parameter name.
        // /// </summary>
        // public string? JEEAuthTokenParameterName { get; set; }

        // /// <summary>
        // /// Gets or sets JEE Score Url to get the JEE scores per country.
        // /// </summary>
        // public string? JEEScoreUrl { get; set; }

        // /// <summary>
        // /// Gets or sets JEE Score Url to get the JEE Recommendation per country.
        // /// </summary>
        // public string? JEERecommendationUrl { get; set; }

        // /// <summary>
        // /// Gets or sets SPAR client id.
        // /// </summary>
        // public string? SPARClientId { get; set; }

        // /// <summary>
        // /// Gets or sets SPAR client secret.
        // /// </summary>
        // public string? SPARClientSecret { get; set; }

        // /// <summary>
        // /// Gets or sets SPAR scope.
        // /// </summary>
        // public string? SPARScope { get; set; }

        // /// <summary>
        // /// Gets or sets SPAR x-api-key.
        // /// </summary>
        // public string? SPARXAPIKey { get; set; }

        // /// <summary>
        // /// Gets or sets SPAR submission url.
        // /// </summary>
        // public string? SPARSubmissionURL { get; set; }

        // /// <summary>
        // /// Gets or sets the profile picture.
        // /// </summary>
        // public string? ProfilePicturePath { get; set; } = "Resources/ProfilePicture";

        // /// <summary>
        // /// Converts the string into instance of AppSettings class.
        // /// </summary>
        // /// <param name="json">json format.</param>
        // /// <returns>Returns the instance of AppSettings class.</returns>
        public static ApplicationSettings? FromJson(string json) => JsonSerializer.Deserialize<ApplicationSettings>(json);

        /// <summary>
        /// Converts the current object into json format.
        /// </summary>
        /// <returns>string that represents the json format of the current object.</returns>
        public string ToJson() => JsonSerializer.Serialize(this);
    }
}
