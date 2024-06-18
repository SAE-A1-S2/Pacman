using System.Diagnostics;
using Engine.utils;

namespace Engine
{
	public class Entity
	{
		public CellCoordinates Position { get; protected set; }
		public string Name { get; protected set; } = "";
		public Cell Kind { get; protected set; }
		public Direction CurrentDirection { get; protected set; }
		public CellCoordinates StartPosition { get; set; }

		public static CellCoordinates GetNextPosition(CellCoordinates currentPosition, Direction direction)
		{
			return direction switch
			{
				Direction.UP => new CellCoordinates(currentPosition.Row - 1, currentPosition.Col),
				Direction.DOWN => new CellCoordinates(currentPosition.Row + 1, currentPosition.Col),
				Direction.LEFT => new CellCoordinates(currentPosition.Row, currentPosition.Col - 1),
				Direction.RIGHT => new CellCoordinates(currentPosition.Row, currentPosition.Col + 1),
				_ => currentPosition,
			};
		}

		public void UpdatePosition(CellCoordinates newCell, Cell[,] maze)
		{
			maze[Position.Row, Position.Col] = Cell.EMPTY;
			maze[newCell.Row, newCell.Col] = Kind;
			Position = newCell;
		}

		public static bool IsInBounds(CellCoordinates cell, Cell[,] maze)
		{
			return cell.Row >= 0 && cell.Row < maze.GetLength(0) &&
				   cell.Col >= 0 && cell.Col < maze.GetLength(1);
		}

		public static Direction GetDirection(CellCoordinates src, CellCoordinates dst)
		{
			if (src.Col < dst.Col)
				return Direction.RIGHT;
			if (src.Col > dst.Col)
				return Direction.LEFT;
			if (src.Row < dst.Row)
				return Direction.DOWN;
			if (src.Row > dst.Row)
				return Direction.UP;
			return Direction.STOP;
		}

		public static void CollideWithEnemy(GameManager gameManager) // this will be moved to gameManager, just testing for now
		{
			gameManager.LevelManager.Health.ReduceHealth();
			if (gameManager.LevelManager.Health.IsDead())
			{
				gameManager.GameOver();
				// Optionally save the game data
			}
			else
			{
				UpdatePlayerPosition(gameManager.Player.StartPosition, gameManager.LevelManager.LevelMap, gameManager);
			}
		}

		public static void UpdatePlayerPosition(CellCoordinates newCell, Cell[,] maze, GameManager gameManager)
		{
			var playerPos = gameManager.LevelManager.Player.Position; // use GetNextPosition instead, 
			maze[playerPos.Row, playerPos.Col] = Cell.EMPTY;
			maze[newCell.Row, newCell.Col] = gameManager.LevelManager.Player.Kind;
			gameManager.LevelManager.Player.SetPlayerPosition(newCell);
			Enemies.enemies.ForEach(enemy =>
			{
				maze[enemy.Position.Row, enemy.Position.Col] = Cell.EMPTY;
				enemy.Position = enemy.StartPosition;
				maze[enemy.Position.Row, enemy.Position.Col] = enemy.Kind;
			}
			);
		}

	}
}
