using System.Configuration;
using MySql.Data.MySqlClient;

namespace DataBase
{
	public class Base
	{
		private static MySqlConnection conn;

		public static bool Connect()
		{
			string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
			conn = new MySqlConnection(connStr);
			try
			{
				conn.Open();
				return true;
			}
			catch (MySqlException)
			{
				return false;
			}
		}
		public static bool Disconnect()
		{
			try
			{
				if (conn.State == System.Data.ConnectionState.Open)
				{
					conn.Close();
					conn.Dispose();
				}
				return true;
			}
			catch (MySqlException)
			{
				return false;
			}
		}
	}
}
