namespace Engine
{
	public class FileManager
	{
		private string infiniteModeFilePath;
		private string storyModeFilePath;

		public FileManager()
		{
			string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			appDataFolder = Path.Combine(appDataFolder, "Le Continentale");
			Directory.CreateDirectory(appDataFolder);

			infiniteModeFilePath = Path.Combine(appDataFolder, "InfiniteModeGameData.dat");
			storyModeFilePath = Path.Combine(appDataFolder, "StoryModeGameData.dat");
			InitializeCSV(infiniteModeFilePath);
			InitializeCSV(storyModeFilePath);
		}

		private static void InitializeCSV(string filePath)
		{
			if (!File.Exists(filePath))
				File.WriteAllText(filePath, "Date;GameMode;Score;TimeSpentInMinutes;Level\n");
		}

		public void SaveGameData(string gameMode, int score, int timeSpent, int level = -1)
		{
			string filePath = gameMode == "Story" ? storyModeFilePath : infiniteModeFilePath;
			string dateString = DateTime.Now.ToString("yyyy-MM-dd");
			string levelPart = level == -1 ? "" : $"{level};";
			string newLine = $"{dateString};{gameMode};{score};{timeSpent};{levelPart}\n";

			File.AppendAllText(filePath, newLine);
		}

		public List<PlayerData> LoadGameData()
		{
			List<PlayerData> allGameData = new List<PlayerData>();
			allGameData.AddRange(ReadGameDataFromFile(infiniteModeFilePath));
			allGameData.AddRange(ReadGameDataFromFile(storyModeFilePath));

			return allGameData;
		}

		private static List<PlayerData> ReadGameDataFromFile(string filePath)
		{
			List<PlayerData> gameData = new List<PlayerData>();
			if (File.Exists(filePath))
			{
				using StreamReader reader = new(filePath);
				reader.ReadLine(); // Skip header
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					if (line != null)
					{
						var values = line.Split(';');
						if (values.Length >= 4)
						{
							PlayerData data = new()
							{
								Date = DateTime.Parse(values[0]),
								GameMode = values[1],
								Score = int.Parse(values[2]),
								TimeSpentInMinutes = int.Parse(values[3]),
								Level = values.Length > 4 ? int.Parse(values[4]) : -1 // Default to -1 if no level is present
							};
							gameData.Add(data);
						}
					}
				}
			}
			return gameData;
		}

		public int GetLastPlayedLevel()
		{
			if (!File.Exists(storyModeFilePath))
				return -1;

			int lastLevel = -1;
			using StreamReader reader = new(storyModeFilePath);
			reader.ReadLine();
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				if (line != null)
				{
					var values = line.Split(';');
					if (values.Length > 4 && int.TryParse(values[4], out int level))
						if (level > lastLevel)
							lastLevel = level;
				}
			}
			return lastLevel;
		}


		public Cell[,] LoadLevel(string levelPath)
		{
			string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Levels", levelPath);
			Console.WriteLine("Attempting to load file at: " + filePath);

			if (!File.Exists(filePath))
				throw new FileNotFoundException("The specified level file was not found.", filePath);

			string[] lines = File.ReadAllLines(filePath);
			int rows = lines.Length;
			int cols = (rows > 0) ? lines[0].Length : 0;
			Cell[,] grid = new Cell[rows, cols];

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					string cellValue = lines[i][j].ToString();
					if (Enum.TryParse(cellValue, true, out Cell result))
					{
						grid[i, j] = result;
					}
					else
					{
						grid[i, j] = Cell.Empty;
					}
				}
			}
			return grid;
		}
	}

	public class PlayerData
	{
		public DateTime Date { get; set; }
		public string GameMode { get; set; } = "Story";
		public int Score { get; set; }
		public int TimeSpentInMinutes { get; set; }
		public int Level { get; set; } = -1;
	}
}
