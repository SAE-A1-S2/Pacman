/*
GROUPE D-06
SAE 2.01
2023-2024

Résumé :
Ce fichier contient le code de la classe StoryMode qui gère la logique du mode histoire du jeu.
Il permet de charger le labyrinthe du niveau en cours et de déterminer la position de départ du joueur.
*/

using DB;

namespace Engine.utils
{
	public class StoryMode // Classe représentant le mode histoire du jeu
	{
		public readonly int Level; // Numéro du niveau actuel (en lecture seule)

		// Propriétés pour accéder aux données du niveau
		public Cell[,] Maze { get; private set; }         // Labyrinthe du niveau (tableau 2D de cellules)
		public CellCoordinates StartPos { get; private set; } // Position de départ du joueur
		public CellCoordinates EndPos { get; private set; }    // Position d'arrivée du niveau

		/// <summary>
		/// Constructeur de la classe StoryMode.
		/// Initialise le niveau actuel, charge le labyrinthe depuis la base de données et détermine les positions de départ et d'arrivée.
		/// </summary>
		/// <param name="PlayerUid">L'UID du joueur pour récupérer le dernier niveau joué.</param>
		public StoryMode(string PlayerUid)
		{
			string maze, start, end; // Variables temporaires pour stocker les données du niveau

			// Récupère le dernier niveau joué par le joueur depuis la base de données
			int LastLevelPlayed = Base.GetLastStoryLevelPlayed(PlayerUid);

			// Si aucun niveau n'a été joué, commence au niveau 1, sinon passe au niveau suivant
			Level = LastLevelPlayed == -1 ? 1 : LastLevelPlayed + 1;

			// Charge les données du niveau depuis la base de données
			(maze, start, end) = Base.LoadStoryLevel(Level);

			// Analyse les chaînes de caractères pour obtenir les coordonnées de départ et d'arrivée
			StartPos = CellCoordinates.Parse(start);
			EndPos = CellCoordinates.Parse(end);

			// Convertit la chaîne de caractères représentant le labyrinthe en un tableau de cellules
			Maze = Static.ParseMap(maze);
		}
	}
}
