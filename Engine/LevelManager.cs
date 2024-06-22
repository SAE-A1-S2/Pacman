using System.ComponentModel;
using Engine.utils;

namespace Engine;

/// <summary>
/// Classe responsable de la gestion du niveau en cours dans le jeu.
/// </summary>
public class LevelManager : INotifyPropertyChanged // Implémentation de l'interface pour notifier les changements de propriété
{
	// Dimensions du labyrinthe (largeur et hauteur)
	private static readonly int s_Width = 30;
	private static readonly int s_Height = 20;

	// Générateur de labyrinthe
	private MazeGenerator m_MazeGenerator;

	// Propriétés liées à l'état du niveau
	public int RemainingCoins { get; private set; } // Nombre de pièces restantes à collecter
	public int Score { get; private set; }          // Score actuel du joueur
	public Health Health { get; private set; }       // Santé du joueur
	public Player Player;                          // Référence au joueur
	public byte Key { get; private set; }          // Nombre de clés possédées par le joueur

	// Représentation du labyrinthe et positions de depart et d'arrivée
	public Cell[,] LevelMap { get; private set; } = new Cell[s_Width, s_Height]; // Tableau 2D représentant le labyrinthe
	public CellCoordinates MazeStartPos { get; private set; } // Position de départ dans le labyrinthe
	public CellCoordinates MazeEndPos { get; private set; }   // Position de fin dans le labyrinthe

	public int Level { get; private set; } = -1; // Niveau actuel (-1 si non applicable, par exemple en mode Infinite)

	// Dictionnaire pour stocker les positions des objets statiques
	private readonly Dictionary<string, CellCoordinates> ObjectPositions = [];

	public Dictionary<CellCoordinates, Stack<Cell>> cellHistory = [];

	// Événement déclenché lorsqu'une propriété change 
	public event PropertyChangedEventHandler? PropertyChanged;

	/// <summary>
	/// Constructeur pour charger un niveau à partir d'une sauvegarde.
	/// </summary>
	/// <param name="score">Le score initial du joueur.</param>
	/// <param name="key">Le nombre initial de clés du joueur.</param>
	/// <param name="levelMap">Le labyrinthe du niveau.</param>
	/// <param name="mazeStartPos">La position de départ du labyrinthe.</param>
	/// <param name="mazeEndPos">La position de fin du labyrinthe.</param>
	/// <param name="player">Le joueur.</param>
	/// <param name="lives">Le nombre de vies du joueur.</param>
	/// <param name="healthPoints">Les points de vie du joueur.</param>
	/// <param name="gameMode">Le mode de jeu (histoire ou infini).</param>
	/// <param name="playerPosition">La position initiale du joueur.</param>
	/// <param name="remainingCoins">Le nombre de pièces restantes.</param>
	public LevelManager(int score, int key, Cell[,] levelMap, CellCoordinates mazeStartPos, CellCoordinates mazeEndPos, Player player, byte lives, byte healthPoints, GameMode gameMode, CellCoordinates playerPosition, int remainingCoins)
	{
		// Initialisation des propriétés du LevelManager
		Health = new Health(lives, healthPoints);
		Score = score;
		Key = (byte)key;
		RemainingCoins = remainingCoins;
		Player = player;
		m_MazeGenerator = new(s_Width, s_Height, levelMap, mazeStartPos, mazeEndPos);

		// Initialisation du niveau en fonction du mode de jeu
		InitializeLevel(gameMode);

		// Placement du joueur dans le labyrinthe
		Player.PlacePlayer(LevelMap, mazeStartPos, playerPosition);
		Static.RestoreEnemy(LevelMap);
	}

