// <copyright file="CommonMethods.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Core.Common
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Common methods.
    /// </summary>
    public static class CommonMethods
    {
        /// <summary>
        /// Remove alphabetic chars from the string.
        /// </summary>
        /// <param name="input"> Input string.</param>
        /// <returns> Returns the non alphabetic chars.</returns>
        public static string RemoveAlphabeticCharacters(string input)
        {
            string pattern = "[a-zA-Z]";
            string replacement = string.Empty;
            return Regex.Replace(input, pattern, replacement);
        }

        /// <summary>
        /// Get technical area id.
        /// </summary>
        /// <param name="technicalArea"> Technical area.</param>
        /// <returns> Returns the technical area id.</returns>
        public static string GetTechnicalAreaCode(string technicalArea)
        {
            if (!string.IsNullOrEmpty(technicalArea))
            {
                string value = technicalArea.Split('.')[0];
                string? numberFromString = ExtractIntegersFromString(value);
                return !string.IsNullOrEmpty(numberFromString) ? value.Replace(numberFromString, string.Empty) : value;
            }

            return string.Empty;
        }

        /// <summary>
        /// Extract numbers from the string.
        /// </summary>
        /// <param name="inputString"> Input string.</param>
        /// <returns> Returns the number from the string.</returns>
        public static string? ExtractIntegersFromString(string inputString)
        {
            return (!string.IsNullOrEmpty(inputString)) ? string.Concat(inputString.Where(char.IsNumber)) : null;
        }

        /// <summary>
        /// Get technical area code id.
        /// </summary>
        /// <param name="technicalArea"> Technical area.</param>
        /// <returns> Returns the technical area code id.</returns>
        public static string? GetTechnicalAreaCodeId(string technicalArea)
        {
            if (!string.IsNullOrEmpty(technicalArea))
            {
                return ExtractIntegersFromString(technicalArea.Split('.')[0]);
            }

            return null;
        }

        /// <summary>
        /// Find indicator from technical area and indicator.
        /// </summary>
        /// <param name="technicalArea"> Technical Area.</param>
        /// <param name="indicator"> Indicator.</param>
        /// <returns> Returns the indicator id.</returns>
        public static string FindIndicatorId(string technicalArea, string indicator)
        {
            if (technicalArea.Contains('.'))
            {
                string strIndex = technicalArea[..technicalArea.IndexOf(".")];
                if (char.IsDigit(strIndex[strIndex.Length - 1]))
                {
                    strIndex = $"{strIndex}.";
                }

                string firstWord = indicator.Split(' ')[0];
                int pos = firstWord.IndexOf(strIndex);
                if (pos == 0)
                {
                    string val = firstWord.Replace(strIndex, string.Empty);
                    int value;

                    if (int.TryParse(val, out value))
                    {
                        return RemoveAlphabeticCharacters(firstWord);
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get technical area name.
        /// </summary>
        /// <param name="technicalAreaName"> Technical area name.</param>
        /// <param name="areaCode"> Area code.</param>
        /// <param name="areaCodeId"> Area code id.</param>
        /// <returns> Returns the area code.</returns>
        public static string? GetTechnicalAreaName(string technicalAreaName, string areaCode, string? areaCodeId)
        {
            if (string.IsNullOrEmpty(technicalAreaName))
            {
                return null;
            }

            technicalAreaName = technicalAreaName
                .ReplaceFirstAppearance(areaCode, string.Empty)
                .ReplaceFirstAppearance(".", string.Empty);

            if (!string.IsNullOrEmpty(areaCodeId))
            {
                technicalAreaName = technicalAreaName.Replace(areaCodeId, string.Empty);
            }

            return technicalAreaName.Trim();
        }

        /// <summary>
        /// Get indicator code.
        /// </summary>
        /// <param name="indicatorName"> Indicator name.</param>
        /// <param name="indicatorId"> Indicator Id.</param>
        /// <returns> Returns the indicator code.</returns>
        public static string? GetIndicatorCode(string indicatorName, string? indicatorId)
        {
            if (string.IsNullOrEmpty(indicatorName))
            {
                return null;
            }

            if (!string.IsNullOrEmpty(indicatorId))
            {
                int? index = indicatorName.IndexOf(indicatorId);

                if (index.GetValueOrDefault() != -1)
                {
                    return indicatorName.Substring(0, index.GetValueOrDefault());
                }
            }

            return indicatorName;
        }

        /// <summary>
        /// Get indicator name.
        /// </summary>
        /// <param name="indicatorName"> Indicator name.</param>
        /// <param name="indicatorId"> Indicator Id.</param>
        /// <param name="indicatorCode"> Indicator code.</param>
        /// <returns> Returns the indicator name.</returns>
        public static string? GetIndicatorName(string indicatorName, string? indicatorId, string? indicatorCode)
        {
            if (string.IsNullOrEmpty(indicatorName))
            {
                return null;
            }

            indicatorName = indicatorName
                    .ReplaceFirstAppearance(
                        string.IsNullOrEmpty(indicatorId) ? string.Empty : indicatorId,
                        string.Empty)
                    .ReplaceFirstAppearance(
                        string.IsNullOrEmpty(indicatorCode) ? string.Empty : indicatorCode,
                        string.Empty);

            return indicatorName.Trim();
        }

        /// <summary>
        /// Replace First Appearance of the string.
        /// </summary>
        /// <param name="originalString"> Original string.</param>
        /// <param name="substringToReplace"> String to replace.</param>
        /// <param name="replacement"> Replacement.</param>
        /// <returns> Returns the updated string.</returns>
        public static string ReplaceFirstAppearance(this string originalString, string substringToReplace, string replacement)
        {
            int index = originalString.IndexOf(substringToReplace);
            if (index != -1)
            {
                return originalString.Remove(index, substringToReplace.Length).Insert(index, replacement);
            }

            return originalString;
        }
    }
}