using Engine.utils;
using DB;
using System.Diagnostics;

namespace Engine
{
	/// <summary>
	/// Classe principale qui gère la logique du jeu.
	/// </summary>
	public class GameManager
	{
		/// <summary>
		/// Contient le gestionnaire de niveau associé à la partie en cours.
		/// </summary>
		public LevelManager LevelManager { get; private set; }

		/// <summary>
		/// Contient l'état actuel du jeu (en cours, pause, terminé, etc.).
		/// </summary>
		public GameState GameState { get; private set; } = GameState.PLAYING; // Initialement, l'état du jeu est "En cours"

		/// <summary>
		/// Contient ou définit le mode de jeu (histoire ou infini).
		/// </summary>
		public GameMode GameMode { get; set; }

		/// <summary>
		/// Obtient ou définit le joueur.
		/// </summary>
		public Player Player { get; set; }

		/// <summary>
		/// Constructeur pour une nouvelle partie.
		/// </summary>
		/// <param name="gameMode">Le mode de jeu (histoire ou infini).</param>
		/// <param name="playerName">Le nom du joueur.</param>
		/// <param name="PlayerUid">L'identifiant unique du joueur (optionnel, utilisé pour le mode histoire).</param>
		public GameManager(GameMode gameMode, string playerName, string PlayerUid = "")
		{
			Player = new Player(playerName);   // Crée une nouvelle instance du joueur avec le nom donné
			GameMode = gameMode;               // Définit le mode de jeu
			LevelManager = new LevelManager(Player, gameMode, PlayerUid); // Crée le gestionnaire de niveau pour le mode sélectionné
		}

		/// <summary>
		/// Constructeur pour charger une partie sauvegardée.
		/// </summary>
		/// <param name="sessionId">L'ID de la session sauvegardée à charger.</param>
		public GameManager(int sessionId)
		{
			// Charge les données de la session sauvegardée depuis la base de données
			SavedData savedData = Base.LoadSession(sessionId);

			// Crée une instance du joueur avec les données sauvegardées
			Player = new Player(savedData.PlayerName, savedData.BonusValue);

			// Convertit la chaîne de caractères du mode de jeu en une valeur de l'énumération GameMode
			GameMode = (GameMode)Enum.Parse(typeof(GameMode), savedData.GameMode, true);

			// Crée le gestionnaire de niveau avec les données sauvegardées
			LevelManager = new LevelManager(
				savedData.Score,
				savedData.Keys,
				Static.ParseMap(savedData.LevelMap),
				CellCoordinates.Parse(savedData.StartPos),
				CellCoordinates.Parse(savedData.EndPos),
				Player,
				(byte)savedData.PlayerHearts,
				(byte)savedData.PlayerHP,
				GameMode,
				CellCoordinates.Parse(savedData.PlayerPos),
				savedData.RemainingCoins
			);
		}

		/// <summary>
		/// Fait avancer le joueur d'une étape dans la direction donnée, uniquement si le jeu est en cours (GameState.PLAYING).
		/// </summary>
		/// <param name="direction">La direction dans laquelle déplacer le joueur.</param>
		public void StepPlayer(Direction direction)
		{
			if (GameState == GameState.PLAYING) // Vérifie si le jeu est en cours
				Player.Move(direction, LevelManager.LevelMap, this); // Déplace le joueur en utilisant la méthode Move de la classe Player
		}

		/// <summary>
		/// Fait avancer tous les fantômes d'une étape, uniquement si le jeu est en cours (GameState.PLAYING).
		/// </summary>
		public void StepGhosts()
		{
			if (GameState == GameState.PLAYING) // Vérifie si le jeu est en cours
			{
				Enemies.Cain.Move(LevelManager.LevelMap, Player.CurrentDirection);    // Déplace le fantôme Cain
																					  // Enemies.Viggo.Move(LevelManager.LevelMap, Player.CurrentDirection);
				Enemies.Marquis.Move(LevelManager.LevelMap, Player.CurrentDirection);  // Déplace le fantôme Marquis
				Enemies.Winston.Move(LevelManager.LevelMap, Player.CurrentDirection);  // Déplace le fantôme Winston
			}
		}

		/// <summary>
		/// Sauvegarde l'état actuel du jeu dans la base de données.
		/// </summary>
		/// <param name="PlayerUid">L'identifiant unique du joueur.</param>
		/// <returns>L'ID de la session sauvegardée, ou -1 en cas d'erreur.</returns>
		public int SaveSession(string PlayerUid)
		{
			// Crée un objet SavedData et le remplit avec les informations pertinentes du jeu
			SavedData savedData = new()
			{
				PlayerName = Player.Name,                   // Nom du joueur
				GameMode = GameMode.ToString(),             // Mode de jeu (chaîne de caractères)
				PlayerHearts = LevelManager.Health.Lives,     // Nombre de vies du joueur
				PlayerHP = LevelManager.Health.HealthPoints, // Points de vie actuels
				Score = LevelManager.Score,                 // Score du joueur
				Keys = LevelManager.Key,                   // Nombre de clés
				LevelWidth = LevelManager.LevelMap.GetLength(0), // Largeur du labyrinthe
				LevelHeight = LevelManager.LevelMap.GetLength(1), // Hauteur du labyrinthe
				LevelMap = Static.FormatMap(LevelManager.LevelMap), // Carte du niveau (formattée en chaîne de caractères)
				RemainingCoins = LevelManager.RemainingCoins,     // Nombre de pièces restantes
				StartPos = LevelManager.MazeStartPos.ToString(),  // Position de départ (chaîne)
				EndPos = LevelManager.MazeEndPos.ToString(),    // Position d'arrivée (chaîne)
				BonusValue = Player.Bonuses.FrontEndValue,     // Valeur du bonus actif
				PlayerPos = Player.Position.ToString()        // Position actuelle du joueur (chaîne)
			};

			GameState = GameState.PAUSED; // Met le jeu en pause pendant la sauvegarde
			return Base.SaveSession(savedData, PlayerUid); // Sauvegarde les données et retourne l'ID de session
		}

