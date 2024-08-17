using MySql.Data.MySqlClient;


namespace DB
{
	public static class Base // Classe statique pour gérer les interactions avec la base de données
	{
		// Connexion à la base de données
		private static MySqlConnection? conn;


		/// <summary>
		/// Méthode pour établir une connexion à la base de données
		/// </summary>
		/// <returns></returns>
		public static bool Connect()
		{
			// Informations de connexion à la base de données
			string serv = "";  // Adresse du serveur
			string db = "";        // Nom de la base de données
			string login = "";         // Nom d'utilisateur
			string pass = "";      // Mot de passe

			// Chaîne de connexion
			string connectionString = $"SERVER={serv};DATABASE={db};UID={login};PASSWORD={pass};";

			conn = new MySqlConnection(connectionString); // Création de l'objet de connexion
			try
			{
				conn.Open(); // Ouverture de la connexion
				return true;  // Connexion réussie
			}
			catch (MySqlException)
			{
				return false; // Échec de la connexion
			}
		}


		/// <summary>
		/// Méthode pour fermer la connexion à la base de données
		/// </summary>
		/// <returns></returns>
		public static bool Disconnect()
		{
			try
			{
				if (conn?.State == System.Data.ConnectionState.Open) // Si la connexion est ouverte
				{
					conn.Close();     // Fermeture de la connexion
					conn.Dispose();  // Libération des ressources
				}
				return true; // Fermeture réussie
			}
			catch (MySqlException)
			{
				return false; // Échec de la fermeture
			}
		}


		/// <summary>
		/// Méthode pour sauvegarder une session de jeu dans la base de données
		/// </summary>
		/// <param name="savedData"></param>
		/// <param name="PlayerUID"></param>
		/// <returns></returns>
		public static int SaveSession(SavedData savedData, string PlayerUID)
		{
			if (!Connect()) return -1; // Vérifie si la connexion est établie

			try
			{
				// Requête SQL pour insérer les données de la session dans la table SessionLevel
				string query = $"INSERT INTO SessionLevel (PlayerName, PlayerUID, GameMode, PlayerHearts, PlayerHP, Score, `Keys`, LevelWidth, LevelHeight, LevelMap, RemainingCoins, StartPos, EndPos, BonusValue, PlayerPos) " +
							   $"VALUES ('{savedData.PlayerName}', '{PlayerUID}','{savedData.GameMode}', {savedData.PlayerHearts}, {savedData.PlayerHP}, {savedData.Score}, {savedData.Keys}, {savedData.LevelWidth}, {savedData.LevelHeight}, '{savedData.LevelMap}', {savedData.RemainingCoins}, '{savedData.StartPos}', '{savedData.EndPos}', {savedData.BonusValue}, '{savedData.PlayerPos}');";

				var cmd = new MySqlCommand(query, conn);  // Création de la commande SQL
				cmd.ExecuteNonQuery();                   // Exécution de la requête
				return (int)cmd.LastInsertedId;          // Retourne l'ID de la session insérée
			}
			catch (MySqlException)
			{
				return -1; // Retourne -1 en cas d'erreur
			}
			finally
			{
				Disconnect(); // Ferme la connexion dans tous les cas (succès ou échec)
			}
		}

		/// <summary>
		/// Charge les données d'un niveau du mode histoire depuis la base de données.
		/// </summary>
		/// <param name="id">L'ID du niveau à charger.</param>
		/// <returns>Un tuple contenant la carte du niveau (`LevelMap`), la position de départ (`StartPos`) et la position d'arrivée (`EndPos`).</returns>
		public static (string, string, string) LoadStoryLevel(int id)
		{
			if (!Connect()) return ("", "", ""); // Vérifier la connexion à la base de données

			try
			{
				var query = $"SELECT * FROM StoryLevel WHERE LevelID = {id}"; // Requête SQL pour sélectionner le niveau
				var cmd = new MySqlCommand(query, conn); // Création de la commande SQL
				var reader = cmd.ExecuteReader(); // Exécution de la requête et obtention du lecteur de résultats

				reader.Read(); // Lecture de la première (et unique) ligne de résultat

				// Retourne un tuple contenant les informations du niveau
				return (reader.GetString("LevelMap"), reader.GetString("StartPos"), reader.GetString("EndPos"));
			}
			catch (MySqlException) // Capture les erreurs MySQL
			{
				return ("", "", ""); // Retourne des chaînes vides en cas d'erreur
			}
			finally
			{
				Disconnect(); // Ferme la connexion à la base de données dans tous les cas (succès ou erreur)
			}
		}

