// <copyright file="DownloadExcelFileViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.Excel
{
    /// <summary>
    /// Excel import view model.
    /// </summary>
    public class DownloadExcelFileViewModel
    {
        /// <summary>
        /// Gets or sets fle bytes.
        /// </summary>
        public byte[] FileBytes { get; set; } = null!;

        /// <summary>
        /// Gets or sets content type of the file.
        /// </summary>
        public string ContentType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets name of the file.
        /// </summary>
        public string FileName { get; set; } = string.Empty;
    }
}