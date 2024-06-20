using Engine.utils;

namespace Engine
{
	/// <summary>
	/// Classe statique contenant des informations et des méthodes relatives aux ennemis du jeu.
	/// </summary>
	public static class Enemies
	{
		// Instances des ennemis avec leurs noms, comportements et types de cellules associés
		public static Enemy Winston = new("Winston", new AmbusherBehavior(), Cell.WINSTON);
		public static Enemy Cain = new("Cain", new ChaserBehavior(), Cell.CAIN);
		public static Enemy Viggo = new("Viggo", new WandererBehavior(), Cell.VIGGO);
		public static Enemy Marquis = new("Marquis", new WhimsicalBehavior(), Cell.MARQUIS);

		// Tuple contenant les quatre ennemis
		public static (Enemy, Enemy, Enemy, Enemy) enemies = (Winston, Cain, Viggo, Marquis);

		/// <summary>
		/// Extension pour convertir un tuple d'ennemis en une collection énumérable (IEnumerable<Enemy>).
		/// </summary>
		/// <param name="tuple">Le tuple d'ennemis.</param>
		/// <returns>Une collection énumérable d'ennemis.</returns>
		public static IEnumerable<Enemy> ToEnumerable(this (Enemy, Enemy, Enemy, Enemy) tuple)
		{
			yield return tuple.Item1; // Winston
			yield return tuple.Item2; // Cain
			yield return tuple.Item3; // Viggo
			yield return tuple.Item4; // Marquis
		}

		/// <summary>
		/// Extension pour appliquer une action à chaque ennemi dans un tuple d'ennemis.
		/// </summary>
		/// <param name="tuple">Le tuple d'ennemis.</param>
		/// <param name="action">L'action à appliquer à chaque ennemi.</param>
		public static void ForEach(this (Enemy, Enemy, Enemy, Enemy) tuple, Action<Enemy> action)
		{
			action(tuple.Item1);
			action(tuple.Item2);
			action(tuple.Item3);
			action(tuple.Item4);
		}

		/// <summary>
		/// Trouve des positions vides aléatoires dans le labyrinthe, en évitant les positions proches du joueur.
		/// </summary>
		/// <param name="maze">Le labyrinthe représenté par un tableau 2D de cellules.</param>
		/// <param name="count">Le nombre de positions vides à trouver.</param>
		/// <param name="playerPosition">La position actuelle du joueur.</param>
		/// <param name="minimumDistance">La distance minimale requise entre une position vide et le joueur.</param>
		/// <returns>Une liste de coordonnées de cellules vides respectant les contraintes.</returns>
		public static List<CellCoordinates> FindEmptyPositions(Cell[,] maze, int count, CellCoordinates playerPosition, int minimumDistance = 3)
		{
			var emptyPositions = new List<CellCoordinates>(); // Liste des positions vides trouvées
			var random = new Random(); // Générateur de nombres aléatoires

			while (emptyPositions.Count < count) // Boucle jusqu'à ce que le nombre requis de positions soit trouvé
			{
				var coord = new CellCoordinates(random.Next(maze.GetLength(0)), random.Next(maze.GetLength(1))); // Génère des coordonnées aléatoires

				var cell = maze[coord.Row, coord.Col]; // Récupère le type de la cellule

				// Vérification des conditions :
				// 1. La cellule est vide (Cell.EMPTY)
				// 2. La cellule n'est pas déjà dans la liste des positions vides trouvées
				// 3. La distance entre la cellule et le joueur est supérieure ou égale à la distance minimale requise
				if (cell == Cell.EMPTY &&
					!emptyPositions.Contains(coord) &&
					Math.Abs(coord.Row - playerPosition.Row) + Math.Abs(coord.Col - playerPosition.Col) >= minimumDistance)
				{
					emptyPositions.Add(coord); // Ajoute la position valide à la liste
				}
			}

			return emptyPositions;
		}

	}