		/// <summary>
		/// Obtient le dernier niveau du mode histoire joué par un joueur.
		/// </summary>
		/// <param name="playerUID">L'UID du joueur.</param>
		/// <returns>Le numéro du dernier niveau joué, ou -1 si aucune donnée n'a été trouvée.</returns>
		public static int GetLastStoryLevelPlayed(string playerUID)
		{
			int lastLevel = -1; // Valeur par défaut (-1 indique qu'aucun niveau n'a été joué)

			if (!Connect()) return lastLevel; // Vérifier la connexion à la base de données

			try
			{
				string query = "SELECT Level FROM LevelStat WHERE PlayerUID = @PlayerUID AND GameMode = 'Story' ORDER BY StatID DESC LIMIT 1"; // Requête SQL pour obtenir le dernier niveau joué
				using var cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@PlayerUID", playerUID); // Ajout du paramètre pour l'UID du joueur
				using var reader = cmd.ExecuteReader(); // Exécution de la requête

				if (reader.Read()) // Si des résultats ont été trouvés
				{
					lastLevel = reader.GetInt32("Level"); // Récupère le numéro du niveau
				}
			}
			catch (MySqlException) // Capture les erreurs MySQL
			{
				return -1; // Retourne -1 en cas d'erreur
			}
			finally
			{
				Disconnect(); // Ferme la connexion à la base de données
			}

			return lastLevel; // Retourne le dernier niveau joué (ou -1 si aucun)
		}

		/// <summary>
		/// Sauvegarde les données d'une partie terminée dans la table LevelStat.
		/// </summary>
		/// <param name="playerData">Les données de la partie à sauvegarder (PlayerData).</param>
		/// <param name="playerUID">L'UID du joueur.</param>
		public static void SaveGameData(PlayerData playerData, string playerUID)
		{
			if (!Connect()) // Vérifier la connexion à la base de données
				throw new InvalidOperationException("Erreur de connexion à la base de données.");

			try
			{
				// Requête SQL paramétrée pour insérer les données dans la table LevelStat
				string query = @"
            INSERT INTO LevelStat (PlayerUID, GameMode, `Date`, Score, TimeSpentInMinutes, Level)
            VALUES (@PlayerUID, @GameMode, @Date, @Score, @TimeSpentInMinutes, @Level)";

				using var cmd = new MySqlCommand(query, conn);

				// Ajout des paramètres à la requête SQL
				cmd.Parameters.AddWithValue("@PlayerUID", playerUID);
				cmd.Parameters.AddWithValue("@GameMode", playerData.GameMode);
				cmd.Parameters.AddWithValue("@Date", playerData.Date.ToString("yyyy-MM-dd HH:mm:ss")); // Formater la date
				cmd.Parameters.AddWithValue("@Score", playerData.Score);
				cmd.Parameters.AddWithValue("@TimeSpentInMinutes", playerData.TimeSpentInMinutes);
				cmd.Parameters.AddWithValue("@Level", playerData.Level);

				cmd.ExecuteNonQuery(); // Exécution de la requête
			}
			catch (MySqlException ex)
			{
				// Gérer les exceptions de la base de données (enregistrer dans un fichier journal, afficher un message d'erreur, etc.)
				throw new InvalidOperationException("Erreur lors de la sauvegarde des données de jeu.", ex);
			}
			finally
			{
				Disconnect(); // Fermer la connexion dans tous les cas
			}
		}


		/// <summary>
		/// Charge les données de jeu d'un joueur depuis la base de données.
		/// </summary>
		/// <param name="playerUID">L'UID du joueur.</param>
		/// <returns>Une liste d'objets PlayerData contenant les données de jeu du joueur.</returns>
		public static List<PlayerData> LoadGameData(string playerUID)
		{
			List<PlayerData> allGameData = []; // Crée une liste pour stocker les données de jeu

			if (!Connect()) throw new InvalidOperationException("Erreur de connexion."); // Vérifie la connexion et lève une exception en cas d'échec

			try
			{
				string query = $"SELECT `Date`, `GameMode`, `Score`, `TimeSpentInMinutes`, `Level` FROM `LevelStat` WHERE `PlayerUID` = '{playerUID}'";
				MySqlCommand cmd = new(query, conn); // Création de la commande SQL
				using MySqlDataReader reader = cmd.ExecuteReader(); // Exécution de la requête

				while (reader.Read()) // Parcours les lignes de résultat
				{
					// Crée un objet PlayerData pour chaque ligne et l'ajoute à la liste
					PlayerData data = new()
					{
						Date = DateTime.Parse(reader.GetString("Date")),
						GameMode = reader.GetString("GameMode"),
						Score = reader.GetInt32("Score"),
						TimeSpentInMinutes = reader.GetInt32("TimeSpentInMinutes"),
						Level = reader.GetInt32("Level")
					};
					allGameData.Add(data);
				}

				if (allGameData.Count == 0) // Si aucune donnée n'a été trouvée, lève une exception
				{
					throw new InvalidOperationException();
				}
			}
			catch (Exception ex) // Capture les erreurs et lève une nouvelle exception avec un message plus explicite
			{
				throw new InvalidOperationException("Erreur lors du chargement des données.", ex);
			}

			return allGameData; // Retourne la liste des données de jeu du joueur
		}


