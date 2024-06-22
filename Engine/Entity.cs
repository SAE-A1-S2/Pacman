using Engine.utils;

namespace Engine
{
	/// <summary>
	/// Classe de base pour les entités du jeu (joueur, fantômes).
	/// </summary>
	public class Entity
	{
		/// <summary>
		/// Obtient ou définit la position actuelle de l'entité dans le labyrinthe.
		/// </summary>
		public CellCoordinates Position { get; protected set; }

		/// <summary>
		/// Obtient ou définit le nom de l'entité.
		/// </summary>
		public string Name { get; protected set; } = ""; // Nom par défaut vide

		/// <summary>
		/// Obtient ou définit le type de cellule (Cell) que l'entité représente dans le labyrinthe.
		/// </summary>
		public Cell Kind { get; protected set; }

		/// <summary>
		/// Obtient ou définit la direction actuelle de l'entité.
		/// </summary>
		public Direction CurrentDirection { get; protected set; }

		/// <summary>
		/// Obtient ou définit la position de départ de l'entité dans le labyrinthe.
		/// </summary>
		public CellCoordinates StartPosition { get; set; }

		public CellCoordinates NextPosition { get; protected set; }


		/// <summary>
		/// Calcule la prochaine position de l'entité en fonction de sa position actuelle et de la direction donnée.
		/// </summary>
		/// <param name="currentPosition">Position actuelle de l'entité.</param>
		/// <param name="direction">Direction dans laquelle l'entité se déplace.</param>
		/// <returns>Les nouvelles coordonnées de l'entité après le déplacement.</returns>
		public static CellCoordinates GetNextPosition(CellCoordinates currentPosition, Direction direction)
		{
			// Utilise une expression switch pour déterminer les nouvelles coordonnées en fonction de la direction
			return direction switch
			{
				Direction.UP => new CellCoordinates(currentPosition.Row - 1, currentPosition.Col),       // Haut
				Direction.DOWN => new CellCoordinates(currentPosition.Row + 1, currentPosition.Col),    // Bas
				Direction.LEFT => new CellCoordinates(currentPosition.Row, currentPosition.Col - 1),    // Gauche
				Direction.RIGHT => new CellCoordinates(currentPosition.Row, currentPosition.Col + 1),   // Droite
				_ => currentPosition, // Si la direction est invalide, l'entité ne bouge pas (retourne la position actuelle)
			};
		}

		/// <summary>
		/// Met à jour la position de l'entité dans le labyrinthe.
		/// </summary>
		/// <param name="newCell">Les nouvelles coordonnées de l'entité.</param>
		/// <param name="maze">Le labyrinthe représenté sous forme de tableau 2D de cellules.</param>
		public void UpdatePosition(CellCoordinates newCell, Cell[,] maze, Cell previousCell = Cell.EMPTY)
		{
			maze[Position.Row, Position.Col] = previousCell;
			maze[newCell.Row, newCell.Col] = Kind;
			Position = newCell;
		}

		/// <summary>
		/// Met à jour la position d'une entité dans le labyrinthe et gère l'historique des états des cellules.
		/// </summary>
		/// <param name="entity">L'entité à déplacer.</param>
		/// <param name="newPosition">La nouvelle position de l'entité.</param>
		/// <param name="maze">Le labyrinthe représenté par un tableau 2D de cellules.</param>
		/// <param name="cellHistory">L'historique des cellules.</param>
		public void UpdatePosition(Entity entity, CellCoordinates newPosition, Cell[,] maze, Dictionary<CellCoordinates, Stack<Cell>> cellHistory)
		{
			CellCoordinates oldPosition = entity.Position;

			if (!cellHistory.ContainsKey(newPosition) || maze[newPosition.Row, newPosition.Col] == Cell.EMPTY)
			{
				cellHistory[newPosition] = new Stack<Cell>();
				cellHistory[newPosition].Push(maze[newPosition.Row, newPosition.Col]);
			}

			if (cellHistory.TryGetValue(oldPosition, out Stack<Cell>? value) && value.Count > 0 && value.Peek() != entity.Kind && value.Peek() != Cell.JOHN)
			{
				maze[oldPosition.Row, oldPosition.Col] = value.Peek();
			}
			else
				maze[oldPosition.Row, oldPosition.Col] = Cell.COIN;

			maze[newPosition.Row, newPosition.Col] = entity.Kind;
			entity.Position = newPosition;
		}

		/// <summary>
		/// Vérifie si une cellule donnée est à l'intérieur des limites du labyrinthe.
		/// </summary>
		/// <param name="cell">Les coordonnées de la cellule à vérifier.</param>
		/// <param name="maze">Le labyrinthe représenté sous forme de tableau 2D de cellules.</param>
		/// <returns>Vrai si la cellule est dans les limites, faux sinon.</returns>
		public static bool IsInBounds(CellCoordinates cell, Cell[,] maze)
		{
			return cell.Row >= 0 && cell.Row < maze.GetLength(0) && cell.Col >= 0 && cell.Col < maze.GetLength(1);
		}

		/// <summary>
		/// Détermine la direction d'un personnage en comparant sa position actuelle et sa position précédente.
		/// </summary>
		/// <param name="src">La position précédente du personnage.</param>
		/// <param name="dst">La position actuelle du personnage.</param>
		/// <returns>La direction du mouvement (Direction.UP, Direction.DOWN, Direction.LEFT, Direction.RIGHT, ou Direction.STOP).</returns>
		public static Direction GetDirection(CellCoordinates src, CellCoordinates dst)
		{
			if (src.Col < dst.Col) return Direction.RIGHT; // droite
			if (src.Col > dst.Col) return Direction.LEFT;  // gauche
			if (src.Row < dst.Row) return Direction.DOWN;  // bas
			if (src.Row > dst.Row) return Direction.UP;    // haut
			return Direction.STOP;                        // Pas de mouvement (position inchangée)
		}
	}
}
