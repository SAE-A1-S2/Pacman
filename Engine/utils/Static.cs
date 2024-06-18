using System.Text;

namespace Engine
{
	public static class Static
	{
		public static string FormatMap(Cell[,] levelMap)
		{
			var rows = levelMap.GetLength(0);
			var cols = levelMap.GetLength(1);
			var sb = new StringBuilder();

			for (var i = 0; i < rows; i++)
			{
				for (var j = 0; j < cols; j++)
				{
					sb.Append((int)levelMap[i, j]); // Convert Cell enum value to int
					if (j < cols - 1)
					{
						sb.Append(',');  // Add comma separator between columns
					}
				}
				sb.AppendLine(";");
			}

			return sb.ToString();
		}

		public static Cell[,] ParseMap(string mapString)
		{
			var rows = mapString.Split(';');
			var levelMap = new Cell[rows.Length, rows[0].Split(',').Length]; // Determine the size based on string

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