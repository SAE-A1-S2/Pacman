using Engine.utils;

namespace Engine
{
	public class GameManager
	{
		public LevelManager LevelManager { get; private set; }
		public GameState GameState { get; private set; } = GameState.PLAYING;
		public GameMode GameMode;

		public Player Player { get; set; }

		public GameManager(GameMode gameMode)
		{
			Player = new();
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

		public void CheckGhostCollisions()
		{
			List<CellCoordinates> enemiesPos = [];
			Enemies.enemies.ToEnumerable().ToList().ForEach(enemy => enemiesPos.Add(enemy.Position));

			if (enemiesPos.Contains(Player.Position))
				Entity.CollideWithEnemy(this);
		}

		public bool CheckGameCompleted()
		{
			if (!LevelManager.Health.IsDead() || LevelManager.Key == 2 || LevelManager.RemainingCoins <= 0)
				return true;
			return false;
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

		public void GameOver()
		{
			GameState = GameState.GAME_OVER;
		}
	}
}
