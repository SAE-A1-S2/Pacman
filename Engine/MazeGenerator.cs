// ReSharper disable CommentTypo
namespace Engine
{
	public class MazeGenerator
	{
		private readonly int m_Width; // La largeur du labyrinthe.
		private readonly int m_Height; // La hauteur du labyrinthe.
		public Cell[,] Map { get; private set; } // La grille représentant le labyrinthe.
		private readonly Random m_Random = new(); // Instance pour la génération de nombres aléatoires.
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
			m_Width = width;
			m_Height = height;
			Map = new Cell[m_Height, m_Width];
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
			m_Width = width;
			m_Height = height;
			Map = map;
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
			List<CellCoordinates> neighbors = [];

			// Définit les directions possibles de déplacement dans le labyrinthe.
			// Chaque direction est un déplacement de deux cellules dans l'une des quatre directions cardinales,
			// ce qui permet de créer des passages entre les cellules visitées.
			(int, int)[] directions = [(0, 2), (0, -2), (2, 0), (-2, 0)];

			// Parcourt chaque direction possible.
			foreach (var direction in directions)
			{
				// Calcule les coordonnées du voisin potentiel en ajoutant la direction actuelle aux coordonnées de la cellule actuelle.
				CellCoordinates neighbor = new(cell.Row + direction.Item1, cell.Col + direction.Item2);

				// Vérifie si le voisin potentiel est dans les limites du labyrinthe et si c'est un mur (non visité).
				if (IsInBounds(neighbor) && Map[neighbor.Row, neighbor.Col] == Cell.WALL)
					// Si les conditions sont remplies, ajoute les coordonnées de ce voisin à la liste des voisins non visités.
					neighbors.Add(neighbor);
			}

			// Retourne la liste des voisins non visités.
			return neighbors;
		}


		// Vérifie si les coordonnées sont à l'intérieur des limites du labyrinthe
		private bool IsInBounds(CellCoordinates cell)
		{
			return cell.Row >= 0 && cell.Row < m_Height && cell.Col >= 0 && cell.Col < m_Width;
		}

		// Enlève le mur entre deux cellules
		private void RemoveWallBetween(CellCoordinates a, CellCoordinates b)
		{
			// Calculez la cellule du mur entre a et b
			CellCoordinates wall = new((a.Row + b.Row) / 2, (a.Col + b.Col) / 2);
			Map[wall.Row, wall.Col] = Cell.EMPTY;
		}

		/// <summary>
		/// Génère un labyrinthe de manière itérative en utilisant l'algorithme de parcours en profondeur.
		/// </summary>
		/// <param name="start">Les coordonnées de la cellule de départ pour la génération du labyrinthe.</param>
		private void GenerateDFSMazeIterative(CellCoordinates start)
		{
			// Crée une pile pour stocker les cellules à visiter.
			Stack<CellCoordinates> stack = new();
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
					CellCoordinates next = neighbors[m_Random.Next(neighbors.Count)];

					// Enlève le mur entre la cellule courante et la cellule voisine choisie.
					RemoveWallBetween(current, next);

					// Marque la cellule voisine comme visitée (chemin ouvert).
					Map[next.Row, next.Col] = Cell.EMPTY;

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
			for (int x = 0; x < m_Height; x++)
				for (int y = 0; y < m_Width; y++)
					Map[x, y] = Cell.WALL;

			// Déclare les variables pour les coordonnées de départ et de fin.
			CellCoordinates start, end;
			// Boucle pour s'assurer que les points de départ et d'arrivée ne sont pas les mêmes.
			do
			{
				// Attribue aléatoirement des coordonnées de départ et d'arrivée.
				start = new CellCoordinates(m_Random.Next(0, m_Height), m_Random.Next(0, m_Width));
				end = new CellCoordinates(m_Random.Next(0, m_Height), m_Random.Next(0, m_Width));
			} while (start.Row == end.Row && start.Col == end.Col);

			// Place le point de départ dans le labyrinthe.
			Map[start.Row, start.Col] = Cell.START;
			Start = new CellCoordinates(start.Row, start.Col);

			// Utilise l'algorithme DFS itératif pour générer le labyrinthe à partir du point de départ.
			GenerateDFSMazeIterative(start);

			// Boucle pour trouver une cellule vide pour la sortie.
			// Continue de générer des coordonnées aléatoires jusqu'à ce qu'une cellule vide soit trouvée.
			while (Map[end.Row, end.Col] != Cell.EMPTY)
				end = new CellCoordinates(m_Random.Next(0, m_Height), m_Random.Next(0, m_Width));

			// Marque la cellule d'arrivée trouvée comme la sortie du labyrinthe.
			Map[end.Row, end.Col] = Cell.END;
			End = new CellCoordinates(end.Row, end.Col);

			EnhanceMazeWithLoops();
		}

