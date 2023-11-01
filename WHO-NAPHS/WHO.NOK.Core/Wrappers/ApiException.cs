// <copyright file="ApiException.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress constructor error.
#pragma warning disable CS8618

namespace WHO.NOK.Core.Wrappers
{
    /// <summary>
    /// Api exception response model.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="statusCode"> Status code.</param>
        /// <param name="errors"> Errors.</param>
        public ApiException(
           string message,
           int statusCode = 500,
           IEnumerable<ValidationError> errors = null)
           : base(message)
        {
            this.StatusCode = statusCode;
            this.Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="ex"> <see cref="Exception"/> details.</param>
        /// <param name="statusCode"> Https Status code.</param>
        public ApiException(Exception ex, int statusCode = 500)
            : base(ex.Message)
        {
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Gets or sets errors.
        /// </summary>
        public IEnumerable<ValidationError> Errors { get; set; }

        /// <summary>
        /// Gets or sets Status code.
        /// </summary>
        public int StatusCode { get; set; }
    }
}
