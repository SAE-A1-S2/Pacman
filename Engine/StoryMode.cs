/*
GROUPE D-06
SAE 2.01
2023-2024

Résumé:
Ce fichier contient le code de la classe StoryMode qui gère la logique du mode histoire du jeu. 
Il inclut des fonctionnalités pour charger le labyrinthe du niveau en cours et déterminer la position de départ.
*/
using DB;
namespace Engine
{
	public class StoryMode
	{
		private readonly int Level; // Niveau dans le jeu

		public Cell[,] Maze { get; private set; } // Labyrinthe du niveau
		public CellCoordinates StartPos { get; private set; } // Position de depart
		public CellCoordinates EndPos { get; private set; } // Position d'arrêt du niveau

		/// <summary>
		/// Constructeur de la classe StoryMode.
		/// Initialise le niveau actuel, charge le labyrinthe et trouve la position.
		/// </summary>
		public StoryMode(string PlayerUid)
		{
			string maze, start, end;
			int LastLevelPlayed = Base.GetLastStoryLevelPlayed(PlayerUid);
			Level = LastLevelPlayed == -1 ? 1 : LastLevelPlayed + 1;
			(maze, start, end) = Base.LoadStoryLevel(Level);
			StartPos = CellCoordinates.Parse(start);
			EndPos = CellCoordinates.Parse(end);
			Maze = Static.ParseMap(maze);
		}

	}
}