// <copyright file="Language.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Languages
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Language Database Model.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        public Language()
        {
        }

        /// <summary>
        /// Gets or sets Language Id.
        /// </summary>
        [Key]
        public int LanguageId { get; protected set; }

        /// <summary>
        /// Gets or sets Language name.
        /// </summary>
        public string Name { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets language code.
        /// </summary>
        public string LanguageCode { get; protected set; } = default!;
    }
}
