using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class HistoryMode
    {
        private string currentFilePath = "";
        private int currentLevel = 1;

        public Cell[,] maze { get; private set; }
        public CellCoordinates startPos { get; private set; }

        public HistoryMode()
        {
            FileManager fileManager = new();
            currentLevel = fileManager.GetLastPlayedLevel() == -1 ? 1 : fileManager.GetLastPlayedLevel() + 1;
            currentFilePath = "Level" + currentLevel + ".txt";
            maze = fileManager.LoadLevel(currentFilePath);
            findStartPos();
        }

        public void findStartPos()
        {
            for (int x = 0; x < maze.GetLength(0); x++)
                for (int y = 0; y < maze.GetLength(1); y++)
                    if (maze[x, y] == Cell.Start)
                    {
                        Debug.WriteLine("Found start position at " + x + ", " + y);
                        startPos = new CellCoordinates(x, y);
                    }
        }

    }
}
