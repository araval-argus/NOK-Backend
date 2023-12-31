﻿// <copyright file="ApiError.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Core.Wrappers
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// API Error response model.
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class.
        /// </summary>
        /// <param name="message">message for the error.</param>
        public ApiError(string message)
        {
            this.ExceptionMessage = message;
            this.IsError = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class.
        /// </summary>
        /// <param name="modelState"><see cref="ModelStateDictionary"/> model state.</param>
        public ApiError(ModelStateDictionary modelState)
        {
            this.IsError = true;

            if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0))
            {
                this.ExceptionMessage = "Please correct the specified validation errors and try again.";

                this.ValidationErrors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
            }
        }

        /// <summary>
        /// Gets or sets error details.
        /// </summary>
        public string Details { get; set; } = default!;

        /// <summary>
        /// Gets or sets error exception message.
        /// </summary>
        public string ExceptionMessage { get; set; } = default!;

        /// <summary>
        /// Gets or sets a value indicating whether is error or not.
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Gets or sets list of validation errors.
        /// </summary>
        public IEnumerable<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();
    }
}