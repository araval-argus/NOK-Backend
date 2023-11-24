// <copyright file="ResponseMiddlewareExtensions.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Core.ResponseMiddleware
{
    using System.Net;
    using System.Net.Mime;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Response middleware extensions.
    /// </summary>
    public static class ResponseMiddlewareExtensions
    {
        /// <summary>
        /// Ignore API for the response modification.
        /// </summary>
        /// <param name="context"> <see cref="HttpContext"/> Http Context.</param>
        /// <returns> Returns the status of the API response if we need to ignore this and modify the response.</returns>
        public static bool Ignore(this HttpContext context)
        {
            return context.IsDownload();
        }

        /// <summary>
        /// API Response wrapper that being used as middleware from Program.cs file.
        /// </summary>
        /// <param name="builder"> <see cref="IApplicationBuilder"/> Application Builder.</param>
        /// <param name="traceResponse"> Trance response.</param>
        /// <typeparam name="ResponseMiddleware"> Response middleware.</typeparam>
        /// <returns> Returns the application builder.</returns>
        public static IApplicationBuilder UseApiResponseWrapperMiddleware(this IApplicationBuilder builder, bool traceResponse = false)
        {
            return builder.UseMiddleware<ResponseMiddleware>(traceResponse);
        }

        /// <summary>
        /// Check if context is downloadable or not.
        /// </summary>
        /// <param name="context"> <see cref="HttpContext"/> Http Context.</param>
        /// <returns> Returns the api is downloadable or not.</returns>
        internal static bool IsDownload(this HttpContext context)
        {
            bool isDowload = false;
            try
            {
                if (context.Response.ContentType != null)
                {
                    foreach (var downloadAbleType in DownloadableTypes())
                    {
                        bool isWildCard = downloadAbleType.Contains('*');
                        if (!isWildCard)
                        {
                            isDowload = downloadAbleType.Contains(context.Response.ContentType);
                        }
                        else
                        {
                            string[] mimeParts = downloadAbleType.Split("/", StringSplitOptions.RemoveEmptyEntries);
                            isDowload = mimeParts != null && mimeParts.Length > 0 && context.Response.ContentType.Contains(mimeParts[0]);
                        }

                        if (isDowload)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return isDowload;
        }

        /// <summary>
        /// Downloadable types.
        /// </summary>
        /// <returns> Returns the list of downloadable types.</returns>
        internal static List<string> DownloadableTypes()
        {
            return new List<string>
            {
                 MediaTypeNames.Application.Octet,
                 "image/*",
                 "text/csv",
                 "application/pdf",
                 "application/json",
                 "application/xml",
                 "application/msword",
                 "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                 "application/vnd.ms-excel",
                 "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                 "application/x-zip-compressed",
                 "text/xml",
                 "application/x-msdownload",
            };
        }

        /// <summary>
        /// Check for no content response.
        /// </summary>
        /// <param name="context"> <see cref="HttpContext"/> Http Context.</param>
        /// <returns> returns the status weather the response has content or not.</returns>
        internal static bool IsNoContent(this HttpContext context) => context.Response.StatusCode == (int)HttpStatusCode.NoContent;
    }
}