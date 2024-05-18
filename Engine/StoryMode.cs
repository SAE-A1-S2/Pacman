using System.Diagnostics;
namespace Engine
{
	public class StoryMode
	{
		private string m_CurrentFilePath = "";
		private int m_CurrentLevel = 1;

		public Cell[,] Maze { get; private set; }
		public CellCoordinates StartPos { get; private set; }

		public StoryMode()
		{
			FileManager fileManager = new();
			m_CurrentLevel = fileManager.GetLastPlayedLevel() == -1 ? 1 : fileManager.GetLastPlayedLevel() + 1;
			m_CurrentFilePath = "Level" + m_CurrentLevel + ".txt";
			Maze = fileManager.LoadLevel(m_CurrentFilePath);
			findStartPos();
		}

		public void findStartPos()
		{
			for (int x = 0; x < Maze.GetLength(0); x++)
				for (int y = 0; y < Maze.GetLength(1); y++)
					if (Maze[x, y] == Cell.Start)
					{
						Debug.WriteLine("Found start position at " + x + ", " + y);
						StartPos = new CellCoordinates(x, y);
					}
		}

	}
}
