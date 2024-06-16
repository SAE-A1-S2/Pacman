// ReSharper disable CommentTypo
namespace Engine
{
	public class MazeGenerator
	{
		private readonly int _width; // La largeur du labyrinthe.
		private readonly int _height; // La hauteur du labyrinthe.
		public Cell[,] _map { get; private set; } // La grille représentant le labyrinthe.
		private readonly Random random = new(); // Instance pour la génération de nombres aléatoires.
		public CellCoordinates Start { get; private set; } // Les coordonnées du point de départ du labyrinthe.
		public CellCoordinates End { get; private set; } // Les coordonnées du point d'arrêt du labyrinthe.

		/// <summary>
		/// Constructeur de la classe MazeGenerator
		/// Initialise la largeur et la hauteur du labyrinthe
		/// et crée une grille de cellules de taille width x height
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public MazeGenerator(int width, int height)
		{
			_width = width;
			_height = height;
			_map = new Cell[_height, _width];
			GenerateLevel();
		}

		/// <summary>
		/// Constructeur de la classe MazeGenerator
		/// Initialise la largeur et la hauteur du labyrinthe, la grille de cellules et les coordonnées du point de depart et d'arrêt.
		/// Elle sert de point pour la récupération depuis la base de données.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="map"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		public MazeGenerator(int width, int height, Cell[,] map, CellCoordinates start, CellCoordinates end)
		{
			_width = width;
			_height = height;
			_map = map;
			Start = start;
			End = end;
		}


		/// <summary>
		/// permet d'obteniur une liste des coordonnées des cellules voisines non visitées à partir d'une cellule spécifiée.
		/// Une cellule voisine est considérée comme non visitée si elle est actuellement un mur, ce qui indique
		/// qu'elle n'a pas été incorporée dans le chemin du labyrinthe.
		/// </summary>
		/// <param name="cell">Les coordonnées de la cellule actuelle.</param>
		/// <returns>Une liste des coordonnées des cellules voisines non visitées.</returns>
		private List<CellCoordinates> GetUnvisitedNeighbors(CellCoordinates cell)
		{
			// Initialise une nouvelle liste pour stocker les voisins non visités.
			List<CellCoordinates> neighbors = new List<CellCoordinates>();

			// Définit les directions possibles de déplacement dans le labyrinthe.
			// Chaque direction est un déplacement de deux cellules dans l'une des quatre directions cardinales,
			// ce qui permet de créer des passages entre les cellules visitées.
			(int, int)[] directions = { (0, 2), (0, -2), (2, 0), (-2, 0) };

			// Parcourt chaque direction possible.
			foreach (var direction in directions)
			{
				// Calcule les coordonnées du voisin potentiel en ajoutant la direction actuelle aux coordonnées de la cellule actuelle.
				CellCoordinates neighbor = new CellCoordinates(cell.row + direction.Item1, cell.col + direction.Item2);

				// Vérifie si le voisin potentiel est dans les limites du labyrinthe et si c'est un mur (non visité).
				if (IsInBounds(neighbor) && _map[neighbor.row, neighbor.col] == Cell.Wall)
					// Si les conditions sont remplies, ajoute les coordonnées de ce voisin à la liste des voisins non visités.
					neighbors.Add(neighbor);
			}

			// Retourne la liste des voisins non visités.
			return neighbors;
		}


		// Vérifie si les coordonnées sont à l'intérieur des limites du labyrinthe
		private bool IsInBounds(CellCoordinates cell)
		{
			return cell.row >= 0 && cell.row < _height && cell.col >= 0 && cell.col < _width;
		}

		// Enlève le mur entre deux cellules
		private void RemoveWallBetween(CellCoordinates a, CellCoordinates b)
		{
			// Calculez la cellule du mur entre a et b
			CellCoordinates wall = new CellCoordinates((a.row + b.row) / 2, (a.col + b.col) / 2);
			_map[wall.row, wall.col] = Cell.Empty;
		}

		/// <summary>
		/// Génère un labyrinthe de manière itérative en utilisant l'algorithme de parcours en profondeur.
		/// </summary>
		/// <param name="start">Les coordonnées de la cellule de départ pour la génération du labyrinthe.</param>
		private void GenerateDFSMazeIterative(CellCoordinates start)
		{
			// Crée une pile pour stocker les cellules à visiter.
			Stack<CellCoordinates> stack = new Stack<CellCoordinates>();
			// Ajoute la cellule de départ dans la pile.
			stack.Push(start);

			// Continue tant qu'il y a des cellules dans la pile.
			while (stack.Count > 0)
			{
				// Récupère la cellule courante en haut de la pile sans la retirer.
				CellCoordinates current = stack.Pop();

				// Obtient une liste des voisins non visités de la cellule courante.
				var neighbors = GetUnvisitedNeighbors(current);

				// Vérifie si la cellule courante a des voisins non visités.
				if (neighbors.Count > 0)
				{
					// remplace la cellule courante dans la pile pour y revenir plus tard.
					stack.Push(current);

					// Choisissez un voisin non visité au hasard.
					CellCoordinates next = neighbors[random.Next(neighbors.Count)];

					// Enlève le mur entre la cellule courante et la cellule voisine choisie.
					RemoveWallBetween(current, next);

					// Marque la cellule voisine comme visitée (chemin ouvert).
					_map[next.row, next.col] = Cell.Empty;

					// Ajoute la cellule voisine dans la pile pour continuer le parcours à partir de ce point.
					stack.Push(next);
				}
			}
		}

