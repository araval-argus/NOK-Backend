// <copyright file="GetActionImportHistoryViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.Excel
{
    /// <summary>
    /// Excel import view model.
    /// </summary>
    public class GetActionImportHistoryViewModel
    {
        /// <summary>
        /// Gets or Sets Id of the sheet/planningTool.
        /// </summary>
        public int SheetId { get; set; }

        /// <summary>
        /// Gets or Sets Name of the sheet that is uploaded.
        /// </summary>
        public string? SheetName { get; set; }

        /// <summary>
        /// Gets or sets current version.
        /// </summary>
        public int CurrentVersion { get; set; }

        /// <summary>
        /// Gets or Sets Time at which the sheet was uploaded.
        /// </summary>
        public DateTime UploadTime { get; set; }
    }
}