/*
- ABASS Hammed
- AURIGNAC Arthur
- DOHER Alexis
- GODET Adrien
- MAS Cédric
- NAHARRO Guerby

GROUPE D-06
SAE 2.01
2023-2024

Résumé:
Ce fichier contient le code de la classe StoryMode qui gère la logique du mode histoire du jeu. 
Il inclut des fonctionnalités pour charger le labyrinthe du niveau en cours et déterminer la position de départ.
*/

namespace Engine
{
	public class StoryMode
	{
		private readonly string FilePath; // Chemin du fichier du niveau
		private readonly int Level; // Niveau dans le jeu

		public Cell[,] Maze { get; private set; } // Labyrinthe du niveau
		public CellCoordinates StartPos { get; private set; } // Position de depart
		public CellCoordinates EndPos { get; private set; } // Position d'arrêt du niveau

		/// <summary>
		/// Constructeur de la classe StoryMode.
		/// Initialise le niveau actuel, charge le labyrinthe et trouve la position de départ.
		/// </summary>
		public StoryMode()
		{
			FileManager fileManager = new();
			Level = fileManager.GetLastPlayedLevel() == -1 ? 1 : fileManager.GetLastPlayedLevel() + 1;
			FilePath = "Level" + Level + ".txt";
			Maze = FileManager.LoadLevel(FilePath);
			FindStartPos();
		}

		/// <summary>
		/// Méthode FindStartPos qui trouve la position de départ du joueur dans le labyrinthe.
		/// </summary>
		private void FindStartPos()
		{
			for (int x = 0; x < Maze.GetLength(0); x++)
				for (int y = 0; y < Maze.GetLength(1); y++)
				{
					if (Maze[x, y] == Cell.Start)
						StartPos = new CellCoordinates(x, y);
					if (Maze[x, y] == Cell.End)
						EndPos = new CellCoordinates(x, y);
				}

		}

	}
}