// <copyright file="Values.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Core.Constant
{
    /// <summary>
    /// Common values.
    /// </summary>
    public class Values
    {
        /// <summary>
        /// Regex for validation of email.
        /// </summary>
        public const string EmailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        /// <summary>
        /// Regex for validation for name.
        /// </summary>
        public const string NameRegex = @"^[A-Za-z\s]*$";

        /// <summary>
        /// Regex for email validation of who domain.
        /// </summary>
        public const string WhoEmailDomainRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@who.int)\Z";
    }
}