	/// <summary>
	/// Constructeur pour un nouveau niveau en fonction du mode de jeu et de l'UID du joueur.
	/// </summary>
	/// <param name="player">Le joueur.</param>
	/// <param name="gameMode">Le mode de jeu (histoire ou infini).</param>
	/// <param name="PlayerUid">L'UID du joueur (optionnel, utilisé pour le mode histoire).</param>
	public LevelManager(Player player, GameMode gameMode, string PlayerUid = "")
	{
		// Initialisation des propriétés du LevelManager
		Health = new Health();
		Score = 0;
		Key = 0;
		m_MazeGenerator = new MazeGenerator(s_Width, s_Height);

		// Initialisation du niveau en fonction du mode de jeu
		InitializeLevel(gameMode, PlayerUid);

		// Le joueur est une référence externe (provient de GameManager)
		Player = player;

		// Placement du joueur à la position de départ du labyrinthe
		Player.PlacePlayer(LevelMap, MazeStartPos, MazeStartPos);

		// Placement des objets statiques, des ennemis et des pièces dans le labyrinthe
		PlaceStaticObjects();
		PlaceEnemies();
		PlaceCoins();
	}

	/// <summary>
	/// Place les ennemis dans le labyrinthe à des positions vides aléatoires, en respectant une distance minimale par rapport à la position de départ du joueur.
	/// </summary>
	private void PlaceEnemies()
	{
		// Recherche 4 positions vides dans le labyrinthe, suffisamment éloignées de la position de départ du joueur
		var pos = Enemies.FindEmptyPositions(LevelMap, 4, MazeStartPos);

		// Place chaque ennemi à l'une des positions trouvées
		Enemies.Cain.SetStartingPosition(pos[0], LevelMap);    // Place l'ennemi Cain
		Enemies.Viggo.SetStartingPosition(pos[1], LevelMap);
		Enemies.Marquis.SetStartingPosition(pos[2], LevelMap);  // Place l'ennemi Marquis
		Enemies.Winston.SetStartingPosition(pos[3], LevelMap);  // Place l'ennemi Winston
	}

	/// <summary>
	/// Initialise le niveau en cours en fonction du mode de jeu sélectionné.
	/// Charge le labyrinthe et les positions de départ et d'arrivée, soit depuis un fichier prédéfini (mode histoire), soit en générant un nouveau labyrinthe (mode infini).
	/// </summary>
	/// <param name="gameMode">Le mode de jeu (GameMode.STORY ou GameMode.INFINITE).</param>
	/// <param name="PlayerUid">L'identifiant unique du joueur (utilisé pour le mode histoire).</param>
	private void InitializeLevel(GameMode gameMode = GameMode.INFINITE, string PlayerUid = "")
	{
		if (gameMode == GameMode.STORY) // Si le mode de jeu est "Histoire"
		{
			StoryMode storyMode = new(PlayerUid); // Crée une instance de StoryMode pour gérer le niveau

			// Initialise les propriétés du niveau à partir des données du mode histoire
			LevelMap = storyMode.Maze;
			MazeStartPos = storyMode.StartPos;
			MazeEndPos = storyMode.EndPos;
			Level = storyMode.Level;
		}
		else // Si le mode de jeu est "Infini"
		{
			// Génère un nouveau labyrinthe et initialise les propriétés du niveau en conséquence
			LevelMap = m_MazeGenerator.Map;
			MazeStartPos = m_MazeGenerator.Start;
			MazeEndPos = m_MazeGenerator.End;
		}
	}


