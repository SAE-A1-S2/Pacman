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
	}
	public sealed class Enemy : Entity
	{
		public EnemyState State { get; private set; }
		public bool IsFrozen => State == EnemyState.FRIGHTENED; // To be changed
		public IEnemyBehavior EnemyBehavior { get; private set; }
		public Cell[,]? Maze { get; private set; }

		private int m_MoveCounter;
		private static int s_RandomMoveInterval = 5;
		private Random m_Random = new();
		private Cell m_PreviousKind = Cell.Empty;

		public Enemy(string name, IEnemyBehavior enemyBehavior, Cell kind)
		{
			Kind = kind;
			Name = name;
			EnemyBehavior = enemyBehavior;
		}

		public void SetStartingPosition(CellCoordinates start) { Position = start; }
		public void SetMaze(Cell[,] maze) { Maze = maze; }

		public void ChangeState(EnemyState newState)
		{
			State = newState;
		}

		public void MoveRandom()
		{
			if (!Position.HasValue) return;
			if (Maze == null) return;
			// Always decrease m_MoveCounter
			m_MoveCounter--;

			// Check if current direction is still valid or if it's time to get a new direction
			if (m_MoveCounter <= 0 || !MazeGenerator.IsInBounds(GetNextPosition(Position.Value, CurrentDirection), Maze))
			{
				CurrentDirection = GetValidRandomDirection();
				m_MoveCounter = s_RandomMoveInterval;
			}

			// Move to the new position if valid
			CellCoordinates newPosition = GetNextPosition(Position.Value, CurrentDirection);
			if (MazeGenerator.IsInBounds(newPosition, Maze))
				UpdatePosition(newPosition, Maze);
			else if (m_MoveCounter <= 0) // In case the new position is not valid and m_MoveCounter is depleted, force direction update
			{
				CurrentDirection = GetValidRandomDirection();
				m_MoveCounter = s_RandomMoveInterval; // Reset move counter after a forced direction change
			}
		}

		private Direction GetValidRandomDirection()
		{
			if (!Position.HasValue) throw new Exception("Position is null");
			if (Maze == null) throw new Exception("Maze is null");
			Direction newDirection;
			int attempts = 0;
			do
			{
				newDirection = GetRandomDirection();
				attempts++;
			} while (!MazeGenerator.IsInBounds(GetNextPosition(Position.Value, newDirection), Maze) && attempts < 4);

			return newDirection;
		}

		private Direction GetRandomDirection()
		{
			Array values = Enum.GetValues(typeof(Direction));
			return (Direction)values.GetValue(m_Random.Next(values.Length))!; // To be checked if null
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

