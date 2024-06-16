using Engine.utils;

namespace Engine
{
	public class GameManager
	{
		public LevelManager LevelManager { get; private set; }
		public GameState GameState { get; private set; } = GameState.PLAYING;
		public GameMode GameMode;

		public Player Player { get; set; }

		public GameManager(GameMode gameMode, string playerName)
		{
			Player = new Player(playerName);
			GameMode = gameMode;
			LevelManager = new LevelManager(Player, gameMode);
		}

		public GameManager(int sessionId)
		{
			throw new NotImplementedException();
		}

		public void StepPlayer(Direction direction)
		{
			if (GameState == GameState.PLAYING)
				Player.Move(direction, LevelManager.LevelMap, this);
		}

		public void StepGhosts() // Had to create a new method to be able to control the player and the ghost speed independently
		{
			if (GameState == GameState.PLAYING)
			{
				Enemies.Cain.Move(LevelManager.LevelMap, Player.CurrentDirection);
			}
		}

		public bool CheckGhostCollisions()
		{
			List<CellCoordinates> enemiesNextPos = [];
			var playerNextPos = Entity.GetNextPosition(Player.Position, Player.CurrentDirection);
			Enemies.enemies.ForEach(enemy => enemiesNextPos.Add(Entity.GetNextPosition(enemy.Position, enemy.CurrentDirection)));

			if (!enemiesNextPos.Contains(playerNextPos)) return false;
			Entity.CollideWithEnemy(this);
			return true;

		}

		public bool CheckGameCompleted()
		{
			return !LevelManager.Health.IsDead() && LevelManager is { Key: 2, RemainingCoins: <= 0 };

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
			GameState = GameState.PLAYING;
			LevelManager = new LevelManager(Player, GameMode);
			// LevelManager.NextLevel(GameMode);
		}

		public void GameOver()
		{
			GameState = GameState.GAME_OVER;
		}
	}
}