	/// <summary>
	/// Met à jour le score du joueur et décrémente le nombre de pièces restantes si nécessaire.
	/// </summary>
	/// <param name="score">Le nombre de points à ajouter au score actuel.</param>
	public void UpdateScore(int score)
	{
		Score += score; // Ajoute le score donné au score total

		if (score == 10) // Si le score ajouté est de 10, c'est qu'une pièce a été mangée
			RemainingCoins--; // Décrémente le nombre de pièces restantes

		// Déclenche l'événement PropertyChanged pour notifier les observateurs (l'interface utilisateur) que le score a changé
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Score)));
	}

	/// <summary>
	/// Place des pièces dans toutes les cellules vides du labyrinthe.
	/// </summary>
	private void PlaceCoins()
	{
		// Parcours toutes les cellules du labyrinthe
		for (var x = 0; x < LevelMap.GetLength(0); x++) // Parcours les lignes
		{
			for (var y = 0; y < LevelMap.GetLength(1); y++) // Parcours les colonnes
			{
				if (LevelMap[x, y] != Cell.EMPTY || LevelMap[x, y] == Cell.END) continue; // Si la cellule n'est pas vide, passe à la suivante

				LevelMap[x, y] = Cell.COIN; // Place une pièce dans la cellule vide
				RemainingCoins++; // Incrémente le compteur de pièces restantes
			}
		}
	}

	/// <summary>
	/// Ajoute une clé à l'inventaire du joueur, jusqu'à un maximum de 2 clés.
	/// </summary>
	public void AddKey()
	{
		if (Key > 2) return; // Ne fait rien si le joueur a déjà le maximum de clés (2)

		Key++; // Incrémente le nombre de clés

		// Déclenche l'événement PropertyChanged pour notifier les observateurs (l'interface utilisateur) que le nombre de clés a changé
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Key)));
	}

	/// <summary>
	/// Réinitialise le niveau actuel du jeu en fonction du mode de jeu spécifié.
	/// </summary>
	/// <param name="gameMode">Le mode de jeu (histoire ou infini).</param>
	/// <param name="PlayerUid">L'identifiant unique du joueur (utilisé pour le mode histoire).</param>
	public void ResetLevel(GameMode gameMode, string PlayerUid)
	{
		// Réinitialise la santé du joueur
		Health.ResetHealth();

		// Réinitialise le score et le nombre de clés
		Score = 0;
		Key = 0;

		// Efface tous les bonus du jeu
		Player.Bonuses.Clear();

		// Réinitialise le labyrinthe et les positions
		InitializeLevel(gameMode, PlayerUid);

		// Supprime tous les objets du jeu
		RemoveObjects();

		// Replace le joueur à la position de départ
		Player.PlacePlayer(LevelMap, MazeStartPos, MazeStartPos);

		// Vide la liste des positions des objets
		ObjectPositions.Clear();

		// Replace les objets statiques (clés, trousse de soins, torche)
		PlaceStaticObjects();

		// Replace les pièces dans le labyrinthe
		PlaceCoins();

		cellHistory.Clear();

		// Notifie l'interface utilisateur des changements de propriétés (score et clés)
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Key)));
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Score)));
	}

	/// <summary>
	/// Place les objets statiques (clés, kit de soin, torche) dans les coins du labyrinthe.
	/// </summary>
	/// <param name="objects">La liste des objets statiques à placer.</param>
	private void PlaceStaticObjects()
	{
		// Liste des objets statiques à placer
		List<Cell> objects = [Cell.HEALTH_KIT, Cell.KEY, Cell.TORCH, Cell.KEY];

		// Coordonnées des quatre coins du labyrinthe
		CellCoordinates[] corners = [
		new(0, 0),
		new(s_Width - 1, 0),
		new(0, s_Height - 1),
		new(s_Width - 1, s_Height - 1)
	];

		// Initialisation du compteur pour les clés en double
		int keyCounter = 0;

		// Parcourt la liste des objets à placer
		foreach (var obj in objects)
		{
			var corner = corners[objects.IndexOf(obj)]; // Sélectionne le coin correspondant à l'objet

			// Trouve une cellule vide près du coin pour placer l'objet (en utilisant l'algorithme FindCell)
			var placement = Algorithms.FindCell(LevelMap, corner);

			// Place l'objet dans le labyrinthe
			LevelMap[placement.Row, placement.Col] = obj;

			// Enregistre la position de l'objet dans le dictionnaire ObjectPositions
			if (obj == Cell.KEY)
			{
				ObjectPositions.Add($"{obj}_{keyCounter}", placement);
				keyCounter++;
			}
			else
				ObjectPositions.Add(obj.ToString(), placement);
		}
	}

	/// <summary>
	/// Supprime tous les objets statiques (clés, kit de soin, torche) du labyrinthe.
	/// </summary>
	private void RemoveObjects()
	{
		// Parcourt le dictionnaire ObjectPositions pour supprimer chaque objet
		foreach (var obj in ObjectPositions.Keys)
			LevelMap[ObjectPositions[obj].Row, ObjectPositions[obj].Col] = Cell.EMPTY;
	}
}
