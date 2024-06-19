using Engine.utils;

namespace Engine
{
	/// <summary>
	/// Classe représentant le joueur dans notre jeu.
	/// Cette classe est sealed, ce qui signifie qu'elle ne peut pas être héritée par d'autres classes.
	/// </summary>
	public sealed class Player : Entity // Le joueur est une entité du jeu
	{
		/// <summary>
		/// Représente les bonus possédés par le joueur (sous forme de paire).
		/// </summary>
		public BonusPair<Bonus> Bonuses;

		/// <summary>
		/// Constructeur pour un nouveau joueur avec un nom par défaut ("John").
		/// </summary>
		/// <param name="name">Le nom du joueur (optionnel).</param>
		public Player(string name = "John")
		{
			Bonuses = new BonusPair<Bonus>(null, null); // Initialise les bonus à vide (aucun bonus actif)
			Name = name;          // Définit le nom du joueur
			Kind = Cell.JOHN;      // Définit le type de cellule du joueur comme étant JOHN (Pac-Man)
		}

		/// <summary>
		/// Constructeur pour un joueur chargé depuis la base de données (avec une valeur de bonus).
		/// </summary>
		/// <param name="name">Le nom du joueur.</param>
		/// <param name="BonusValue">La valeur du bonus à charger.</param>
		public Player(string name, int BonusValue)
		{
			Name = name;
			Kind = Cell.JOHN;
			Bonuses = new BonusPair<Bonus>(BonusValue); // Initialise les bonus avec la valeur chargée depuis la base de données
		}

		/// <summary>
		/// Définit le nom du joueur.
		/// </summary>
		/// <param name="newName">Le nouveau nom du joueur.</param>
		public void SetPlayerName(string newName)
		{
			Name = newName;
		}

		/// <summary>
		/// Définit la position du joueur.
		/// </summary>
		/// <param name="newPostion">La nouvelle position (coordonnées).</param>
		public void SetPlayerPosition(CellCoordinates newPosition)
		{
			Position = newPosition;
		}

		public void SetToStart()
		{
			Position = StartPosition;
			NextPosition = Position;
		}

		/// <summary>
		/// Place le joueur dans le labyrinthe à une position donnée.
		/// </summary>
		/// <param name="maze">Le labyrinthe.</param>
		/// <param name="startPosition">La position de départ du joueur.</param>
		/// <param name="PlayerPosition">La position où placer le joueur.</param>
		public void PlacePlayer(Cell[,] maze, CellCoordinates startPosition, CellCoordinates PlayerPosition)
		{
			// Met à jour le labyrinthe avec le type de cellule du joueur
			maze[PlayerPosition.Row, PlayerPosition.Col] = Kind;

			// Met à jour la position du joueur
			Position = PlayerPosition;

			// Met à jour la position de depart du joueur
			StartPosition = startPosition;

			// Arrête le joueur initialement
			CurrentDirection = Direction.STOP;
		}

		/// <summary>
		/// Déplace le joueur dans une direction donnée, en vérifiant les collisions et les limites du labyrinthe.
		/// </summary>
		/// <param name="direction">La direction du mouvement.</param>
		/// <param name="maze">Le labyrinthe.</param>
		/// <param name="gameManager">Le gestionnaire du jeu.</param>
		public void Move(Direction direction, Cell[,] maze, GameManager gameManager)
		{
			NextPosition = GetNextPosition(Position, direction);

			// Check if the new position is within the bounds of the maze
			if (IsInBounds(NextPosition, maze))
			{
				if (maze[NextPosition.Row, NextPosition.Col] != Cell.WALL)
				{
					// Met à jour la direction actuelle du joueur
					CurrentDirection = direction;
					CheckCollisions(maze[NextPosition.Row, NextPosition.Col], gameManager);
					UpdatePosition(NextPosition, maze);
				}
				else
					CurrentDirection = Direction.STOP; // Arrête le joueur s'il rencontre un mur
			}
		}

		/// <summary>
		/// Gère les collisions du joueur avec différents types de cellules (pièces, kits de santé, clés, torches).
		/// </summary>
		/// <param name="cellType">Le type de cellule avec lequel le joueur est entré en collision.</param>
		/// <param name="gameManager">Le gestionnaire du jeu.</param>
		public void CheckCollisions(Cell cellType, GameManager gameManager)
		{
			// Switch pour gérer les différents types de collisions
			switch (cellType)
			{
				case Cell.COIN:       // Si c'est une pièce
					gameManager.LevelManager.UpdateScore(10); // Augmente le score de 10 points
					break;
				case Cell.HEALTH_KIT: // Si c'est un kit de santé
					Bonuses.Add(new HealthBonus()); // Ajoute un bonus de santé au joueur
					break;
				case Cell.KEY:        // Si c'est une clé
					gameManager.LevelManager.AddKey(); // Ajoute une clé à l'inventaire du joueur
					break;
				case Cell.TORCH:     // Si c'est une torche
					Bonuses.Add(new TorchBonus());  // Ajoute un bonus de torche au joueur
					break;
				default:
					break;
			}
		}
	}
}