	/// <summary>
	/// Représente un ennemi dans le jeu, avec un comportement spécifique et des états.
	/// </summary>
	public sealed class Enemy : Entity // Un ennemi est une entité du jeu
	{
		/// <summary>
		/// État actuel de l'ennemi (effrayé, chasse, dispersion, etc.).
		/// </summary>
		public EnemyState State { get; private set; } = EnemyState.CHASE; // État initial : effrayé

		/// <summary>
		/// Comportement de l'ennemi, qui détermine comment il se déplace et prend des décisions.
		/// </summary>
		public IEnemyBehavior EnemyBehavior { get; private set; }

		// Générateur de nombres aléatoires pour les décisions aléatoires
		private readonly Random m_Random = new();

		/// <summary>
		/// Moment où le mode de déplacement SCATTER a été activé pour la dernière fois.
		/// </summary>
		private DateTime lastScatterStartTime;

		/// <summary>
		/// Durée pendant laquelle l'ennemi reste en mode SCATTER (en secondes).
		/// </summary>
		private const int scatterDurationSeconds = 30;

		/// <summary>
		/// Type de cellule précédemment occupé par l'ennemi (utilisé pour restaurer la cellule après son passage).
		/// </summary>
		public Cell m_PreviousKind { get; private set; } = Cell.COIN;

		/// <summary>
		/// Constructeur de la classe Enemy.
		/// </summary>
		/// <param name="name">Nom de l'ennemi.</param>
		/// <param name="enemyBehavior">Comportement de l'ennemi.</param>
		/// <param name="kind">Type de cellule représentant l'ennemi dans le labyrinthe.</param>
		public Enemy(string name, IEnemyBehavior enemyBehavior, Cell kind)
		{
			Kind = kind;      // Définit le type de cellule de l'ennemi
			Name = name;      // Définit le nom de l'ennemi
			EnemyBehavior = enemyBehavior; // Définit le comportement de l'ennemi
		}

		/// <summary>
		/// Définit la position de départ de l'ennemi dans le labyrinthe.
		/// </summary>
		/// <param name="start">Les coordonnées de la position de départ.</param>
		/// <param name="maze">Le labyrinthe du jeu.</param>
		public void SetStartingPosition(CellCoordinates start, Cell[,] maze)
		{
			StartPosition = start;  // Enregistre la position de départ
			Position = start;       // Place l'ennemi à sa position de départ
			maze[Position.Row, Position.Col] = Kind; // Met à jour le labyrinthe avec la cellule de l'ennemi
		}

		/// <summary>
		/// Déplace l'ennemi dans le labyrinthe en fonction de son état actuel.
		/// </summary>
		/// <param name="maze">Le labyrinthe représenté par un tableau 2D de cellules.</param>
		/// <param name="direction">La direction actuelle du joueur (utilisée pour certains comportements).</param>
		public void Move(Cell[,] maze, Direction direction)
		{

			// Gestion du changement d'état en fonction du temps et de la position du joueur
			if (State == EnemyState.CHASE && Position == Algorithms.FindPlayer(maze)) // Si l'ennemi est en mode CHASE et atteint le joueur
			{
				foreach (var enemy in Enemies.enemies.ToEnumerable()) // Pour chaque ennemi
				{
					enemy.ChangeEnemyState(EnemyState.FRIGHTENED); // Passe en mode EFFRAYÉ
					enemy.lastScatterStartTime = DateTime.Now; // Enregistre le moment du changement d'état
				}
			}
			else if (State == EnemyState.FRIGHTENED && (DateTime.Now - lastScatterStartTime).TotalSeconds >= scatterDurationSeconds) // Si l'ennemi est en mode SCATTER et que 30 secondes se sont écoulées
			{
				foreach (var enemy in Enemies.enemies.ToEnumerable()) // Pour chaque ennemi
				{
					enemy.ChangeEnemyState(EnemyState.CHASE); // Passe en mode CHASE
				}
			}
			// Si l'ennemi est effrayé, il se déplace aléatoirement
			if (State == EnemyState.FRIGHTENED || State == EnemyState.SCATTER)
			{
				if (!MoveRandom(maze)) return; // Si le déplacement aléatoire échoue, ne pas continuer
			}
			else // Sinon, il suit son comportement normal
			{
				// Calcule la prochaine position en fonction de son comportement et de la direction du joueur
				NextPosition = EnemyBehavior.NextPosition(maze, Position, direction);

				// Détermine la direction du mouvement vers la prochaine position
				CurrentDirection = GetDirection(Position, NextPosition);
			}

			// Obtient le type de cellule statique sur la prochaine position (ignore le joueur et les autres ennemis)
			Cell nextCell = GetStaticEntity(maze[NextPosition.Row, NextPosition.Col]);

			// Met à jour la position de l'ennemi dans le labyrinthe
			UpdatePosition(NextPosition, maze, m_PreviousKind);

			// Enregistre le type de cellule précédemment occupé pour le restaurer plus tard
			m_PreviousKind = nextCell;
		}

