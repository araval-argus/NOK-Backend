// <copyright file="ControllerBaseExtensions.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Helper
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller base extensions.
    /// </summary>
    public static class ControllerBaseExtensions
    {
        /// <summary>
        /// Appends custom content disposition header to file result.
        /// </summary>
        /// <param name="cb">Controller base reference.</param>
        /// <param name="fileContents">File Bytes.</param>
        /// <param name="contentType">Mime Type.</param>
        /// <param name="headers">Custom headers.</param>
        /// <returns>FileContentResult with content-disposition header.</returns>
        public static FileContentResult FileWithCustomHeaders(this ControllerBase cb, byte[] fileContents, string? contentType, Dictionary<string, string> headers = null)
        {
            // Adds custom headers to response.
            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    cb.Response.Headers.Add(header.Key, header.Value);
                }
            }

            return cb.File(fileContents, contentType, true);
        }
    }
}