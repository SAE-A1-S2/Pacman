using System.Text;

namespace Engine.utils
{
	public static class Static
	{
		// Méthode pour formater le labyrinthe en une chaîne de caractères
		public static string FormatMap(Cell[,] levelMap)
		{
			var rows = levelMap.GetLength(0);
			var cols = levelMap.GetLength(1);
			var sb = new StringBuilder();

			for (var i = 0; i < rows; i++)
			{
				for (var j = 0; j < cols; j++)
				{
					sb.Append((int)levelMap[i, j]);
					if (j < cols - 1)
					{
						sb.Append(',');
					}
				}
				sb.AppendLine();
			}

			return sb.ToString().TrimEnd();
		}

		// Méthode pour convertir une chaîne de caractères en labyrinthe (tableau de cellules)
		public static Cell[,] ParseMap(string mapString)
		{
			var rows = mapString.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries); // Split on newlines, remove empty lines
			var levelMap = new Cell[rows.Length, rows[0].Split(',').Length];

			for (var i = 0; i < rows.Length; i++)
			{
				var cols = rows[i].Split(',');
				for (var j = 0; j < cols.Length; j++)
				{
					if (Enum.TryParse(cols[j], true, out Cell cell))
					{
						levelMap[i, j] = cell;
					}
				}
			}

			return levelMap;
		}

		// Fonction heuristique : estime la distance entre deux cellules (Manhattan distance)
		public static int Heuristic(CellCoordinates a, CellCoordinates b)
		{
			return Math.Abs(a.Row - b.Row) + Math.Abs(a.Col - b.Col);
		}

		// Obtient les voisins d'une cellule dans le labyrinthe, en évitant les murs
		public static List<CellCoordinates> GetNeighbors(Cell[,] maze, CellCoordinates cell)
		{
			List<CellCoordinates> neighbors = [];

			// Directions possibles : haut, bas, gauche, droite
			int[] dx = [-1, 1, 0, 0];
			int[] dy = [0, 0, -1, 1];

			for (int i = 0; i < 4; i++)
			{
				int newRow = cell.Row + dx[i];
				int newCol = cell.Col + dy[i];

				// Vérifie si la nouvelle position est dans les limites du labyrinthe et n'est pas un mur
				if (newRow >= 0 && newRow < maze.GetLength(0) && newCol >= 0 && newCol < maze.GetLength(1) && maze[newRow, newCol] != Cell.WALL)
				{
					neighbors.Add(new CellCoordinates(newRow, newCol));
				}
			}

			return neighbors;
		}
	}
}