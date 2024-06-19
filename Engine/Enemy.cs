using System.Diagnostics;
using Engine.utils;

namespace Engine
{
	public static class Enemies
	{
		public static Enemy Winston = new("Winston", new AmbusherBehavior(), Cell.WINSTON);
		public static Enemy Cain = new("Cain", new ChaserBehavior(), Cell.CAIN);
		public static Enemy Viggo = new("Viggo", new WandererBehavior(), Cell.VIGGO);
		public static Enemy Marquis = new("Marquis", new WhimsicalBehavior(), Cell.MARQUIS);
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
				var cell = maze[coord.Row, coord.Col];

				if (cell == Cell.EMPTY && !emptyPositions.Contains(new CellCoordinates(coord.Row, coord.Col)))
					emptyPositions.Add(new CellCoordinates(coord.Row, coord.Col));
			}
			return emptyPositions;
		}
	}
	public sealed class Enemy : Entity
	{
		public EnemyState State { get; private set; } = EnemyState.FRIGHTENED;
		public IEnemyBehavior EnemyBehavior { get; private set; }
		private readonly Random m_Random = new();
		private Cell m_PreviousKind = Cell.COIN;

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
			maze[Position.Row, Position.Col] = Kind;
		}

		public void Move(Cell[,] maze, Direction direction)
		{
			if (State == EnemyState.FRIGHTENED)
			{
				MoveRandom(maze);
			}
			else
			{
				// A factoriser
				NextPosition = EnemyBehavior.NextPosition(maze, Position, direction);
				var dir = GetDirection(Position, NextPosition);
				CurrentDirection = dir;
				Cell nextCell = GetStaticEntity(maze[NextPosition.Row, NextPosition.Col]);
				UpdatePosition(NextPosition, maze, m_PreviousKind);
				m_PreviousKind = nextCell;
			}
		}

		public void MoveRandom(Cell[,] maze)
		{
			var currentPos = Position;
			var dir = CurrentDirection;
			NextPosition = GetNextPosition(currentPos, dir);

			bool pathBlocked = !IsInBounds(NextPosition, maze) || maze[NextPosition.Row, NextPosition.Col] == Cell.WALL || IsOccupied(NextPosition, maze);

			int randomDecision = m_Random.Next(1, 4);
			// If the path is blocked or randomness forces a change, select a new direction
			if (pathBlocked || randomDecision != 1)
			{
				var alternativeDirections = GetAlternativeDirections(dir);
				bool foundValidDirection = false;

				// Try all alternative directions excluding the opposite direction first
				foreach (var altDir in alternativeDirections)
				{
					NextPosition = GetNextPosition(currentPos, altDir);
					if (IsInBounds(NextPosition, maze) && maze[NextPosition.Row, NextPosition.Col] != Cell.WALL)
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
					NextPosition = GetNextPosition(currentPos, oppositeDir);
					if (IsInBounds(NextPosition, maze) && maze[NextPosition.Row, NextPosition.Col] != Cell.WALL)
						dir = oppositeDir;
				}
			}

			// Move to the new position if valid
			if (IsInBounds(NextPosition, maze) && maze[NextPosition.Row, NextPosition.Col] != Cell.WALL)
			{
				CurrentDirection = dir;
				Cell nextCell = GetStaticEntity(maze[NextPosition.Row, NextPosition.Col]);
				UpdatePosition(NextPosition, maze, m_PreviousKind);
				m_PreviousKind = nextCell;
			}
		}

		private bool IsDynamicEntity(Cell cell)
		{
			return
				   cell == Cell.JOHN ||
				   (cell == Cell.WINSTON && cell != Kind) ||
				   (cell == Cell.CAIN && cell != Kind) ||
				   (cell == Cell.VIGGO && cell != Kind) ||
				   (cell == Cell.MARQUIS && cell != Kind);
		}

		private Cell GetStaticEntity(Cell cell)
		{
			return IsDynamicEntity(cell) ? Cell.EMPTY : cell;
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
			return maze[position.Row, position.Col] != Cell.EMPTY || maze[position.Row, position.Col] != Cell.START || maze[position.Row, position.Col] != Cell.END || maze[position.Row, position.Col] != Cell.JOHN || maze[position.Row, position.Col] != Cell.HEALTH_KIT || maze[position.Row, position.Col] != Cell.COIN || maze[position.Row, position.Col] != Cell.TORCH || maze[position.Row, position.Col] != Cell.KEY;
		}
	}
}

