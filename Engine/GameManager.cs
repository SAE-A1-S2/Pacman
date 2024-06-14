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
			Player = new(playerName);
			GameMode = gameMode;
			LevelManager = new LevelManager(Player, gameMode);
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
			var PlayerNextPos = Entity.GetNextPosition(Player.Position, Player.CurrentDirection);
			Enemies.enemies.ForEach(enemy => enemiesNextPos.Add(Entity.GetNextPosition(enemy.Position, enemy.CurrentDirection)));

			if (enemiesNextPos.Contains(PlayerNextPos))
			{
				Entity.CollideWithEnemy(this);
				return true;
			}

			return false;
		}

		public bool CheckGameCompleted()
		{
			return !LevelManager.Health.IsDead() && LevelManager.Key == 2 && LevelManager.RemainingCoins <= 0;

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
			LevelManager = new(Player, GameMode);
			// LevelManager.NextLevel(GameMode);
		}

		public void GameOver()
		{
			GameState = GameState.GAME_OVER;
		}
	}
}
