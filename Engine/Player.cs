using Engine.utils;

namespace Engine
{
	public sealed class Player : Entity
	{
		private readonly BonusPair<Bonus> m_Bonuses = new(null, null);

		public Player(string name = "John")
		{
			Name = name;
			Kind = Cell.John;
		}

		public void SetPlayerName(string newName)
		{
			Name = newName;
		}

		public void SetPlayerPosition(CellCoordinates newPostion)
		{
			Position = newPostion;
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
				if (maze[newPosition.row, newPosition.col] != Cell.Wall)
				{
					CheckCollisions(maze[newPosition.row, newPosition.col], gameManager);
					UpdatePosition(newPosition, maze);
				}
			}
		}

		public void CheckCollisions(Cell cellType, GameManager gameManager)
		{
			switch (cellType)
			{
				case Cell.Coin:
					gameManager.LevelManager.UpdateScore(10);
					break;
				case Cell.HealthKit:
					m_Bonuses.Add(new HealthBonus());
					break;
				case Cell.Key:
					gameManager.LevelManager.AddKey();
					break;

				default:
					break;
					// Add more cases as needed
			}
		}
	}
}
