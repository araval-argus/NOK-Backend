// <copyright file="ExcelMapping.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress constructor error.
#pragma warning disable CS8618

namespace WHO.NAPHS.Infrastructure.Models.Excel
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Excel mapping DB Model.
    /// </summary>
    public class ExcelMapping
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets <see cref="ExcelTypes"/> enum.
        /// </summary>
        public ExcelTypes ExcelType { get; set; }

        /// <summary>
        /// Gets or sets excel column.
        /// </summary>
        public string ExcelColumn { get; set; }

        /// <summary>
        /// Gets or sets Database field name.
        /// </summary>
        public string DatabaseField { get; set; }

        /// <summary>
        /// Gets or sets Table name.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether first row is column name or not.
        /// </summary>
        public bool IsFirstRowColumnName { get; set; } = true;

        /// <summary>
        /// Gets or sets <see cref="MappingTypes"/> enum.
        /// </summary>
        public MappingTypes MappingType { get; set; }

        /// <summary>
        /// Gets or sets sheet name.
        /// </summary>
        public string SheetName { get; set; }
    }
}