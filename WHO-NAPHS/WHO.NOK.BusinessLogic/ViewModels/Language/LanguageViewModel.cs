// <copyright file="LanguageViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.Language
{
    /// <summary>
    /// View model for language.
    /// </summary>
    public class LanguageViewModel
    {
        /// <summary>
        /// Gets or sets Language Id.
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets Language name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets language code.
        /// </summary>
        public string? LanguageCode { get; set; }
    }
}