		/// <summary>
		/// Effectue un déplacement aléatoire de l'ennemi dans le labyrinthe.
		/// </summary>
		/// <param name="maze">Le labyrinthe représenté par un tableau 2D de cellules.</param>
		/// <returns>True si le déplacement est réussi, false sinon.</returns>
		public bool MoveRandom(Cell[,] maze)
		{
			var currentPos = Position;     // Position actuelle de l'ennemi
			var dir = CurrentDirection;   // Direction actuelle de l'ennemi
			NextPosition = GetNextPosition(currentPos, dir); // Calcule la prochaine position en fonction de la direction actuelle

			// Vérifie si le chemin est bloqué (hors limites, mur ou occupé par une autre entité)
			bool pathBlocked = !IsInBounds(NextPosition, maze) || maze[NextPosition.Row, NextPosition.Col] == Cell.WALL || IsOccupied(NextPosition, maze);

			// Génère un nombre aléatoire entre 1 et 3 (inclus)
			int randomDecision = m_Random.Next(1, 4);

			// Si le chemin est bloqué ou si le nombre aléatoire indique un changement de direction
			if (pathBlocked || randomDecision != 1)
			{
				var alternativeDirections = GetAlternativeDirections(dir); // Obtient les directions alternatives possibles (sauf l'opposé)
				bool foundValidDirection = false;  // Indicateur pour savoir si une direction valide a été trouvée

				// Essaie toutes les directions alternatives sauf l'opposé en premier
				foreach (var altDir in alternativeDirections)
				{
					NextPosition = GetNextPosition(currentPos, altDir);
					if (IsInBounds(NextPosition, maze) && maze[NextPosition.Row, NextPosition.Col] != Cell.WALL) // Si la direction est valide
					{
						dir = altDir;           // Change la direction
						foundValidDirection = true; // Indique qu'une direction valide a été trouvée
						break; // Sort de la boucle
					}
				}

				// Si aucune direction valide n'a été trouvée, essaie la direction opposée
				if (!foundValidDirection)
				{
					var oppositeDir = GetOppositeDirection(CurrentDirection); // Obtient la direction opposée
					NextPosition = GetNextPosition(currentPos, oppositeDir);
					if (IsInBounds(NextPosition, maze) && maze[NextPosition.Row, NextPosition.Col] != Cell.WALL) // Si la direction opposée est valide
						dir = oppositeDir; // Change la direction
				}
			}
			// Vérifie si la nouvelle position est valide (dans les limites et pas un mur)
			bool isValid = IsInBounds(NextPosition, maze) && maze[NextPosition.Row, NextPosition.Col] != Cell.WALL;
			if (isValid) CurrentDirection = dir; // Met à jour la direction si la nouvelle position est valide
			return isValid; // Renvoie true si le déplacement est valide, false sinon
		}

