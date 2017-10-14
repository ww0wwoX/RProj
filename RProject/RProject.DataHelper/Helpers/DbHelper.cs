using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RProject.DataHelper
{
	public class DbHelper : IDbHelper
	{
		const int COMMAND_TIMEOUT_IN_SECONDS = 4 * 60 * 60; // seconds
		static readonly string connectionString = "";

		public DbHelper()
		{

		}

		static DbHelper()
		{
			connectionString = ConfigurationManager.ConnectionStrings["RConnection"].ConnectionString;
		}

		public string Test()
		{
			return connectionString;
		}


		#region Parameters
		protected static SqlParameter GetParameter(string name, string val)
		{
			return new SqlParameter
			{
				ParameterName = name,
				Value = string.IsNullOrEmpty(val) || val.Trim().Length == 0 ? (object)DBNull.Value : val.Trim()
			};
		}

		protected static SqlParameter GetParameter<T>(string name, T? val) where T : struct
		{
			SqlParameter parameter = new SqlParameter { ParameterName = name };

			if (val.HasValue)
			{
				parameter.Value = val.Value;
			}
			else
			{
				// small hack to define SqlParameter.SqlDbType
				parameter.Value = default(T);
				SqlDbType type = parameter.SqlDbType;

				parameter.Value = DBNull.Value;
				parameter.SqlDbType = type;
			}

			return parameter;
		}

		protected static SqlParameter GetParameter<T>(string name, T val) where T : struct
		{
			return new SqlParameter(name, val);
		}

		protected static SqlParameter GetParameter(string name, byte[] val)
		{
			return new SqlParameter
			{
				ParameterName = name,
				SqlDbType = SqlDbType.Image,
				Value = val == null || val.Length == 0 ? (object)DBNull.Value : val
			};
		}
		#endregion

		#region Output fields
		protected static T? GetNullableFieldValue<T>(object value) where T : struct
		{
			return value == null || value == DBNull.Value ? default(T?) : (T?)value;
		}

		protected static string GetStringValue(object value)
		{
			return value == null || value == DBNull.Value ? null : value.ToString();
		}
		#endregion

		#region Connection
		static SqlConnection GetConnection()
		{
			return new SqlConnection(connectionString);
		}
		#endregion

		#region Command
		static SqlCommand GetCommand(string text, params SqlParameter[] parameters)
		{
			SqlConnection connection = GetConnection();

			SqlCommand command = new SqlCommand(text, connection)
			{
				CommandType = CommandType.StoredProcedure,
				CommandTimeout = COMMAND_TIMEOUT_IN_SECONDS
			};
			command.Parameters.AddRange(parameters);

			return command;
		}

		protected static int ExecuteNonQuery(string text, params SqlParameter[] parameters)
		{
			using (SqlCommand command = GetCommand(text, parameters))
			{
				// command executes in the scope of transaction
				if (command.Connection.State == ConnectionState.Open)
					return command.ExecuteNonQuery();

				using (command.Connection)
				{
					command.Connection.Open();
					return command.ExecuteNonQuery();
				}
			}
		}

		protected static T ExecuteScalarFunction<T>(string text, T defVal, params SqlParameter[] parameters)
		{
			using (SqlCommand command = GetCommand(text, parameters))
			{
				SqlParameter pRetVal = GetParameter("@RETURN_VALUE", string.Empty);
				pRetVal.Direction = ParameterDirection.ReturnValue;
				command.Parameters.Add(pRetVal);

				// command executes in the scope of transaction
				if (command.Connection.State == ConnectionState.Open)
				{
					command.ExecuteNonQuery();
				}
				else
				{
					using (command.Connection)
					{
						command.Connection.Open();
						command.ExecuteNonQuery();
					}
				}

				return pRetVal.Value == null || pRetVal.Value == DBNull.Value ? defVal : (T)pRetVal.Value;
			}
		}

		protected static SqlDataReader GetReader(string text, params SqlParameter[] parameters)
		{
			SqlCommand command = GetCommand(text, parameters);

			CommandBehavior behavior = CommandBehavior.Default;

			if (command.Connection.State != ConnectionState.Open)
			{
				command.Connection.Open();
				behavior |= CommandBehavior.CloseConnection;
			}

			var reader = command.ExecuteReader(behavior);
			return reader;
		}
		#endregion

	}
}
