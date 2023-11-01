// <copyright file="StringExtension.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Core.ResponseMiddleware
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// String extension.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Check if is valid json.
        /// </summary>
        /// <param name="text"> JSON Text.</param>
        /// <returns> Returns the status that indicates if the json is valid or not.</returns>
        public static bool IsValidJson(this string text)
        {
            text = text.Trim();

            if ((!text.StartsWith("{") || !text.EndsWith("}")) && (!text.StartsWith("[") || !text.EndsWith("]")))
            {
                return false;
            }

            try
            {
                JToken.Parse(text);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