		/// <summary>
		/// Charge les données d'une session de jeu à partir de la base de données.
		/// </summary>
		/// <param name="id">L'identifiant de la session à charger.</param>
		/// <returns>Un objet SavedData contenant les données de la session, ou un objet vide en cas d'erreur.</returns>
		/// <exception cref="InvalidOperationException">En cas d'échec de la connexion à la base de données.</exception>
		public static SavedData LoadSession(int id)
		{
			// Vérifie la connexion à la base de données
			if (!Connect()) return new();
			try
			{
				var query = $"SELECT * FROM SessionLevel WHERE SessionID = {id}"; // Requête pour sélectionner la session
				var cmd = new MySqlCommand(query, conn); // Création de la commande SQL
				var reader = cmd.ExecuteReader(); // Exécution de la requête
				reader.Read(); // Lecture de la première (et unique) ligne de résultat

				// Crée et remplit un objet SavedData avec les données de la session
				SavedData savedData = new()
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
				cmd.Dispose(); // Libère les ressources de la commande

				var deleteQuery = $"DELETE FROM SessionLevel WHERE SessionID = {id}"; // Requête pour supprimer la session
				var deleteCmd = new MySqlCommand(deleteQuery, conn); // Création de la commande SQL
				deleteCmd.ExecuteNonQuery(); // Exécution de la requête de suppression

				// Retourne les données de la session
				return savedData;
			}
			catch (MySqlException)
			{
				throw new(); // Lève une exception en cas d'erreur
			}
			finally
			{
				Disconnect(); // Ferme la connexion
			}
		}
	}

	// Classe représentant les données d'une session de jeu sauvegardée
	public class SavedData
	{
		// Nom du joueur
		public string PlayerName { get; set; } = string.Empty;

		// Position actuelle du joueur (chaîne formatée)
		public string PlayerPos { get; set; } = string.Empty;

		// Représentation textuelle de la carte du niveau (format personnalisé)
		public string LevelMap { get; set; } = string.Empty;

		// Position de départ du joueur (chaîne formatée)
		public string StartPos { get; set; } = string.Empty;

		// Mode de jeu (Story, Infinite, etc.)
		public string GameMode { get; set; } = string.Empty;

		// Position d'arrivée du niveau (chaîne formatée)
		public string EndPos { get; set; } = string.Empty;

		// Nombre de pièces restantes à collecter
		public int RemainingCoins { get; set; }

		// Nombre de vies (cœurs) restantes du joueur
		public int PlayerHearts { get; set; }

		// Hauteur du niveau en nombre de cellules
		public int LevelHeight { get; set; }

		// Valeur correspondante aux bonus récupérés 
		public int BonusValue { get; set; }

		// Largeur du niveau en nombre de cellules
		public int LevelWidth { get; set; }

		// Points de vie actuels du joueur (santé)
		public int PlayerHP { get; set; }

		// Score actuel du joueur
		public int Score { get; set; }

		// Nombre de clés possédées par le joueur
		public int Keys { get; set; }
	}

	// Classe représentant les données de jeu d'un joueur ( pour les statistiques)
	public class PlayerData
	{
		// Date et heure de la partie
		public DateTime Date { get; set; }

		// Mode de jeu (Story, Infinite)
		public string GameMode { get; set; } = "Story"; // Par défaut, le mode Story

		// Score obtenu dans la partie
		public int Score { get; set; }

		// Temps passé dans la partie en minutes
		public int TimeSpentInMinutes { get; set; }

		// Niveau actuel (-1 si non applicable, par exemple en mode Infinite)
		public int Level { get; set; } = -1;
	}

}