		/// <summary>
		/// Améliore le labyrinthe en créant des boucles potentielles en supprimant certains murs.
		/// Cette méthode prend en compte à la fois les cellules entourées de trois murs ET les cellules avec une seule direction de mouvement possible.
		/// </summary>
		private void EnhanceMazeWithLoops()
		{
			// Parcours toutes les cellules du labyrinthe
			for (int y = 1; y < m_Height; y++) // Lignes 
			{
				for (int x = 1; x < m_Width; x++) // Colonnes 
				{
					// Vérifie si la cellule est vide, le point de départ ou le point d'arrivée
					if (Map[y, x] == Cell.EMPTY || Map[y, x] == Cell.START || Map[y, x] == Cell.END)
					{
						List<CellCoordinates> surroundingWalls = []; // Murs entourant la cellule
						List<CellCoordinates> validWalls = [];       // Murs potentiellement supprimables

						// Vérifie les cellules voisines (haut, bas, gauche, droite) pour identifier les murs
						if (IsInBounds(new CellCoordinates(y - 1, x)) && Map[y - 1, x] == Cell.WALL) surroundingWalls.Add(new CellCoordinates(y - 1, x)); // Haut
						if (IsInBounds(new CellCoordinates(y + 1, x)) && Map[y + 1, x] == Cell.WALL) surroundingWalls.Add(new CellCoordinates(y + 1, x)); // Bas
						if (IsInBounds(new CellCoordinates(y, x - 1)) && Map[y, x - 1] == Cell.WALL) surroundingWalls.Add(new CellCoordinates(y, x - 1)); // Gauche
						if (IsInBounds(new CellCoordinates(y, x + 1)) && Map[y, x + 1] == Cell.WALL) surroundingWalls.Add(new CellCoordinates(y, x + 1)); // Droite

						// Condition 1 : Si la cellule est entourée de trois murs, on peut potentiellement en supprimer un pour créer une boucle
						// Condition 2 : Si la cellule a seulement une direction valide de mouvement (entourée de 2 murs + bord du labyrinthe)
						if (surroundingWalls.Count == 3 || (surroundingWalls.Count == 2 && HasOnlyOneValidDirection(Map, x, y)))
						{
							// Vérifie si la suppression d'un mur crée un nouveau chemin valide
							foreach (var wall in surroundingWalls)
							{
								int wallRow = wall.Row;
								int wallCol = wall.Col;

								// Vérifie si la suppression du mur connecterait deux passages distincts (horizontalement ou verticalement)
								if (
									(IsInBounds(new CellCoordinates(wallRow - 1, wallCol)) && Map[wallRow - 1, wallCol] == Cell.EMPTY && IsInBounds(new CellCoordinates(wallRow + 1, wallCol)) && Map[wallRow + 1, wallCol] == Cell.EMPTY) ||
									(IsInBounds(new CellCoordinates(wallRow, wallCol - 1)) && Map[wallRow, wallCol - 1] == Cell.EMPTY && IsInBounds(new CellCoordinates(wallRow, wallCol + 1)) && Map[wallRow, wallCol + 1] == Cell.EMPTY)
								)
								{
									validWalls.Add(wall); // Ajoute le mur à la liste des murs pouvant être supprimés
								}
							}

							// Supprime un mur aléatoire parmi les murs valides (s'il y en a)
							if (validWalls.Count > 0)
							{
								CellCoordinates wallToRemove = validWalls[m_Random.Next(validWalls.Count)];
								Map[wallToRemove.Row, wallToRemove.Col] = Cell.EMPTY;
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Vérifie si une cellule donnée a une seule direction de mouvement valide (entourée de 2 murs et d'un bord du labyrinthe).
		/// </summary>
		/// <param name="maze">Le labyrinthe représenté par un tableau 2D de cellules.</param>
		/// <param name="x">La colonne de la cellule à vérifier.</param>
		/// <param name="y">La ligne de la cellule à vérifier.</param>
		/// <returns>True si la cellule a une seule direction valide, false sinon.</returns>
		private bool HasOnlyOneValidDirection(Cell[,] maze, int x, int y)
		{
			int validDirections = 0;
			if (y > 0 && maze[y - 1, x] != Cell.WALL) validDirections++; // Haut
			if (y < m_Height - 1 && maze[y + 1, x] != Cell.WALL) validDirections++; // Bas
			if (x > 0 && maze[y, x - 1] != Cell.WALL) validDirections++; // Gauche
			if (x < m_Width - 1 && maze[y, x + 1] != Cell.WALL) validDirections++; // Droite
			return validDirections == 1;
		}

	}
}
