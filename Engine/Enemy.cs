using Engine.utils;

namespace Engine
{
	public static class Enemies
	{
		public static Enemy Winston = new("Winston", new AmbusherBehavior(), Cell.Winston);
		public static Enemy Cain = new("Cain", new ChaserBehavior(), Cell.Cain);
		public static Enemy Viggo = new("Viggo", new WandererBehavior(), Cell.Viggo);
		public static Enemy Marquis = new("Marquis", new WhimsicalBehavior(), Cell.Marquis);
		public static Tuple<Enemy, Enemy, Enemy, Enemy> enemies =
			new Tuple<Enemy, Enemy, Enemy, Enemy>(Winston, Cain, Viggo, Marquis);

		public static List<CellCoordinates> FindEmptyPositions(Cell[,] maze, int count) // Test, will be changed
		{
			var emptyPositions = new List<CellCoordinates>();
			var random = new Random();

			while (emptyPositions.Count < count)
			{
				var coord = new CellCoordinates(random.Next(maze.GetLength(0)), random.Next(maze.GetLength(1)));
				var cell = maze[coord.X, coord.Y];

				if (cell == Cell.Empty && !emptyPositions.Contains(new CellCoordinates(coord.X, coord.Y)))
					emptyPositions.Add(new CellCoordinates(coord.X, coord.Y));
			}
			return emptyPositions;
		}
	}
	public sealed class Enemy : Entity
	{
		public EnemyState State { get; private set; }
		public bool IsFrozen => State == EnemyState.FRIGHTENED; // To be changed
		public IEnemyBehavior EnemyBehavior { get; private set; }
		private Random m_Random = new();
		private Cell m_PreviousKind = Cell.Empty;

		public Enemy(string name, IEnemyBehavior enemyBehavior, Cell kind)
		{
			Kind = kind;
			Name = name;
			EnemyBehavior = enemyBehavior;
		}

		public void SetStartingPosition(CellCoordinates start, Cell[,] maze)
		{
			Position = start;
			maze[Position.X, Position.Y] = Kind;
		}

		public new void UpdatePosition(CellCoordinates newCell, Cell[,] maze)
		{
			maze[Position.X, Position.Y] = m_PreviousKind;
			Position = newCell;
			m_PreviousKind = maze[Position.X, Position.Y];
			maze[Position.X, Position.Y] = Kind;
		}

		public void Move(Cell[,] maze) // Everything we need to move an enemy, nothing else
		{
			CellCoordinates newPosition = EnemyBehavior.NextPositon(maze, Position);
			UpdatePosition(newPosition, maze);
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
	}
}