		public void SaveLevelData(string PlayerUID, int timeSpent)
		{
			PlayerData playerData = new()
			{
				Date = DateTime.Now, // Date et heure actuelle
				GameMode = GameMode.ToString(), // Mode de jeu (chaîne de caractères)
				Score = LevelManager.Score, // Score du joueur
				TimeSpentInMinutes = timeSpent, // Temps de jeu (en minutes)
				Level = LevelManager.Level, // Niveau actuel
			};
			Base.SaveGameData(playerData, PlayerUID); // Sauvegarde les données du joueur
		}

		/// <summary>
		/// Gère la collision du joueur avec un ennemi.
		/// </summary>
		public void CollideWithEnemy()
		{
			LevelManager.Health.ReduceHealth();  // Réduit la santé du joueur

			if (LevelManager.Health.IsDead())   // Vérifie si le joueur est mort
			{
				GameOver();                     // Déclenche la fin de partie
				return;                         // Sort de la méthode si le joueur est mort
			}

			Player.SetToStart(); // Replace le joueur à sa position de départ
		}

		/// <summary>
		/// Vérifie si le joueur est entré en collision avec un fantôme.
		/// </summary>
		/// <returns>True si une collision a eu lieu, false sinon.</returns>
		public bool CheckGhostCollisions()
		{
			// Parcours tous les ennemis
			foreach (var enemy in Enemies.enemies.ToEnumerable())
			{
				// Vérifie si la prochaine position du joueur correspond à la position actuelle ou à la prochaine position d'un ennemi
				if (Player.NextPosition == enemy.NextPosition || Player.Position == enemy.NextPosition)
				{
					CollideWithEnemy(); // Gère la collision avec l'ennemi
					return true;        // Indique qu'une collision a eu lieu
				}
			}
			return false; // Aucune collision détectée
		}

		/// <summary>
		/// Charge les statistiques de jeu d'un joueur à partir de la base de données.
		/// </summary>
		/// <param name="PlayerUid">L'identifiant unique du joueur.</param>
		/// <returns>Une liste d'objets PlayerData contenant les statistiques du joueur, ou une liste vide si aucune donnée n'est trouvée.</returns>
		/// <exception cref="InvalidOperationException">Lancée si une erreur survient lors du chargement des données.</exception>
		public static List<PlayerData> LoadGameStat(string PlayerUid)
		{
			try
			{
				return Base.LoadGameData(PlayerUid); // Charge les données de jeu depuis la base de données
			}
			catch (InvalidOperationException) // Capture l'exception si aucune donnée n'est trouvée
			{
				throw new InvalidOperationException(); // Lancer l'exception pour indiquer qu'aucune donnée n'a été trouvée
			}
		}


		/// <summary>
		/// Vérifie si les conditions de victoire du jeu sont remplies.
		/// </summary>
		/// <returns>True si le jeu est terminé (victoire), false sinon.</returns>
		public bool CheckGameCompleted()
		{
			// Le jeu est terminé si :
			// - Le joueur n'est pas mort (Health.IsDead() retourne false)
			// ET (&&)
			//   - (Condition 1) Le joueur a 2 clés ET il n'y a plus de pièces restantes (RemainingCoins <= 0)
			//   OU (||)
			//   - (Condition 2) Le labyrinthe est vide (IsMazeEmpty retourne true)
			return !LevelManager.Health.IsDead() &&
				   (LevelManager is { Key: 2, RemainingCoins: <= 0 } || Static.IsMazeEmpty(LevelManager.LevelMap));
		}

		/// <summary>
		/// Met le jeu en pause en modifiant l'état du jeu (GameState) à PAUSED.
		/// </summary>
		public void Pause()
		{
			GameState = GameState.PAUSED;
		}

		/// <summary>
		/// Reprend le jeu après une pause en modifiant l'état du jeu (GameState) à PLAYING.
		/// </summary>
		public void Resume()
		{
			GameState = GameState.PLAYING;
		}

		/// <summary>
		/// Initialise le niveau suivant et réinitialise les bonus du joueur.
		/// </summary>
		public void NextLevel()
		{
			// Crée un nouveau gestionnaire de niveau pour le prochain niveau, en utilisant le mode de jeu actuel et le joueur existant
			LevelManager = new LevelManager(Player, GameMode);
			Player.Bonuses.Clear(); // Supprime tous les bonus du joueur
			GameState = GameState.PLAYING; // Rétablit l'état du jeu à PLAYING
		}

		/// <summary>
		/// Met fin à la partie en cours en modifiant l'état du jeu (GameState) à GAME_OVER.
		/// </summary>
		public void GameOver()
		{
			GameState = GameState.GAME_OVER;
		}

	}
}
