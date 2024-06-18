/*
- ABASS Hammed
- AURIGNAC Arthur
- DOHER Alexis
- GODET Adrien
- MAS Cédric
- NAHARRO Guerby

GROUPE D-06
SAE 2.01
2023-2024

plus d'informations:
https://learn.microsoft.com/fr-fr/dotnet/api/system.environment?view=net-8.0
https://learn.microsoft.com/fr-fr/dotnet/api/system.io.directory?view=net-8.0
https://learn.microsoft.com/fr-fr/dotnet/api/system.io.path?view=net-8.0
https://learn.microsoft.com/fr-fr/dotnet/api/system.io.streamreader?view=net-8.0

Résumé:
Ce fichier contient le code de la classe FileManager qui est utilisée pour gérer les opérations de lecture
et d'écriture des données de jeu. Il inclut des fonctionnalités pour initialiser les fichiers DAT, sauvegarder les
données de jeu, charger les données de jeu et charger les niveaux de jeu à partir de fichiers.
*/

namespace Engine
{
	public class FileManager
	{
		// Chemins des fichiers pour les modes de jeu infini et histoire
		private readonly string m_InfiniteModeFilePath;
		private readonly string m_StoryModeFilePath;

		/// <summary>
		/// Constructeur de la classe FileManager
		/// Initialise les chemins des fichiers de données de jeu et crée les fichiers s'ils n'existent pas.
		/// </summary>
		public FileManager()
		{
			// Rècupère le chemin du dossier d'application local "C:\Users\$USERNAME\AppData\Local" sous windows
			string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			// Concatener le chemin du dossier d'application local avec le nom de dossier "Le Continentale"
			appDataFolder = Path.Combine(appDataFolder, "Le Continentale");
			// Creer le dossier s'il n'existe pas
			Directory.CreateDirectory(appDataFolder);

			// Chemin des fichiers pour les modes de jeu infini et histoire
			// l'extension ".dat" permet d'indiquer que le fichier est un fichier de données et de faire genre que c'est un fichier sécuriser😆
			m_InfiniteModeFilePath = Path.Combine(appDataFolder, "InfiniteModeGameData.dat");
			m_StoryModeFilePath = Path.Combine(appDataFolder, "StoryModeGameData.dat");
			InitializeDAT(m_InfiniteModeFilePath);
			InitializeDAT(m_StoryModeFilePath);
		}

		/// <summary>
		/// Initialise un fichier DAT s'il n'existe pas.
		/// Ajoute l'en-tête des colonnes au fichier DAT
		/// </summary>
		/// <param name="filePath">Le chemin du fichier DAT à initialiser.</param>
		private static void InitializeDAT(string filePath)
		{
			if (!File.Exists(filePath))
				File.WriteAllText(filePath, "Date;GameMode;Score;TimeSpentInMinutes;Level\n");
		}

		/// <summary>
		/// Sauvegarde les données de jeu dans le fichier approprié.
		/// </summary>
		/// <param name="gameMode">Le mode de jeu (par exemple, "Story" ou "Infinite").</param>
		/// <param name="score">Le score du joueur.</param>
		/// <param name="timeSpent">Le temps passé en minutes.</param>
		/// <param name="level">Le niveau atteint par le joueur (par défaut -1 si non applicable).</param>
		public void SaveGameData(string gameMode, int score, int timeSpent, int level = -1)
		{
			string filePath = gameMode == "Story" ? m_StoryModeFilePath : m_InfiniteModeFilePath;
			string dateString = DateTime.Now.ToString("yyyy-MM-dd");
			// -1 signifie qu'il n'y a pas de niveau, dans le cas du mode infini
			string levelPart = level == -1 ? "" : $"{level};";
			string newLine = $"{dateString};{gameMode};{score};{timeSpent};{levelPart}\n";

			// Ajoute la nouvelle ligne au fichier sana supprime les autres lignes
			File.AppendAllText(filePath, newLine);
		}

		/// <summary>
		/// Charge les données de jeu depuis les fichiers de mode infini et mode histoire.
		/// </summary>
		/// <returns>Une liste de PlayerData contenant toutes les données de jeu chargées.</returns>
		public List<PlayerData> LoadGameData()
		{
			// Concatenation des données de jeu de mode infini et mode histoire pour en faire une seule
			List<PlayerData> allGameData =
			[
				// récupère les données de jeu
				.. ReadGameDataFromFile(m_InfiniteModeFilePath),
				.. ReadGameDataFromFile(m_StoryModeFilePath),
			];

			return allGameData;
		}

		/// <summary>
		/// Lit les données de jeu depuis un fichier spécifique.
		/// </summary>
		/// <param name="filePath">Le chemin du fichier de données de jeu.</param>
		/// <returns>Une liste de PlayerData contenant les données de jeu lues depuis le fichier.</returns>
		private static List<PlayerData> ReadGameDataFromFile(string filePath)
		{
			// Initialise une nouvelle liste de PlayerData pour stocker les données de jeu
			List<PlayerData> gameData = [];
			if (File.Exists(filePath)) // Vérifie si le fichier existe
			{
				using StreamReader reader = new(filePath);
				reader.ReadLine(); // passe la première ligne qui contient les en-têtes des colonnes
				while (!reader.EndOfStream) // tant qu'il y a de lignes à lire
				{
					var line = reader.ReadLine(); // lit la ligne suivante
					if (line != null) // vérifie si la ligne n'est pas nulle
					{
						var values = line.Split(';'); // sépare les valeurs de la ligne
						if (values.Length >= 4) // vérifie si la ligne contient au moins 4 valeurs
						{
							PlayerData data = new() // crée une nouvelle instance de PlayerData
							{
								Date = DateTime.Parse(values[0]), // convertit la date en DateTime
								GameMode = values[1], // affecte le mode de jeu
								Score = int.Parse(values[2]), // convertit le score
								TimeSpentInMinutes = int.Parse(values[3]), // convertit le temps en entuer
								Level = values.Length > 4 ? int.Parse(values[4]) : -1
							};
							gameData.Add(data); // ajoute la nouvelle instance de PlayerData à la liste
						}
					}
				}
			}
			return gameData;
		}

		/// <summary>
		/// Obtient le dernier niveau joué en mode histoire.
		/// </summary>
		/// <returns>Le dernier niveau joué, ou -1 s'il n'y a pas de données disponibles.</returns>
		public int GetLastPlayedLevel()
		{
			if (!File.Exists(m_StoryModeFilePath))
				return -1;

			int lastLevel = -1;
			using StreamReader reader = new(m_StoryModeFilePath);
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
	}

	/// <summary>
	/// Classe représentant les données d'un joueur
	/// </summary>
	public class PlayerData
	{
		public DateTime Date { get; set; }
		public string GameMode { get; set; } = "Story";
		public int Score { get; set; }
		public int TimeSpentInMinutes { get; set; }
		public int Level { get; set; } = -1;
	}
}