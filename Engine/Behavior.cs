namespace Engine.utils
{
	/// <summary>
	/// Interface définissant le comportement d'un ennemi dans le jeu.
	/// </summary>
	public interface IEnemyBehavior
	{
		/// <summary>
		/// Calcule la prochaine position de l'ennemi en fonction du labyrinthe et de sa position actuelle.
		/// </summary>
		/// <param name="maze">Le labyrinthe du jeu.</param>
		/// <param name="currentPosition">La position actuelle de l'ennemi.</param>
		/// <param name="direction">La direction actuelle de l'ennemi (optionnel).</param>
		/// <returns>La prochaine position vers laquelle l'ennemi doit se déplacer.</returns>
		CellCoordinates NextPosition(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP);
	}

	/// <summary>
	/// Comportement de chasseur : l'ennemi cherche à atteindre directement le joueur en utilisant l'algorithme de Bellman-Ford.
	/// </summary>
	public class ChaserBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPosition(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP)
		{
			CellCoordinates dst = Algorithms.FindPlayer(maze);       // Trouve la position du joueur
			return Algorithms.FindBellmanFord(currentPosition, dst, maze); // Calcule le chemin le plus court vers le joueur
		}
	}

	/// <summary>
	/// Comportement d'embuscade : l'ennemi tente de prévoir la position future du joueur et de l'intercepter en utilisant l'algorithme de Bellman-Ford.
	/// </summary>
	public class AmbusherBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPosition(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP)
		{
			CellCoordinates playerPosition = Algorithms.FindPlayer(maze);                                 // Trouve la position du joueur
			CellCoordinates dst = Algorithms.CalculatePositionAhead(maze, playerPosition, direction, 2); // Estime la position future du joueur
			return Algorithms.FindBellmanFord(currentPosition, dst, maze);                              // Calcule le chemin le plus court vers la position future
		}
	}

	/// <summary>
	/// Comportement lunatique : l'ennemi se déplace vers une cellule cible aléatoire en utilisant l'algorithme de Dijkstra.
	/// </summary>
	public class WhimsicalBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPosition(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP)
		{
			CellCoordinates dst = Algorithms.FindClosestCell(maze);  // Trouve une cellule cible aléatoire (clé, soin, etc.)
			return Algorithms.FindDijkstra(currentPosition, dst, maze); // Calcule le chemin le plus court vers la cible
		}
	}

	/// <summary>
	/// Comportement errant : l'ennemi se déplace de manière aléatoire dans le labyrinthe.
	/// </summary>
	public class WandererBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPosition(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP)
		{
			throw new NotImplementedException();
		}
	}
}
