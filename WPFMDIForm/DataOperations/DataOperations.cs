using System;
using System.Data.SqlClient;

namespace WPFMDIForm
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Windows;
    using WPFMDIForm.JKHModel;

	public class DataOperations
	{
		private DataTable dataTable;
		public DataTable GetDataTable()
		{
			return dataTable;
		}
		/// <summary>
		/// Load data, rather than use DataTable.Load we cycle through rows else the Load method 
		/// will mark the primary key as read-only which means no way to set the primary key and
		/// would cause us to reload the data and set the current record.
		/// </summary>
		/// <remarks></remarks>
		public DataOperations(String tableName)
		{

			this.dataTable = new DataTable();
			this.dataTable.Columns.Add(new DataColumn { ColumnName = "Identifier", DataType = typeof(int) });
			this.dataTable.Columns.Add(new DataColumn { ColumnName = "CompanyName", DataType = typeof(string) });
			this.dataTable.Columns.Add(new DataColumn { ColumnName = "ContactName", DataType = typeof(string) });
			this.dataTable.Columns.Add(new DataColumn { ColumnName = "ContactTitle", DataType = typeof(string) });

			ReadRows(CommandType.Text,
				"SELECT Identifier, CompanyName, ContactName, ContactTitle FROM " + tableName + " ORDER BY Identifier",
				reader =>
				{
					this.dataTable.Rows.Add(new object[] { reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) });
				});


			this.dataTable.AcceptChanges();

		}
		/// <summary>
		/// Remove a single row from Customer table
		/// </summary>
		/// <param name="Identifier"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool RemoveRow(int Identifier)
		{
			bool Result = false;
			using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
			{
				using (SqlCommand cmd = new SqlCommand { Connection = cn, CommandText = "DELETE FROM Customer WHERE Identifier = " + Identifier.ToString() })
				{
					cn.Open();
					Result = (cmd.ExecuteNonQuery() == 1);
				}
			}
			return Result;
		}
		/// <summary>
		/// Responsible for removing more than one row.
		/// 
		/// Options
		///  - pass in selected rows from the DataGridView, get identifier and do delete
		///  - add a checkbox column, pass in rows selected and use identifier to delete
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Rather than present one method of the above none are done as it shouldf be
		/// easy enough for you the reader to implement
		/// </remarks>
		public bool RemoveRows()
		{
			throw new NotImplementedException("TODO");
		}
		private string UpdateStatement = "UPDATE Customer SET CompanyName = @CompanyName, ContactName = @ContactName,ContactTitle = @ContactTitle WHERE Identifier = @Identifier";
		/// <summary>
		/// Responsible for updating rows that have 
		/// Row.RowState = Modified
		/// 
		/// Use the same logic as done in the add method below.
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		/// <remarks>
		/// Once you understand the logic in add rows you will be capable
		/// to implement this method
		/// </remarks>
		public bool UpdateRow(DataRow row)
		{
			bool Result = false;
			using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
			{
				using (SqlCommand cmd = new SqlCommand { Connection = cn })
				{
					cmd.CommandText = UpdateStatement;
					cmd.Parameters.AddWithValue("@CompanyName", row.Field<string>("CompanyName"));
					cmd.Parameters.AddWithValue("@ContactName", row.Field<string>("ContactName"));
					cmd.Parameters.AddWithValue("@ContactTitle", row.Field<string>("ContactTitle"));
					cmd.Parameters.AddWithValue("@Identifier", row.Field<int>("Identifier"));
					cn.Open();
					try
					{
						if (Convert.ToInt32(cmd.ExecuteNonQuery()) == 1)
						{
							Result = true;
						}
					}
					catch (Exception)
					{
						return false;
					}
				}
			}
			return Result;
		}
		public bool UpdateRow(int Identifier, string CompanyName, string ContactName, string ContactTitle)
		{
			bool Result = false;
			using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
			{
				using (SqlCommand cmd = new SqlCommand { Connection = cn })
				{
					cmd.CommandText = UpdateStatement;
					cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
					cmd.Parameters.AddWithValue("@ContactName", ContactName);
					cmd.Parameters.AddWithValue("@ContactTitle", ContactTitle);
					cmd.Parameters.AddWithValue("@Identifier", Identifier);
					cn.Open();
					try
					{
						if (Convert.ToInt32(cmd.ExecuteNonQuery()) == 1)
						{
							Result = true;
						}
					}
					catch (Exception)
					{
						return false;
					}
				}
			}
			return Result;
		}
		public bool UpdateRows(DataTable table)
		{
			throw new NotImplementedException("TODO");
		}
		/// <summary>
		/// Add new rows from the DataTable passed to us
		/// </summary>
		/// <param name="table"></param>
		/// <remarks></remarks>
		public void AddCustomers(DataTable table)
		{
			foreach (DataRow row in table.Rows)
			{
				if (row.RowState == DataRowState.Added)
				{
					if (!(string.IsNullOrWhiteSpace(row.Field<string>("CompanyName"))))
					{
						int NewIdentifier = 0;
						if (AddNewCustomer(row.Field<string>("CompanyName"), row.Field<string>("ContactName"), row.Field<string>("ContactTitle"), ref NewIdentifier))
						{
							row.SetField<int>("Identifier", NewIdentifier);
						}
					}

				}
			}

			table.AcceptChanges();

		}
		private string InsertStatement = "INSERT INTO Customer (CompanyName,ContactName,ContactTitle) VALUES (@CompanyName,@ContactName,@ContactTitle); SELECT CAST(scope_identity() AS int);";
		/// <summary>
		/// Called from AddCustomers to add a single new record
		/// </summary>
		/// <param name="CompanyName"></param>
		/// <param name="ContactName"></param>
		/// <param name="ContactTitle"></param>
		/// <param name="NewIdentifier"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool AddNewCustomer(string CompanyName, string ContactName, string ContactTitle, ref int NewIdentifier)
		{
			using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
			{
				using (SqlCommand cmd = new SqlCommand { Connection = cn })
				{
					cmd.CommandText = InsertStatement;
					cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
					cmd.Parameters.AddWithValue("@ContactName", ContactName);
					cmd.Parameters.AddWithValue("@ContactTitle", ContactTitle);
					cn.Open();
					try
					{
						NewIdentifier = Convert.ToInt32(cmd.ExecuteScalar());
						return true;
					}
					catch (Exception)
					{
						return false;
					}
				}
			}
		}

		public static bool DropTable(string tableName)
		{
			//bool result = ExecuteCommand("DROP TABLE IF EXISTS " + tableName + ";");
			bool result = ExecuteCommand("if exists (select * from sys.tables where name = N'" + tableName + "') drop table " + tableName);
			//bool result = ExecuteCommand("DROP TABLE " + tableName + ";");
			return result;
		}
		public static bool CreateTable(string tableName, String[][] parameters, bool dropPreviousTable = true)
		{
			if (dropPreviousTable)
				DropTable(tableName);

			String command = "CREATE TABLE \"" + tableName + "\" (";
			bool first = true;
			foreach (var parameter in parameters)
			{
				if (!first)
					command += " , ";
				first = false;

				command += "\"" + parameter[0] + "\" " + parameter[1];
			}
			command += " );";

			bool result = ExecuteCommand(command);
			return result;
		}

		public static bool InsertIntoTable(string tableNameWithParameterNames, String[][] rows)
		{
			String command = "INSERT INTO " + tableNameWithParameterNames + " VALUES ";
			bool firstRow = true;
			foreach (String[] row in rows)
			{
				if (!firstRow)
					command += " , ";

				firstRow = false;

				bool first = true;
				command += "(";

				foreach (String value in row)
				{
					if (!first)
						command += " , ";
					first = false;

					command += "'" + value + "'";
				}
				command += ")";
			}
			command += ";";

			bool result = ExecuteCommand(command);
			return result;
		}

		public static int GetNumberOfRows(string tableName)
		{
			int result = 0;
			ReadRows(CommandType.Text,
				"SELECT Identifier, CompanyName, ContactName, ContactTitle FROM " + tableName + " ORDER BY Identifier",
				reader =>
				{
					result++;
				});
			return result;
		}

		public static String GetRowsDump(string tableName)
		{
			String result = String.Empty;
			ReadRows(CommandType.Text, 
				"SELECT Identifier, CompanyName, ContactName, ContactTitle FROM " + tableName + " ORDER BY Identifier",
				reader =>
				{
					result += reader.GetInt32(0) + ", ";
					result += reader.GetString(1) + ", ";
					result += reader.GetString(2) + ", ";
					result += reader.GetString(3) + "\n";
				});
			return result;
		}
		public static String GetRowsOmniDump(string tableName)
		{
			String result = String.Empty;
			ReadRows(CommandType.Text, 
				"SELECT * FROM " + tableName + " ORDER BY Identifier",
				reader =>
				{
					for (int column = 0; column < reader.FieldCount; column++)
					{
						object obj = reader[column];
						result += obj.ToString()
							+ (column == reader.FieldCount - 1 ? "\n" : ", ");
					}
				});
			return result;
		}

		public static String GetFirstContactName(string tableName)
		{
			String result = String.Empty;
			ReadRows(CommandType.Text, 
				"SELECT ContactName FROM " + tableName + " ORDER BY Identifier",
				reader =>
				{
					if (result == String.Empty)
						result = reader.GetString(0);
				});
			return result;
		}
		public static int GetIdentifiersSumByContactName(string tableName, string contactName)
		{
			int result = 0;
			ReadRows(CommandType.Text, 
				"SELECT Identifier FROM " + tableName + " WHERE ContactName='" + contactName + "' ORDER BY Identifier",
				reader =>
				{
					int identifier = reader.GetInt32(0);
					result += identifier;
				});
			return result;
		}



		public static bool ExecuteCommand(string command/*, String[][] parameters*/)
		{
			bool result = false;
			using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
			{
				using (SqlCommand cmd = new SqlCommand { Connection = cn })
				{
					cmd.CommandText = command;
					/*foreach (var parameter in parameters)
					{
						cmd.Parameters.AddWithValue("@" + parameter[0], parameter[1]);
					}*/

					try
					{
						cn.Open();
						int lines = cmd.ExecuteNonQuery();
						if (lines == 1)
						{
							result = true;
						}
						else
							MessageBox.Show("Failed to execute query '" + command + "' lines=" + lines);
					}
					catch (Exception e)
					{
						MessageBox.Show("Failed to execute '" + command + "': " + e.ToString());
						return false;
					}
				}
			}
			return result;
		}

		public static void Read(CommandType commandType, string command, Action<SqlDataReader> action)
		{
			using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
			{
				using (SqlCommand cmd = new SqlCommand { Connection = cn })
				{
					cmd.CommandType = commandType;
					cmd.CommandText = command;

                    var param = new SqlParameter("@period", 14);

                    cmd.Parameters.Add(param);

					SqlDataReader result = null;
					try
					{
						cn.Open();
						result = cmd.ExecuteReader();
					}
					catch (Exception e)
					{
						MessageBox.Show("Failed to read '" + e.ToString() + "'");
					}

					action(result);
				}
			}
		}

		public static void ReadRows(CommandType commandType, string command, Action<SqlDataReader> action)
		{
			Read(commandType, command,
				reader =>
				{
					if (reader != null && reader.HasRows)
					{
						while (reader.Read())
						{
							action(reader);
						}
					}
				});
		}

		public static readonly String TypeId = "INTEGER PRIMARY KEY AUTOINCREMENT";
		public static readonly String TypeInt = "INTEGER DEFAULT 0";
		public static readonly String TypeString = "VARCHAR";

        private static String _connectionString = null;
		private static String ConnectionString
		{
			get
			{
                if (!string.IsNullOrEmpty(_connectionString))
                    return _connectionString;
                else
                {
                    using (var context = new JKHModelContainer())
                    {
                        _connectionString = context.Database.Connection.ConnectionString;
                    }
                }

                return _connectionString;
			}
		}
	}
}
