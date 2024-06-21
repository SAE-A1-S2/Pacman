namespace Engine.utils
{
	/// <summary>
	/// Classe utilitaire pour trouver un chemin eulérien dans un labyrinthe.
	/// </summary>
	public class EulerianPathFinder
	{
		private bool[,] visited; // Tableau pour marquer les arêtes visitées
		public List<CellCoordinates> path { get; private set; } // Le chemin eulérien trouvé

		/// <summary>
		/// Constructeur de la classe EulerianPathFinder.
		/// Initialise le labyrinthe et le tableau des arêtes visitées.
		/// </summary>
		/// <param name="maze">Le labyrinthe représenté sous forme de tableau 2D de cellules.</param>
		public EulerianPathFinder(Cell[,] maze, CellCoordinates start)
		{
			visited = new bool[maze.GetLength(0), maze.GetLength(1)];
			path = FindEulerianPath(maze, start);
		}

		/// <summary>
		/// Trouve un chemin eulérien dans le labyrinthe.
		/// </summary>
		/// <returns>Une liste de CellCoordinates représentant le chemin eulérien, ou null si aucun chemin n'existe.</returns>
		public List<CellCoordinates> FindEulerianPath(Cell[,] maze, CellCoordinates start)
		{
			if (!IsEulerian(maze)) // Vérifie si le labyrinthe admet un chemin eulérien
				return null;

			FindEulerianPathUtil(maze, start); // Lance la recherche du chemin eulérien à partir du nœud de départ
			path.Reverse(); // Inverse le chemin pour obtenir l'ordre correct
			return path;
		}

		/// <summary>
		/// Fonction récursive utilitaire pour trouver le chemin eulérien.
		/// </summary>
		/// <param name="u">La cellule actuelle.</param>
		private void FindEulerianPathUtil(Cell[,] maze, CellCoordinates u)
		{
			// Parcours tous les voisins non visités de la cellule actuelle
			foreach (CellCoordinates v in Static.GetNeighbors(maze, u))
			{
				if (!visited[u.Row, v.Col]) // Si l'arête (u, v) n'a pas été visitée
				{
					visited[u.Row, v.Col] = true; // Marque l'arête comme visitée
					visited[v.Row, u.Col] = true; // Marque l'arête inverse également
					FindEulerianPathUtil(maze, v);  // Appel récursif pour explorer le voisin
					path.Add(u); // Ajoute la cellule actuelle au chemin après avoir exploré ses voisins
				}
			}
		}

		/// <summary>
		/// Vérifie si le labyrinthe admet un chemin eulérien.
		/// </summary>
		/// <returns>True si le labyrinthe est eulérien, false sinon.</returns>
		private bool IsEulerian(Cell[,] maze)
		{
			int odd = 0;
			for (int i = 0; i < maze.GetLength(0); i++)
			{
				for (int j = 0; j < maze.GetLength(1); j++)
				{
					if (GetDegree(maze, new CellCoordinates(i, j)) % 2 != 0) // Compte les nœuds de degré impair
						odd++;
				}
			}
			return odd == 0 || odd == 2; // Un graphe eulérien a soit 0, soit 2 nœuds de degré impair
		}

		/// <summary>
		/// Calcule le degré d'un nœud (cellule) dans le labyrinthe.
		/// Le degré d'un nœud est le nombre d'arêtes (passages) connectées à ce nœud.
		/// </summary>
		/// <param name="cell">Les coordonnées de la cellule dont on veut calculer le degré.</param>
		/// <returns>Le degré de la cellule.</returns>
		private int GetDegree(Cell[,] maze, CellCoordinates cell)
		{
			int degree = 0;
			int[] dx = [-1, 1, 0, 0]; // Déplacements possibles pour les lignes (haut, bas)
			int[] dy = [0, 0, -1, 1]; // Déplacements possibles pour les colonnes (gauche, droite)

			// Parcours les directions possibles
			for (int i = 0; i < 4; i++)
			{
				int newRow = cell.Row + dx[i];
				int newCol = cell.Col + dy[i];
				// Vérifie si le voisin est dans les limites du labyrinthe et n'est pas un mur
				if (Entity.IsInBounds(new CellCoordinates(newRow, newCol), maze) && maze[newRow, newCol] != Cell.WALL)
					degree++; // Incrémente le degré si le voisin est valide
			}

			return degree;
		}
	}
}
