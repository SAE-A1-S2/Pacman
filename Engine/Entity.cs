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
				Direction.UP => new CellCoordinates(currentPosition.row - 1, currentPosition.col),
				Direction.DOWN => new CellCoordinates(currentPosition.row + 1, currentPosition.col),
				Direction.LEFT => new CellCoordinates(currentPosition.row, currentPosition.col - 1),
				Direction.RIGHT => new CellCoordinates(currentPosition.row, currentPosition.col + 1),
				_ => currentPosition,
			};
		}

		public void UpdatePosition(CellCoordinates newCell, Cell[,] maze)
		{
			maze[Position.row, Position.col] = Cell.Empty;
			maze[newCell.row, newCell.col] = Kind;
			Position = newCell;
		}

		public static bool IsInBounds(CellCoordinates cell, Cell[,] maze)
		{
			return cell.row >= 0 && cell.row < maze.GetLength(0) &&
				   cell.col >= 0 && cell.col < maze.GetLength(1);
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
			var playerPos = gameManager.LevelManager.Player.Position;
			maze[playerPos.row, playerPos.col] = Cell.Empty;
			maze[newCell.row, newCell.col] = gameManager.LevelManager.Player.Kind;
			gameManager.LevelManager.Player.SetPlayerPosition(newCell);
			Enemies.enemies.ToEnumerable().ToList().ForEach(enemy => enemy.Position = enemy.StartPosition);
		}

	}
}
