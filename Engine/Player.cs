using Engine.utils;

namespace Engine
{
	public sealed class Player : Entity
	{
		private BonusPair<Bonus> m_Bonuses = new(null, null);

		public Player(string name = "John")
		{
			Name = name;
			Kind = Cell.John;
		}

		public void SetPlayerName(string newName)
		{
			Name = newName;
		}

		public void PlacePlayer(Cell[,] maze)
		{
			for (int x = 0; x < maze.GetLength(0); x++)
				for (int y = 0; y < maze.GetLength(1); y++)
					if (maze[x, y] == Cell.Start)
					{
						maze[x, y] = Cell.John;
						Position = new CellCoordinates(x, y);
					}
		}

		public void Move(Direction direction, Cell[,] maze, GameManager gameManager) // will be optimized
		{
			CellCoordinates newPosition = GetNextPosition(Position, direction);

			// Check if the new position is within the bounds of the maze
			if (IsInBounds(newPosition, maze))
			{
				gameManager.CheckCollisions(maze[newPosition.row, newPosition.col]);

				if (maze[newPosition.row, newPosition.col] != Cell.Wall)
				{
					UpdatePosition(newPosition, maze);
					HandleCellInteraction(maze[Position.row, Position.col]);
				}
			}
		}


		private void HandleCellInteraction(Cell cellType)
		{
			switch (cellType)
			{
				case Cell.Coin:
					// Increment score
					break;
				case Cell.HealthKit:
					m_Bonuses.Add(new HealthBonus());
					break;
				case Cell.Torch:
					m_Bonuses.Add(new TorchBonus(10 * 1000));
					break;
				// If enemy
				case Cell.Winston:
				case Cell.Cain:
				case Cell.Viggo:
				case Cell.Marquis:
					// Handle collision with ghost
					break;
				default:
					break;
					// Add more cases as needed
			}
		}
	}
}
