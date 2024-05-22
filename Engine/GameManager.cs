using Engine.utils;

namespace Engine
{
	public class GameManager
	{
		public LevelManager LevelManager { get; private set; }
		public GameState GameState { get; private set; } = GameState.PLAYING;
		public GameMode GameMode;

		public Player Player { get; set; }

		public GameManager(GameMode gameMode = GameMode.STORY)
		{
			Player = new();
			GameMode = gameMode;
			LevelManager = new LevelManager(Player, gameMode);
		}

		public void StepPlayer(Direction direction)
		{
			if (GameState == GameState.PLAYING)
			{
				CellCoordinates newPosition = Entity.GetNextPosition(Player.Position, direction);
				if (Entity.IsInBounds(newPosition, LevelManager.LevelMap))
					if (LevelManager.LevelMap[newPosition.X, newPosition.Y] != Cell.Wall)
						Player.Move(direction, LevelManager.LevelMap, this);
			}
		}

		public void StepGhosts() // Had to create a new method to be able to control the player and the ghost speed independently
		{
			if (GameState == GameState.PLAYING)
			{
				Enemies.Cain.Move(LevelManager.LevelMap, Player.CurrentDirection);
			}
		}

		public void Pause()
		{
			GameState = GameState.PAUSED;
		}

		public void Resume()
		{
			GameState = GameState.PLAYING;
		}

		public void NextLevel()
		{

		}

		public void CheckCollisions(Cell cellType)
		{
			switch (cellType)
			{
				case Cell.Coin:
					LevelManager.UpdateScore(10);
					break;
				case Cell.HealthKit:
					// Trigger power-up mode
					break;
				case Cell.Winston:
				case Cell.Cain:
				case Cell.Viggo:
				case Cell.Marquis:
					CollideWithEnemy();
					break;
				case Cell.Key:
					LevelManager.AddKey();
					break;

				default:
					break;
					// Add more cases as needed
			}
		}

		public void CollideWithEnemy()
		{
			LevelManager.Health.ReduceHealth();
			if (LevelManager.Health.IsDead())
			{
				GameState = GameState.GAME_OVER;
				// Optionally save the game data
			}
			else
			{
				Player.UpdatePosition(LevelManager.MazeStartPos, LevelManager.LevelMap);
			}
		}
	}
}
