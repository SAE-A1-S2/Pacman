using System.Diagnostics;
using Engine.utils;

namespace Engine
{
	public static class Enemies
	{
		public static Enemy Winston = new("Winston", new AmbusherBehavior(), Cell.Winston);
		public static Enemy Cain = new("Cain", new ChaserBehavior(), Cell.Cain);
		public static Enemy Viggo = new("Viggo", new WandererBehavior(), Cell.Viggo);
		public static Enemy Marquis = new("Marquis", new WhimsicalBehavior(), Cell.Marquis);
		public static (Enemy, Enemy, Enemy, Enemy) enemies = (Winston, Cain, Viggo, Marquis);

		public static IEnumerable<Enemy> ToEnumerable(this (Enemy, Enemy, Enemy, Enemy) tuple)
		{
			yield return tuple.Item1;
			yield return tuple.Item2;
			yield return tuple.Item3;
			yield return tuple.Item4;
		}

		public static void ForEach(this (Enemy, Enemy, Enemy, Enemy) tuple, Action<Enemy> action)
		{
			action(tuple.Item1);
			action(tuple.Item2);
			action(tuple.Item3);
			action(tuple.Item4);
		}

		public static List<CellCoordinates> FindEmptyPositions(Cell[,] maze, int count) // Test, will be changed
		{
			var emptyPositions = new List<CellCoordinates>();
			var random = new Random();

			while (emptyPositions.Count < count)
			{
				var coord = new CellCoordinates(random.Next(maze.GetLength(0)), random.Next(maze.GetLength(1)));
				var cell = maze[coord.row, coord.col];

				if (cell == Cell.Empty && !emptyPositions.Contains(new CellCoordinates(coord.row, coord.col)))
					emptyPositions.Add(new CellCoordinates(coord.row, coord.col));
			}
			return emptyPositions;
		}
	}
	public sealed class Enemy : Entity
	{
		public EnemyState State { get; private set; } = EnemyState.FRIGHTENED;
		public IEnemyBehavior EnemyBehavior { get; private set; }
		private readonly Random m_Random = new();
		private Cell m_PreviousKind = Cell.Coin;

		public Enemy(string name, IEnemyBehavior enemyBehavior, Cell kind)
		{
			Kind = kind;
			Name = name;
			EnemyBehavior = enemyBehavior;
		}

		public void SetStartingPosition(CellCoordinates start, Cell[,] maze)
		{
			StartPosition = start;
			Position = start;
			maze[Position.row, Position.col] = Kind;
		}

		public void UpdateStatus(CellCoordinates newCell, Cell[,] maze, Direction direction = Direction.STOP)
		{
			CurrentDirection = direction;
			if (!IsDynamicEntity(maze[newCell.row, newCell.col]))
				maze[Position.row, Position.col] = m_PreviousKind;
			m_PreviousKind = GetStaticEntity(maze[newCell.row, newCell.col]);
			Position = newCell;
			maze[Position.row, Position.col] = Kind;
		}

		public void Move(Cell[,] maze, Direction direction)
		{
			if (State == EnemyState.FRIGHTENED)
			{
				MoveRandom(maze);
			}
			else
			{
				var nextPosition = EnemyBehavior.NextPositon(maze, Position, direction);
				var dir = GetDirection(Position, nextPosition);
				UpdateStatus(nextPosition, maze, dir);
			}
		}

		public void MoveRandom(Cell[,] maze)
		{
			var currentPos = Position;
			var dir = CurrentDirection;
			var newPos = GetNextPosition(currentPos, dir);

			bool pathBlocked = !IsInBounds(newPos, maze) || maze[newPos.row, newPos.col] == Cell.Wall || IsOccupied(newPos, maze);

			int randomDecision = m_Random.Next(1, 4);
			// If the path is blocked or randomness forces a change, select a new direction
			if (pathBlocked || randomDecision != 1)
			{
				var alternativeDirections = GetAlternativeDirections(dir);
				bool foundValidDirection = false;

				// Try all alternative directions excluding the opposite direction first
				foreach (var altDir in alternativeDirections)
				{
					newPos = GetNextPosition(currentPos, altDir);
					if (IsInBounds(newPos, maze) && maze[newPos.row, newPos.col] != Cell.Wall)
					{
						dir = altDir;
						foundValidDirection = true;
						break;
					}
				}

				// If no valid direction found, try the opposite direction
				if (!foundValidDirection)
				{
					var oppositeDir = GetOppositeDirection(CurrentDirection);
					newPos = GetNextPosition(currentPos, oppositeDir);
					if (IsInBounds(newPos, maze) && maze[newPos.row, newPos.col] != Cell.Wall)
						dir = oppositeDir;
				}
			}

			// Move to the new position if valid
			if (IsInBounds(newPos, maze) && maze[newPos.row, newPos.col] != Cell.Wall)
				UpdateStatus(newPos, maze, dir);
		}

		private bool IsDynamicEntity(Cell cell)
		{
			return
				   cell == Cell.John ||
				   (cell == Cell.Winston && cell != Kind) ||
				   (cell == Cell.Cain && cell != Kind) ||
				   (cell == Cell.Viggo && cell != Kind) ||
				   (cell == Cell.Marquis && cell != Kind);
		}

		private Cell GetStaticEntity(Cell cell)
		{
			return IsDynamicEntity(cell) ? Cell.Empty : cell;
		}

		public void ChangeState(EnemyState newState)
		{
			State = newState;
		}

		public void Freeze()
		{
			ChangeState(EnemyState.SCATTER);
			// Additional logic when frozen, e.g., disable collision effects
		}

		public void Unfreeze()
		{
			ChangeState(EnemyState.CHASE);
			// Resume normal movement and interactions
		}

		public static Direction[] GetAlternativeDirections(Direction currentDirection)
		{
			var directions = new[] { Direction.UP, Direction.DOWN, Direction.LEFT, Direction.RIGHT };
			var random = new Random();
			return [.. directions.Where(d => d != GetOppositeDirection(currentDirection)).OrderBy(d => random.Next())];
		}
		public static Direction GetOppositeDirection(Direction direction)
		{
			return direction switch
			{
				Direction.UP => Direction.DOWN,
				Direction.DOWN => Direction.UP,
				Direction.LEFT => Direction.RIGHT,
				Direction.RIGHT => Direction.LEFT,
				_ => direction,
			};
		}

		public static bool IsOccupied(CellCoordinates position, Cell[,] maze)
		{
			return maze[position.row, position.col] != Cell.Empty || maze[position.row, position.col] != Cell.Start || maze[position.row, position.col] != Cell.End || maze[position.row, position.col] != Cell.John || maze[position.row, position.col] != Cell.HealthKit || maze[position.row, position.col] != Cell.Coin || maze[position.row, position.col] != Cell.Torch || maze[position.row, position.col] != Cell.Key;
		}
	}
}

