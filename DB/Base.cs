using MySql.Data.MySqlClient;


namespace DB
{
	public static class Base
	{
		private static MySqlConnection conn;

		public static bool Connect()
		{
			string serv = "10.1.139.236";
			string db = "based6";
			string login = "d6";
			string pass = "coubeh";

			string connectionString = $"SERVER={serv};DATABASE={db};UID={login};PASSWORD={pass};";


			conn = new MySqlConnection(connectionString);
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

		public static int SaveSession(SavedData savedData)
		{
			if (!Connect()) return -1; // Ensure a connection is established
			try
			{
				// Note the added comma after the Score parameter in the VALUES list
				string query = $"INSERT INTO SessionLevel (PlayerName, GameMode, PlayerHearts, PlayerHP, Score, `Keys`, LevelWidth, LevelHeight, LevelMap, RemainingCoins, StartPos, EndPos, BonusValue) " +
					$"VALUES ('{savedData.PlayerName}', '{savedData.GameMode}', {savedData.PlayerHearts}, {savedData.PlayerHP}, {savedData.Score}, {savedData.Keys}, {savedData.LevelWidth}, {savedData.LevelHeight}, '{savedData.LevelMap}', {savedData.RemainingCoins}, '{savedData.StartPos}', '{savedData.EndPos}', {savedData.BonusValue})";

				var cmd = new MySqlCommand(query, conn);
				cmd.ExecuteNonQuery();
				return (int)cmd.LastInsertedId;
			}
			catch (MySqlException)
			{
				// Log the exception (e.g., Console.WriteLine(ex.Message))
				return -1; // Return an error code
			}
			finally
			{
				Disconnect(); // Always close the connection
			}
		}





	}

	public class SavedData
	{
		public string PlayerName { get; set; } = "John Nick";
		public string GameMode { get; set; } = "Story";
		public int PlayerHearts { get; set; } = 3;
		public int PlayerHP { get; set; } = 2;
		public int Score { get; set; } = 0;
		public int Keys { get; set; } = 0;
		public int LevelWidth { get; set; } = 30;
		public int LevelHeight { get; set; } = 20;
		public string LevelMap { get; set; } = "";
		public int RemainingCoins { get; set; }
		public string StartPos { get; set; } = "0,0";
		public string EndPos { get; set; } = "0,0";
		public int BonusValue { get; set; } = 0;
	}
}
