// <copyright file="DBExtensions.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress space warning.
#pragma warning disable SA1011

namespace WHO.NAPHS.Infrastructure.Helper
{
    using System.Data;
    using System.Data.Common;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using WHO.NAPHS.Infrastructure.Models.DatabaseContext;

    /// <summary>
    /// Db Extensions.
    /// </summary>
    public static class DBExtensions
    {
        /// <summary>
        /// Executes a stored procedure.
        /// </summary>
        /// <param name="context"><see cref="ApplicationDbContext"/> representing Db Context.</param>
        /// <param name="commandType"><see cref="CommandType"/> enum.</param>
        /// <param name="commandText">Command Text.</param>
        /// <param name="callback">Callback delegate which will be executed before closing connection.</param>
        /// <param name="sqlParameters">List of <see cref="SqlParameter"/> parameters.</param>
        /// <param name="commandTimeout">Command time out.</param>
        /// <returns> Returns the async task. </returns>
        public static async Task ExecuteReaderAsync(this ApplicationDbContext context, CommandType commandType, string commandText, Action<DbDataReader> callback, List<SqlParameter>? sqlParameters = null, int commandTimeout = 120)
        {
            try
            {
                var connection = context.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                if (connection.State == ConnectionState.Closed)
                {
                    await context.Database.OpenConnectionAsync();
                }

                command.CommandText = commandText;
                command.CommandType = commandType;
                command.CommandTimeout = commandTimeout;
                if (sqlParameters != null && sqlParameters.Count > 0)
                {
                    foreach (var parameter in sqlParameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                var reader = await command.ExecuteReaderAsync();
                callback?.Invoke(reader);
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            {
                // AddToErrorLogs(ex, context, "ExecuteReader", sqlParameters, commandText);
            }
            finally
            {
                if (context.Database.GetDbConnection().State == ConnectionState.Open)
                {
                    await context.Database.CloseConnectionAsync();
                }
            }
        }

        /// <summary>
        /// Executes scalar.
        /// </summary>
        /// <param name="context">Current Db Context.</param>
        /// <param name="commandType"><see cref="CommandType"/> enum.</param>
        /// <param name="commandText">Command Text.</param>
        /// <param name="sqlParameters">List of <see cref="SqlParameter"/> parameters.</param>
        /// <param name="commandTimeout">Command time out.</param>
        /// <returns> Object depending upon the type of the execution.</returns>
        public static async Task<object?> ExecuteScalarAsync(this ApplicationDbContext context, CommandType commandType, string commandText, List<SqlParameter>? sqlParameters = null, int commandTimeout = 120)
        {
            object? returnValue = null;
            try
            {
                var connection = context.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                if (connection.State == ConnectionState.Closed)
                {
                    await context.Database.OpenConnectionAsync();
                }

                command.CommandText = commandText;
                command.CommandType = commandType;
                command.CommandTimeout = commandTimeout;
                if (sqlParameters != null && sqlParameters.Count > 0)
                {
                    foreach (var parameter in sqlParameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                returnValue = await command.ExecuteScalarAsync();
            }
            catch (Exception)
            {
                // AddToErrorLogs(ex, context, "ExecuteScalar", sqlParameters, commandText);
            }
            finally
            {
                if (context.Database.GetDbConnection().State == ConnectionState.Open)
                {
                    await context.Database.CloseConnectionAsync();
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Executes NonQuery.
        /// </summary>
        /// <param name="context">Current Db Context.</param>
        /// <param name="commandType"><see cref="CommandType"/> enum.</param>
        /// <param name="commandText">Command Text.</param>
        /// <param name="sqlParameters">List of <see cref="SqlParameter"/> parameters.</param>
        /// <param name="commandTimeout">Command time out.</param>
        /// <returns>Affected rows.</returns>
        public static async Task<int> ExecuteNonQueryAsync(this ApplicationDbContext context, CommandType commandType, string commandText, List<SqlParameter>? sqlParameters = null, int commandTimeout = 120)
        {
            int affectedRows = 0;
            try
            {
                var connection = context.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                if (connection.State == ConnectionState.Closed)
                {
                    await context.Database.OpenConnectionAsync();
                }

                command.CommandText = commandText;
                command.CommandType = commandType;
                command.CommandTimeout = commandTimeout;
                if (sqlParameters != null && sqlParameters.Count > 0)
                {
                    foreach (var parameter in sqlParameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                affectedRows = await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                // AddToErrorLogs(ex, context, "ExecuteNonQuery", sqlParameters, commandText);
            }
            finally
            {
                if (context.Database.GetDbConnection().State == ConnectionState.Open)
                {
                    await context.Database.CloseConnectionAsync();
                }
            }

            return affectedRows;
        }

        /// <summary>
        /// Executes a stored procedure.
        /// </summary>
        /// <param name="context">Current Db Context.</param>
        /// <param name="commandType"><see cref="CommandType"/> enum.</param>
        /// <param name="commandText">Command Text.</param>
        /// <param name="callback">Callback after filling dataset.</param>
        /// <param name="sqlParameters">List of <see cref="SqlParameter"/> parameters.</param>
        /// <param name="commandTimeout">Command time out.</param>
        /// <returns> Returns the async task.</returns>
        public static async Task ExecuteProcedureAsync(this ApplicationDbContext context, CommandType commandType, string commandText, Action<DataSet> callback, List<SqlParameter>? sqlParameters = null, int commandTimeout = 120)
        {
            var ds = new DataSet();

            try
            {
                var connection = new SqlConnection(context.Database.GetDbConnection().ConnectionString);
                using SqlCommand command = connection.CreateCommand();
                if (connection.State == ConnectionState.Closed)
                {
                    await context.Database.OpenConnectionAsync();
                }

                command.CommandText = commandText;
                command.CommandType = commandType;
                command.CommandTimeout = commandTimeout;
                if (sqlParameters != null && sqlParameters.Count > 0)
                {
                    foreach (var parameter in sqlParameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                using var adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                adapter.Dispose();
                callback?.Invoke(ds);
                ds.Dispose();
            }
            catch (Exception)
            {
                // AddToErrorLogs(ex, context, "ExecuteProcedure", sqlParameters, commandText);
            }
            finally
            {
                if (context.Database.GetDbConnection().State == ConnectionState.Open)
                {
                    await context.Database.CloseConnectionAsync();
                }
            }
        }

        /// <summary>
        /// Returns Object List.
        /// </summary>
        /// <typeparam name="T">Type of object that you want to convert.</typeparam>
        /// <param name="dt">Data table. </param>
        /// <param name="ignoreList">Properties which you want to ignore.</param>
        /// <returns>Object List.</returns>
        public static List<T> ConvertToList<T>(this DataTable dt, string[]? ignoreList = null)
        {
            ignoreList ??= Array.Empty<string>();

            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();

            var properties = typeof(T).GetProperties();

            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();

                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name) && !ignoreList.Contains(pro.Name))
                    {
                        if (row[pro.Name] == DBNull.Value)
                        {
                            pro.SetValue(objT, null);
                        }
                        else
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                    }
                }

                return objT;
            }).ToList();
        }

        /// <summary>
        /// Checks whether the data record has column or not.
        /// </summary>
        /// <param name="dr">Data record.</param>
        /// <param name="columnName">Column Name.</param>
        /// <returns>A Boolean indicating whether record has column or not.</returns>
        public static bool HasColumn(this IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
