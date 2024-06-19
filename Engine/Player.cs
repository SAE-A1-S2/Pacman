using Engine.utils;

namespace Engine
{
	public sealed class Player : Entity
	{
		public BonusPair<Bonus> Bonuses;

		public Player(string name = "John")
		{
			Bonuses = new BonusPair<Bonus>(null, null);
			Name = name;
			Kind = Cell.JOHN;
		}

		public Player(string name, int BonusValue)
		{
			Name = name;
			Kind = Cell.JOHN;
			Bonuses = new BonusPair<Bonus>(BonusValue);
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
			maze[PlayerPosition.Row, PlayerPosition.Col] = Kind;
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
				if (maze[newPosition.Row, newPosition.Col] != Cell.WALL)
				{
					CurrentDirection = direction;
					CheckCollisions(maze[newPosition.Row, newPosition.Col], gameManager);
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
				case Cell.COIN:
					gameManager.LevelManager.UpdateScore(10);
					break;
				case Cell.HEALTH_KIT:
					Bonuses.Add(new HealthBonus());
					break;
				case Cell.KEY:
					gameManager.LevelManager.AddKey();
					break;
				case Cell.TORCH:
					Bonuses.Add(new TorchBonus());
					break;

				default:
					break;
					// Add more cases as needed
			}
		}
	}
}
