﻿// <copyright file="ApiResponse.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8601

namespace WHO.NAPHS.Core.Wrappers
{
    using System.Net;
    using System.Runtime.Serialization;

    /// <summary>
    /// API response for generic response for all.
    /// </summary>
    /// <typeparam name="T"> Model/Class.</typeparam>
    [DataContract]
    public class ApiResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class.
        /// </summary>
        /// <param name="statusCode"> Status code.</param>
        /// <param name="message"> Message.</param>
        /// <param name="result"> Result.</param>
        /// <param name="apiError"> <see cref="ApiError"/> API Error.</param>
        /// <param name="apiVersion"> API version.</param>
        public ApiResponse(
           int statusCode = (int)HttpStatusCode.OK,
           string message = "",
           T result = default(T),
           ApiError apiError = null,
           string apiVersion = "1.0")
        {
            this.StatusCode = statusCode;
            this.Message = message;
            this.Result = result;
            this.ResponseException = apiError;
            this.Version = apiVersion;
        }

        /// <summary>
        /// Gets or sets response message.
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets response exception.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public ApiError ResponseException { get; set; }

        /// <summary>
        /// Gets or sets response result.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public T Result { get; set; }

        /// <summary>
        /// Gets or sets response http status code.
        /// </summary>
        [DataMember]
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets response version.
        /// </summary>
        [DataMember]
        public string Version { get; set; }
    }
}
