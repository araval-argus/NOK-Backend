// <copyright file="StringEnumExtension.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8604

namespace WHO.NOK.Core.ResponseMiddleware
{
    using System.ComponentModel;
    using System.Globalization;

    /// <summary>
    /// String extension methods for enum.
    /// </summary>
    public static class StringEnumExtension
    {
        /// <summary>
        /// Get description.
        /// </summary>
        /// <param name="e"> Model.</param>
        /// <typeparam name="T"> Class.</typeparam>
        /// <returns> Get description from the enum.</returns>
        public static string GetDescription<T>(this T e)
                    where T : IConvertible
        {
            var description = string.Empty;

            if (e is not Enum)
            {
                return description;
            }

            var type = e.GetType();
            var values = Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val != e.ToInt32(CultureInfo.InvariantCulture))
                {
                    continue;
                }

                var memInfo = type.GetMember(type.GetEnumName(val));
                var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descriptionAttributes.Length > 0)
                {
                    description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                }

                break;
            }

            return description;
        }
    }
}