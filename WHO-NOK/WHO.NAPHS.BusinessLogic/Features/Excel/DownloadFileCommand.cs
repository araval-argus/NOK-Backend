// <copyright file="DownloadFileCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.Excel
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to get custom nbw recommendations for plan.
    /// </summary>
    public class DownloadFileCommand : Request
    {
        /// <summary>
        /// Gets or sets planning tool id.
        /// </summary>
        public int PlanningToolId { get; set; }
    }
}