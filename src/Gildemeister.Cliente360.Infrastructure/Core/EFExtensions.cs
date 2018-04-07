using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Infrastructure
{
    public static class EFExtensions
    {
        /// <summary>
        /// Creates an initial DbCommand object based on a stored procedure name
        /// </summary>
        /// <param name="context">target database context</param>
        /// <param name="storedProcName">target procedure name</param>
        /// <param name="prependDefaultSchema">Prepend the default schema name to <paramref name="storedProcName"/> if explicitly defined in <paramref name="context"/></param>
        /// <returns></returns>
        public static DbCommand LoadStoredProc(this DbContext context, string storedProcName, bool prependDefaultSchema = true)
        {
            var cmd = context.Database.GetDbConnection().CreateCommand();
            if (prependDefaultSchema)
            {
                var schemaName = context.Model.Relational().DefaultSchema;
                if (schemaName != null)
                {
                    storedProcName = $"{schemaName}.{storedProcName}";
                }

            }
            cmd.CommandText = storedProcName;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }



        /// <summary>
        /// Creates a DbParameter object and adds it to a DbCommand
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public static DbCommand WithSqlParam(this DbCommand cmd, string paramName, object paramValue, Action<DbParameter> configureParam = null)
        {
            if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != CommandType.StoredProcedure)
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            var param = cmd.CreateParameter();

            param.ParameterName = paramName;
            param.Value = paramValue;
            configureParam?.Invoke(param);
            cmd.Parameters.Add(param);


            return cmd;
        }

        /// <summary>
        /// Datatable parameters
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <param name="configureParam"></param>
        /// <returns></returns>


        public static DbCommand WithSqlParam(this DbCommand cmd, string paramName, Action<DbParameter> configureParam = null)
        {
            if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != CommandType.StoredProcedure)
                throw new InvalidOperationException("Call LoadStoredProc before using this method");


            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            configureParam?.Invoke(param);
            cmd.Parameters.Add(param);

            return cmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="storedProc"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>

        public static async Task<object[]> ExecuteNonQueryAsync(this DbContext context, string storedProc, Dictionary<string, StoredProcedure> parameters)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            List<string> list = new List<string>();
            int returnValuesCount = 0;

            foreach (KeyValuePair<string, StoredProcedure> entry in parameters)
            {
                SqlParameter sqlParameter = new SqlParameter(entry.Key, entry.Value.Value == null ? DBNull.Value : entry.Value.Value);
                if (entry.Value.IsOutput)
                {
                    sqlParameter.Direction = ParameterDirection.Output;
                    sqlParameter.Size = 50;
                    list.Add("@" + entry.Key + " output");
                    returnValuesCount++;
                }
                else
                    list.Add("@" + entry.Key);

                sqlParameterList.Add(sqlParameter);
            }

            string parameterString = string.Join(",", list.ToArray());

            context.Database.GetCommandTimeout();
            await context.Database.ExecuteSqlCommandAsync(storedProc + (String.IsNullOrEmpty(parameterString) ? "" : " " + parameterString), sqlParameterList.Cast<object>().ToArray());


            if (returnValuesCount > 0)
            {
                object[] returnValues = new object[returnValuesCount];
                int index = 0;
                foreach (SqlParameter sqlParameter in sqlParameterList)
                {
                    if (sqlParameter.Direction == ParameterDirection.Output)
                    {
                        returnValues[index] = sqlParameter.Value;
                        index++;
                    }
                }

                return returnValues;
            }

            return null;
        }

        public static object[] ExecuteNonQuery(this DbContext context, string storedProc,
            Dictionary<string, StoredProcedure> parameters)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            List<string> list = new List<string>();
            int returnValuesCount = 0;

            foreach (KeyValuePair<string, StoredProcedure> entry in parameters)
            {
                SqlParameter sqlParameter = new SqlParameter(entry.Key, entry.Value.Value);
                if (entry.Value.IsOutput)
                {
                    sqlParameter.Direction = ParameterDirection.Output;
                    sqlParameter.Size = 50;
                    list.Add("@" + entry.Key + " output");
                    returnValuesCount++;
                }
                else
                    list.Add("@" + entry.Key);

                sqlParameterList.Add(sqlParameter);
            }

            string parameterString = string.Join(",", list.ToArray());

            context.Database.GetCommandTimeout();
            context.Database.ExecuteSqlCommand(storedProc + (String.IsNullOrEmpty(parameterString) ? "" : " " + parameterString), sqlParameterList.Cast<object>().ToArray());


            if (returnValuesCount > 0)
            {
                object[] returnValues = new object[returnValuesCount];
                int index = 0;
                foreach (SqlParameter sqlParameter in sqlParameterList)
                {
                    if (sqlParameter.Direction == ParameterDirection.Output)
                    {
                        returnValues[index] = sqlParameter.Value;
                        index++;
                    }
                }

                return returnValues;
            }

            return null;
        }


        public static IList<T> ListStoreProc<T>(this DbContext context, string storedProc)
        {
            List<T> listaEntidad = new List<T>();
            

            return listaEntidad;

        }

        public class SprocResults
        {

            //  private DbCommand _command;
            private DbDataReader _reader;

            public SprocResults(DbDataReader reader)
            {
                // _command = command;
                _reader = reader;
            }

            public IList<T> ReadToList<T>()
            {
                return MapToList<T>(_reader);
            }

            public T? ReadToValue<T>() where T : struct
            {
                return MapToValue<T>(_reader);
            }

            public Task<bool> NextResultAsync()
            {
                return _reader.NextResultAsync();
            }

            public Task<bool> NextResultAsync(CancellationToken ct)
            {
                return _reader.NextResultAsync(ct);
            }

            public bool NextResult()
            {
                return _reader.NextResult();
            }

            /// <summary>
            /// Retrieves the column values from the stored procedure and maps them to <typeparamref name="T"/>'s properties
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="dr"></param>
            /// <returns>IList<<typeparamref name="T"/>></returns>
            private IList<T> MapToList<T>(DbDataReader dr)
            {
                var objList = new List<T>();
                var props = typeof(T).GetRuntimeProperties();

                var colMapping = dr.GetColumnSchema()
                    .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                    .ToDictionary(key => key.ColumnName.ToLower());

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        T obj = Activator.CreateInstance<T>();
                        foreach (var prop in props)
                        {
                            bool column = ColumnExists(dr, prop.Name.ToLower());
                            if (column)
                            {
                                var val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                                prop.SetValue(obj, val == DBNull.Value ? null : val);
                            }
                        }
                        objList.Add(obj);
                    }
                }


                return objList;
            }

            /// <summary>
            /// Validate column exist on the Query
            /// </summary>
            /// <param name="reader"></param>
            /// <param name="columnName"></param>
            /// <returns></returns>
            public bool ColumnExists(IDataReader reader, string columnName)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                        return true;

                return false;
            }

            /// <summary>
            ///Attempts to read the first value of the first row of the resultset.
            /// </summary>
            private T? MapToValue<T>(DbDataReader dr) where T : struct
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        return dr.IsDBNull(0) ? new T?() : new T?(dr.GetFieldValue<T>(0));
                    }
                }
                return new T?();
            }

        }

        /// <summary>
        /// Executes a DbDataReader and returns a list of mapped column values to the properties of <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public static void ExecuteStoredProc(this DbCommand command, Action<SprocResults> handleResults, System.Data.CommandBehavior commandBehaviour = System.Data.CommandBehavior.Default)
        {
            if (handleResults == null)
            {
                throw new ArgumentNullException(nameof(handleResults));
            }

            using (command)
            {
                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using (var reader = command.ExecuteReader(commandBehaviour))
                    {
                        var sprocResults = new SprocResults(reader);
                        // return new SprocResults();
                        handleResults(sprocResults);
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        /// <summary>
        /// Executes a DbDataReader asynchronously and returns a list of mapped column values to the properties of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public async static Task ExecuteStoredProcAsync(this DbCommand command, Action<SprocResults> handleResults,
            CommandBehavior commandBehaviour = CommandBehavior.Default, CancellationToken ct = default(CancellationToken))
        {
            if (handleResults == null)
            {
                throw new ArgumentNullException(nameof(handleResults));
            }

            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    await command.Connection.OpenAsync(ct).ConfigureAwait(false);
                try
                {
                    using (var reader = await command.ExecuteReaderAsync(commandBehaviour, ct).ConfigureAwait(false))
                    {
                        var sprocResults = new SprocResults(reader);
                        handleResults(sprocResults);
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }



    }
}

