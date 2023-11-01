// <copyright file="TechnicalAreaViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress constructor error.
#pragma warning disable CS8618

namespace WHO.NAPHS.BusinessLogic.ViewModels.Excel
{
    /// <summary>
    /// Technical area view model for assessment import process.
    /// </summary>
    public class TechnicalAreaViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicalAreaViewModel"/> class.
        /// </summary>
        public TechnicalAreaViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicalAreaViewModel"/> class.
        /// </summary>
        /// <param name="index"> Index.</param>
        /// <param name="originalValue"> Original value.</param>
        /// <param name="value"> Value.</param>
        /// <param name="areaCode"> Area code. </param>
        /// <param name="areaCodeId"> Area code id. </param>
        /// <param name="parentIndex"> Parent index.</param>
        public TechnicalAreaViewModel(int index, string? originalValue, string? value, string areaCode, string? areaCodeId, int? parentIndex = null)
        {
            this.Index = index;
            this.OriginalValue = originalValue;
            this.Value = value;
            this.ParentIndex = parentIndex;
            this.AreaCode = areaCode;
            this.AreaCodeId = areaCodeId;
        }

        /// <summary>
        /// Gets or sets index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets original value.
        /// </summary>
        public string? OriginalValue { get; set; }

        /// <summary>
        /// Gets or sets Value.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Gets or sets parent index for null cell values.
        /// </summary>
        public int? ParentIndex { get; set; }

        /// <summary>
        /// Gets or sets Area code.
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// Gets or sets area code id.
        /// </summary>
        public string? AreaCodeId { get; set; }
    }
}