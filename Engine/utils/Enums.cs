/*
GROUPE D-06
SAE 2.01
2023-2024

Résumé:
Ce fichier contient plusieurs énumérations utilisées dans le jeu pour définir les différents états et modes de jeu, 
les directions de déplacement et les états des ennemis.
*/
namespace Engine.utils
{
	public enum GameState
	{
		GAME_OVER,
		PLAYING,
		PAUSED,
	}

	public enum Direction
	{
		UP,
		DOWN,
		LEFT,
		RIGHT,
		STOP,
	}

	public enum GameMode
	{
		STORY,
		INFINITE,
	}

	public enum EnemyState
	{
		CHASE,
		SCATTER,
		FRIGHTENED
	}
}