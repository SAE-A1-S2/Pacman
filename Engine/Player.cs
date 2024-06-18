using Engine.utils;

namespace Engine
{
	public sealed class Player : Entity
	{
		public BonusPair<Bonus> m_Bonuses;

		public Player(string name = "John")
		{
			m_Bonuses = new BonusPair<Bonus>(null, null);
			Name = name;
			Kind = Cell.John;
		}

		public Player(string name, int BonusValue)
		{
			Name = name;
			Kind = Cell.John;
			m_Bonuses = new BonusPair<Bonus>(BonusValue);
		}

		public void SetPlayerName(string newName)
		{
			Name = newName;
		}

		public void SetPlayerPosition(CellCoordinates newPostion)
		{
			Position = newPostion;
		}

		public void PlacePlayer(Cell[,] maze, CellCoordinates startPosition, CellCoordinates PlayerPosition)
		{
			maze[startPosition.row, startPosition.col] = Kind;
			Position = PlayerPosition;
			StartPosition = startPosition;
			CurrentDirection = Direction.STOP;
		}

		public void Move(Direction direction, Cell[,] maze, GameManager gameManager)
		{
			CellCoordinates newPosition = GetNextPosition(Position, direction);

			// Check if the new position is within the bounds of the maze
			if (IsInBounds(newPosition, maze))
			{
				if (maze[newPosition.row, newPosition.col] != Cell.Wall)
				{
					CurrentDirection = direction;
					CheckCollisions(maze[newPosition.row, newPosition.col], gameManager);
					UpdatePosition(newPosition, maze);
				}
				else
					CurrentDirection = Direction.STOP;
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
				case Cell.Torch:
					m_Bonuses.Add(new TorchBonus());
					break;

				default:
					break;
					// Add more cases as needed
			}
		}
	}
}
