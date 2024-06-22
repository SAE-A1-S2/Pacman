using System.Text;

namespace Engine.utils
{
	/// <summary>
	/// Classe utilitaire statique contenant des méthodes pour manipuler les données du labyrinthe et effectuer des calculs.
	/// </summary>
	public static class Static
	{
		/// <summary>
		/// Formate un labyrinthe (représenté par un tableau 2D de Cell) en une chaîne de caractères.
		/// Chaque ligne du labyrinthe est séparée par un retour à la ligne, et chaque cellule est représentée par sa valeur entière correspondante.
		/// </summary>
		/// <param name="levelMap">Le labyrinthe à formater.</param>
		/// <returns>Une chaîne de caractères représentant le labyrinthe formaté.</returns>
		public static string FormatMap(Cell[,] levelMap)
		{
			var rows = levelMap.GetLength(0); // Nombre de lignes dans le labyrinthe
			var cols = levelMap.GetLength(1); // Nombre de colonnes dans le labyrinthe
			var sb = new StringBuilder();      // StringBuilder pour construire la chaîne

			// Parcourt chaque ligne du labyrinthe
			for (var i = 0; i < rows; i++)
			{
				// Parcourt chaque colonne de la ligne
				for (var j = 0; j < cols; j++)
				{
					// Ajoute la valeur entière de la cellule à la chaîne
					sb.Append((int)levelMap[i, j]);
					if (j < cols - 1)
						sb.Append(',');             // Ajoute une virgule après chaque cellule sauf la dernière de la ligne
				}
				// Nouvelle ligne pour la prochaine ligne du labyrinthe
				sb.AppendLine();
			}

			return sb.ToString().TrimEnd(); // Retourne la chaîne formatée, en supprimant les caractères de nouvelle ligne en trop à la fin
		}

		/// <summary>
		/// Convertit une chaîne de caractères représentant un labyrinthe et la convertit en un tableau 2D de cellules (Cell).
		/// Les lignes sont séparées par des retours à la ligne et les cellules sont séparées par des virgules.
		/// </summary>
		/// <param name="mapString">La chaîne de caractères représentant le labyrinthe.</param>
		/// <returns>Un tableau 2D de cellules représentant le labyrinthe.</returns>
		public static Cell[,] ParseMap(string mapString)
		{
			// Divise la chaîne en lignes (en ignorant les lignes vides)
			var rows = mapString.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

			// Crée un tableau 2D de cellules avec les dimensions correctes
			var levelMap = new Cell[rows.Length, rows[0].Split(',').Length];

			// Parcourt chaque ligne du labyrinthe
			for (var i = 0; i < rows.Length; i++)
			{
				var cols = rows[i].Split(','); // Divise la ligne en cellules

				// Parcourt chaque cellule de la ligne
				for (var j = 0; j < cols.Length; j++)
				{
					// Convertit la valeur entière de la cellule en type Cell (enum)
					if (Enum.TryParse(cols[j], true, out Cell cell))
						levelMap[i, j] = cell;
				}
			}
			return levelMap;
		}

		/// <summary>
		/// Calcule la distance de Manhattan entre deux cellules (CellCoordinates).
		/// </summary>
		/// <param name="a">La première cellule.</param>
		/// <param name="b">La deuxième cellule.</param>
		/// <returns>La distance de Manhattan entre les deux cellules.</returns>
		public static int Heuristic(CellCoordinates a, CellCoordinates b)
		{
			return Math.Abs(a.Row - b.Row) + Math.Abs(a.Col - b.Col); // Somme des différences absolues des lignes et colonnes
		}

		/// <summary>
		/// Obtient les cellules voisines valides (haut, bas, gauche, droite) d'une cellule donnée dans le labyrinthe.
		/// </summary>
		/// <param name="maze">Le labyrinthe représenté par un tableau 2D de cellules.</param>
		/// <param name="cell">La cellule dont on cherche les voisins.</param>
		/// <returns>Une liste de CellCoordinates représentant les voisins valides.</returns>
		public static List<CellCoordinates> GetNeighbors(Cell[,] maze, CellCoordinates cell)
		{
			// Liste des voisins valides de la cellule donnée
			List<CellCoordinates> neighbors = [];

			int[] dx = [-1, 1, 0, 0]; // Déplacements en ligne (-1 : haut, 1 : bas)
			int[] dy = [0, 0, -1, 1]; // Déplacements en colonne (-1 : gauche, 1 : droite)

			for (int i = 0; i < 4; i++)
			{
				int newRow = cell.Row + dx[i];
				int newCol = cell.Col + dy[i];

				// Vérifie si le voisin est dans les limites du labyrinthe et n'est pas un mur
				if (newRow >= 0 && newRow < maze.GetLength(0) && newCol >= 0 && newCol < maze.GetLength(1) && maze[newRow, newCol] != Cell.WALL)
				{
					// Ajoute le voisin valide à la liste
					neighbors.Add(new CellCoordinates(newRow, newCol));
				}
			}
			return neighbors;
		}


		/// <summary>
		/// Vérifie si un labyrinthe est vide, c'est-à-dire s'il ne contient plus aucun objet à collecter (kit de santé, pièces, clés ou torches).
		/// </summary>
		/// <param name="maze">Le labyrinthe représenté par un tableau 2D de cellules.</param>
		/// <returns>True si le labyrinthe est vide, false sinon.</returns>
		public static bool IsMazeEmpty(Cell[,] maze)
		{
			// Parcours toutes les cellules du labyrinthe
			for (int i = 0; i < maze.GetLength(0); i++)  // Parcours des lignes (hauteur)
			{
				for (int j = 0; j < maze.GetLength(1); j++) // Parcours des colonnes (largeur)
				{
					// Vérifie si la cellule contient un objet à collecter (kit de santé, pièce, clé ou torche)
					if (maze[i, j] == Cell.HEALTH_KIT || maze[i, j] == Cell.COIN || maze[i, j] == Cell.KEY || maze[i, j] == Cell.TORCH)
						return false; // Si un objet est trouvé, le labyrinthe n'est pas vide
				}
			}
			return true; // Si aucun objet n'a été trouvé, le labyrinthe est vide
		}

		public static void RestoreEnemy(Cell[,] maze)
		{
			for (int i = 0; i < maze.GetLength(0); i++)
			{
				for (int j = 0; j < maze.GetLength(1); j++)
				{
					switch (maze[i, j])
					{
						case Cell.MARQUIS:
							Enemies.Marquis.SetStartingPosition(new CellCoordinates(i, j), maze);
							break;
						case Cell.WINSTON:
							Enemies.Winston.SetStartingPosition(new CellCoordinates(i, j), maze);
							break;
						case Cell.CAIN:
							Enemies.Cain.SetStartingPosition(new CellCoordinates(i, j), maze);
							break;
						case Cell.VIGGO:
							Enemies.Viggo.SetStartingPosition(new CellCoordinates(i, j), maze);
							break;
					}
				}
			}
		}
	}
}
