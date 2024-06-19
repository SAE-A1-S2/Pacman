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

		public CellCoordinates NextPosition { get; protected set; }

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

		public void UpdatePosition(CellCoordinates newCell, Cell[,] maze, Cell previousCell = Cell.EMPTY)
		{
			maze[Position.Row, Position.Col] = previousCell;
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
	}
}