		/// <summary>
		/// Génère un labyrinthe complet avec des points de départ et d'arrivée désignés.
		/// La méthode prépare d'abord le terrain en initialisant toutes les cellules comme des murs,
		/// puis elle utilise GenerateDFSMazeIterative pour créer des chemins.
		/// </summary>
		private void GenerateLevel()
		{
			// Remplit toute la grille avec des murs pour commencer avec un labyrinthe 'vierge'.
			for (int x = 0; x < _height; x++)
				for (int y = 0; y < _width; y++)
					_map[x, y] = Cell.Wall;

			// Déclare les variables pour les coordonnées de départ et de fin.
			CellCoordinates start, end;
			// Boucle pour s'assurer que les points de départ et d'arrivée ne sont pas les mêmes.
			do
			{
				// Attribue aléatoirement des coordonnées de départ et d'arrivée.
				start = new CellCoordinates(random.Next(0, _height), random.Next(0, _width));
				end = new CellCoordinates(random.Next(0, _height), random.Next(0, _width));
			} while (start.row == end.row && start.col == end.col);

			// Place le point de départ dans le labyrinthe.
			_map[start.row, start.col] = Cell.Start;
			Start = new CellCoordinates(start.row, start.col);

			// Utilise l'algorithme DFS itératif pour générer le labyrinthe à partir du point de départ.
			GenerateDFSMazeIterative(start);

			// Boucle pour trouver une cellule vide pour la sortie.
			// Continue de générer des coordonnées aléatoires jusqu'à ce qu'une cellule vide soit trouvée.
			while (_map[end.row, end.col] != Cell.Empty)
				end = new CellCoordinates(random.Next(0, _height), random.Next(0, _width));

			// Marque la cellule d'arrivée trouvée comme la sortie du labyrinthe.
			_map[end.row, end.col] = Cell.End;
			End = new CellCoordinates(end.row, end.col);

			EnhanceMazeWithLoops();
		}

		private void EnhanceMazeWithLoops()
		{
			for (int y = 1; y < _height - 1; y++)
			{
				for (int x = 1; x < _width - 1; x++)
				{
					if (_map[y, x] == Cell.Empty || _map[y, x] == Cell.Start || _map[y, x] == Cell.End)
					{
						List<CellCoordinates> surroundingWalls = new List<CellCoordinates>();
						List<CellCoordinates> validWalls = new List<CellCoordinates>();

						// Check surrounding cells
						if (_map[y - 1, x] == Cell.Wall) surroundingWalls.Add(new CellCoordinates(y - 1, x)); // above
						if (_map[y + 1, x] == Cell.Wall) surroundingWalls.Add(new CellCoordinates(y + 1, x)); // below
						if (_map[y, x - 1] == Cell.Wall) surroundingWalls.Add(new CellCoordinates(y, x - 1)); // left
						if (_map[y, x + 1] == Cell.Wall) surroundingWalls.Add(new CellCoordinates(y, x + 1)); // right

						// If there are exactly three surrounding walls, consider removing one
						if (surroundingWalls.Count == 3)
						{
							// Check if removing a wall creates a valid new path
							foreach (var wall in surroundingWalls)
							{
								int wallRow = wall.row;
								int wallCol = wall.col;

								// Check if removing the wall will connect two distinct passages
								if ((IsInBounds(new CellCoordinates(wallRow - 1, wallCol)) && _map[wallRow - 1, wallCol] == Cell.Empty && IsInBounds(new CellCoordinates(wallRow + 1, wallCol)) && _map[wallRow + 1, wallCol] == Cell.Empty) ||
									(IsInBounds(new CellCoordinates(wallRow, wallCol - 1)) && _map[wallRow, wallCol - 1] == Cell.Empty && IsInBounds(new CellCoordinates(wallRow, wallCol + 1)) && _map[wallRow, wallCol + 1] == Cell.Empty))
								{
									validWalls.Add(wall);
								}
							}

							// Remove one of the valid walls if any
							if (validWalls.Count > 0)
							{
								CellCoordinates wallToRemove = validWalls[random.Next(validWalls.Count)];
								_map[wallToRemove.row, wallToRemove.col] = Cell.Empty;
							}
						}
					}
				}
			}
		}
	}
}
