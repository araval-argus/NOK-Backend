// <copyright file="FluentErrorExtension.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Core.ResponseMiddleware
{
    using System.Text;
    using FluentValidation.Results;

    /// <summary>
    /// Extension method to get all validation error.
    /// </summary>
    public static class FluentErrorExtension
    {
        /// <summary>
        /// Get all validation errors.
        /// </summary>
        /// <param name="result"> <see cref="ValidationResult"/> Validation result set.</param>
        /// <returns> Returns the all validation errors. </returns>
        public static string GetAllValidationErrors(this ValidationResult result)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Validation Error(s)");
            foreach (var error in result.Errors)
            {
                stringBuilder.AppendLine($"Property Name= {error.PropertyName}, Error= {error.ErrorMessage}");
            }

            return stringBuilder.ToString();
        }
    }
}