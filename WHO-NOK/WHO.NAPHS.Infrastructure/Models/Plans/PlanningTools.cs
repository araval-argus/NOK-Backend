// <copyright file="PlanningTools.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Plan indicator DB model.
    /// </summary>
    public class PlanningTools : ITrackable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanningTools"/> class.
        /// </summary>
        /// <param name="fileName"> Name of the file that has been uploaded.</param>
        /// <param name="filePath"> Path of the file in which file has been stored.</param>
        /// <param name="excelTypeId"> <see cref="ExcelTypes"/> enum representing type of excel file.</param>
        /// <param name="countryId"> Country id.</param>
        /// <param name="countryPlanId"> Country Plan id.</param>
        public PlanningTools(string fileName, string filePath, ExcelTypes excelTypeId, int? countryId, int? countryPlanId)
        {
            this.FileName = fileName;
            this.FilePath = filePath;
            this.ExcelTypeId = excelTypeId;
            this.CountryId = countryId;
            this.CountryPlanId = countryPlanId;
        }

        /// <summary>
        /// Gets or sets planning tool id.
        /// </summary>
        [Key]
        public int PlanningToolId { get; protected set; }

        /// <summary>
        /// Gets or sets file name.
        /// </summary>
        public string FileName { get; protected set; }

        /// <summary>
        /// Gets or sets file path.
        /// </summary>
        public string FilePath { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="ExcelTypes"/> enum.
        /// </summary>
        public ExcelTypes ExcelTypeId { get; protected set; }

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int? CountryPlanId { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc/>
        public int CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime LastUpdatedAt { get; set; }

        /// <inheritdoc/>
        public int LastUpdatedBy { get; set; }
    }
}