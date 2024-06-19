using System.Text;

namespace Engine
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
	}
}