		/// <summary>
		/// Vérifie si une cellule donnée contient une entité dynamique (joueur ou autre ennemi différent de l'ennemi actuel).
		/// </summary>
		/// <param name="cell">Le type de cellule à vérifier.</param>
		/// <returns>True si la cellule contient une entité dynamique, false sinon.</returns>
		private bool IsDynamicEntity(Cell cell)
		{
			return cell == Cell.JOHN || // Joueur
				   (cell == Cell.WINSTON && cell != Kind) || // Fantôme Winston (différent de l'ennemi actuel)
				   (cell == Cell.CAIN && cell != Kind) ||    // Fantôme Cain (différent de l'ennemi actuel)
				   (cell == Cell.VIGGO && cell != Kind) ||    // Fantôme Viggo (différent de l'ennemi actuel)
				   (cell == Cell.MARQUIS && cell != Kind);   // Fantôme Marquis (différent de l'ennemi actuel)
		}

		/// <summary>
		/// Obtient le type d'entité statique dans une cellule, en considérant les entités dynamiques comme vides.
		/// </summary>
		/// <param name="cell">Le type de cellule à vérifier.</param>
		/// <returns>Le type de cellule statique (mur, pièce, etc.), ou Cell.EMPTY si la cellule contient une entité dynamique.</returns>
		private Cell GetStaticEntity(Cell cell)
		{
			// Retourne Cell.EMPTY si c'est une entité dynamique (joueur ou autre ennemi), sinon retourne le type de cellule original
			return IsDynamicEntity(cell) ? Cell.EMPTY : cell;
		}

		/// <summary>
		/// Change l'état actuel de l'ennemi.
		/// </summary>
		/// <param name="newState">Le nouvel état de l'ennemi.</param>
		public void ChangeEnemyState(EnemyState newState)
		{
			State = newState; // Met à jour l'état
		}

		/// <summary>
		/// Obtient un tableau des directions alternatives possibles pour l'ennemi, en excluant la direction opposée à sa direction actuelle.
		/// </summary>
		/// <param name="currentDirection">La direction actuelle de l'ennemi.</param>
		/// <returns>Un tableau de directions alternatives (Direction[]).</returns>
		public static Direction[] GetAlternativeDirections(Direction currentDirection)
		{
			var directions = new[] { Direction.UP, Direction.DOWN, Direction.LEFT, Direction.RIGHT }; // Toutes les directions
			var random = new Random(); // Générateur de nombres aléatoires

			// Filtre les directions pour exclure la direction opposée, puis mélange les directions restantes
			return directions.Where(d => d != GetOppositeDirection(currentDirection)).OrderBy(d => random.Next()).ToArray();
		}

		/// <summary>
		/// Obtient la direction opposée à une direction donnée.
		/// </summary>
		/// <param name="direction">La direction.</param>
		/// <returns>La direction opposée.</returns>
		public static Direction GetOppositeDirection(Direction direction)
		{
			return direction switch // Expression switch pour déterminer la direction opposée
			{
				Direction.UP => Direction.DOWN,
				Direction.DOWN => Direction.UP,
				Direction.LEFT => Direction.RIGHT,
				Direction.RIGHT => Direction.LEFT,
				_ => direction, // Retourne la direction originale si elle est invalide
			};
		}

		/// <summary>
		/// Vérifie si une position donnée dans le labyrinthe est occupée par une entité statique ou dynamique.
		/// </summary>
		/// <param name="position">Les coordonnées de la position à vérifier.</param>
		/// <param name="maze">Le labyrinthe représenté par un tableau 2D de cellules.</param>
		/// <returns>True si la position est occupée, false sinon.</returns>
		public static bool IsOccupied(CellCoordinates position, Cell[,] maze)
		{
			// Vérifie si la cellule à la position donnée est différente de Cell.EMPTY et des autres cellules non-occupées
			return maze[position.Row, position.Col] != Cell.EMPTY && maze[position.Row, position.Col] != Cell.START
				   && maze[position.Row, position.Col] != Cell.END && maze[position.Row, position.Col] != Cell.JOHN
				   && maze[position.Row, position.Col] != Cell.HEALTH_KIT && maze[position.Row, position.Col] != Cell.COIN
				   && maze[position.Row, position.Col] != Cell.TORCH && maze[position.Row, position.Col] != Cell.KEY;
		}

	}
}

