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
				string query = $"INSERT INTO SessionLevel (PlayerName, GameMode, PlayerHearts, PlayerHP, Score, `Keys`, LevelWidth, LevelHeight, LevelMap, RemainingCoins, StartPos, EndPos, BonusValue, PlayerPos) " +
					$"VALUES ('{savedData.PlayerName}', '{savedData.GameMode}', {savedData.PlayerHearts}, {savedData.PlayerHP}, {savedData.Score}, {savedData.Keys}, {savedData.LevelWidth}, {savedData.LevelHeight}, '{savedData.LevelMap}', {savedData.RemainingCoins}, '{savedData.StartPos}', '{savedData.EndPos}', {savedData.BonusValue}, '{savedData.PlayerPos}');";

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

		public static (string, string, string) LoadStoryLevel(int id)
		{
			if (!Connect()) return ("", "", ""); // Ensure a connection is established
			try
			{
				var query = $"SELECT * FROM StoryLevel WHERE LevelID = {id}";
				var cmd = new MySqlCommand(query, conn);
				var reader = cmd.ExecuteReader();
				reader.Read();
				return (reader.GetString("LevelMap"), reader.GetString("StartPos"), reader.GetString("EndPos"));
			}
			catch (MySqlException)
			{
				// Log the exception (e.g., Console.WriteLine(ex.Message))
				return ("", "", "");
			}
			finally
			{
				Disconnect(); // Always close the connection
			}
		}

		public static SavedData LoadSession(int id)
		{
			if (!Connect()) return new SavedData(); // Ensure a connection is established
			try
			{
				var query = $"SELECT * FROM SessionLevel WHERE SessionID = {id}";
				var cmd = new MySqlCommand(query, conn);
				var reader = cmd.ExecuteReader();
				reader.Read();
				var savedData = new SavedData
				{
					PlayerName = reader.GetString("PlayerName"),
					GameMode = reader.GetString("GameMode"),
					PlayerHearts = reader.GetInt32("PlayerHearts"),
					PlayerHP = reader.GetInt32("PlayerHP"),
					Score = reader.GetInt32("Score"),
					Keys = reader.GetInt32("Keys"),
					LevelWidth = reader.GetInt32("LevelWidth"),
					LevelHeight = reader.GetInt32("LevelHeight"),
					LevelMap = reader.GetString("LevelMap"),
					RemainingCoins = reader.GetInt32("RemainingCoins"),
					StartPos = reader.GetString("StartPos"),
					EndPos = reader.GetString("EndPos"),
					BonusValue = reader.GetInt32("BonusValue"),
					PlayerPos = reader.GetString("PlayerPos")
				};
				return savedData;
			}
			catch (MySqlException)
			{
				return new SavedData(); // Return an empty SavedData object
			}
			finally
			{
				Disconnect(); // Always close the connection
			}
		}





	}

	public class SavedData
	{
		public string PlayerName { get; set; } = string.Empty;
		public string PlayerPos { get; set; } = string.Empty;
		public string LevelMap { get; set; } = string.Empty;
		public string StartPos { get; set; } = string.Empty;
		public string GameMode { get; set; } = string.Empty;
		public string EndPos { get; set; } = string.Empty;
		public int RemainingCoins { get; set; }
		public int PlayerHearts { get; set; }
		public int LevelHeight { get; set; }
		public int BonusValue { get; set; }
		public int LevelWidth { get; set; }
		public int PlayerHP { get; set; }
		public int Score { get; set; }
		public int Keys { get; set; }
	}
}
