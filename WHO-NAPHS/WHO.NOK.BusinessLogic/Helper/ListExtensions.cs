// <copyright file="ListExtensions.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Helper
{
    using System.Collections.Generic;
    using System.Reflection;
    using WHO.NOK.BusinessLogic.ViewModels.PaginatedResponses;
    using WHO.NOK.Core.Wrappers;

    /// <summary>
    /// Extension class of List.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Sorts and paginates the <see cref="T"/> in the list.
        /// </summary>
        /// <typeparam name="T">Generic model class T.</typeparam>
        /// <param name="model">Model.</param>
        /// <param name="filter"><see cref="PaginationFilter"/> object.</param>
        /// <param name="filterParamsList">List of filter parameters.</param>
        /// <returns>Returns the instance of <see cref="PaginatedResponse"/> class.</returns>
        /// <exception cref="ApiException">ApiException.</exception>
        public static async Task<PaginatedResponse<T>> SortAndPaginateAsync<T>(this List<T> model, PaginationFilter filter, List<FilterParams<T>>? filterParamsList = null)
        {
            PaginatedResponse<T> response = new ();

            if (model.Count == 0)
            {
                return response;
            }

            // Sorting Part.
            if (!string.IsNullOrEmpty(filter.OrderBy))
            {
                // To check whether column is valid or not.
                model[0].IsNestedPropertyValid(filter.OrderBy);

                model = filter.OrderByAscending ? model.OrderBy(x => GetNestedPropertyValue(x, filter!.OrderBy)).ToList() :
                        model.OrderByDescending(x => GetNestedPropertyValue(x, filter!.OrderBy)).ToList();
            }

            if (filterParamsList != null && filterParamsList.Count > 0)
            {
                foreach (var filterParams in filterParamsList)
                {
                    if (filterParams.FilterFunc.Count == 0 && model.Count > 0)
                    {
                        // To check whether column is valid or not.
                        model[0].IsNestedPropertyValid(filterParams.Column);
                        model = model.Where(x => filterParams.Values.Contains(GetNestedPropertyValue(x!, filterParams.Column, true) !)).ToList();
                    }
                    else
                    {
                        List<T> temp = new ();
                        foreach (var predicate in filterParams.FilterFunc)
                        {
                            temp.AddRange(model.Where(x => predicate(x)).ToList());
                        }

                        model = temp;
                    }
                }
            }

            if (filter.PageIndex <= 0)
            {
                filter.PageIndex = 1;
            }

            int skippedRows = (filter.PageIndex - 1) * filter.PageSize;

            if (skippedRows > model.Count)
            {
                throw new ApiException("Skipped records are more then total records.");
            }

            filter.PageSize = filter.PageSize <= 0 ? 10 : Math.Min(10, filter.PageSize);

            response.TotalRows = model.Count;

            response.Data = filter.PageSize > 0 ?
                model.Skip(skippedRows).Take(filter.PageSize).ToList() :
                model.Skip(skippedRows).ToList();

            return await Task.FromResult(response);
        }

        /// <summary>
        /// Gets the nested property value.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="t">Type of T.</param>
        /// <param name="propertyPath">Path of the property.</param>
        /// <param name="forCompare">A value indicating whether the value to be returned is for compare or not.</param>
        /// <returns>Returns the value of the property.</returns>
        private static object? GetNestedPropertyValue<T>(this T t, string propertyPath, bool forCompare = false)
        {
            string[] properties = propertyPath.Split(".");
            object? result = t;

            foreach (string property in properties)
            {
                result = result?.GetType()?.GetProperty(property)?.GetValue(result, null);
                if (result == null)
                {
                    return forCompare ? string.Empty : null;
                }
            }

            return forCompare ? result!.ToString() : result;
        }

        /// <summary>
        /// Gets the nested property.
        /// </summary>
        /// <typeparam name="T">Type T.</typeparam>
        /// <param name="t">current object.</param>
        /// <param name="propertyPath">Path of the property.</param>
        /// <exception cref="Exception">throws an exception if the object does not contain the key specified in propertyPath.</exception>
        private static void IsNestedPropertyValid<T>(this T t, string propertyPath)
        {
            string[] properties = propertyPath.Split(".");

            object result = t ?? throw new Exception("Current object is null");

            foreach (string property in properties)
            {
                result = result.GetType().GetProperty(property) ?? throw new Exception("Invalid column");
            }
        }
    }
}