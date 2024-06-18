using Engine.utils;
using DB;
using System.Text;

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
			SavedData savedData = Base.LoadSession(sessionId);
			Player = new Player(savedData.PlayerName, savedData.BonusValue);
			GameMode = (GameMode)Enum.Parse(typeof(GameMode), savedData.GameMode, true);
			LevelManager = new(savedData.Score, savedData.Keys, ParseMap(savedData.LevelMap), CellCoordinates.Parse(savedData.StartPos), CellCoordinates.Parse(savedData.EndPos), Player, (byte)savedData.PlayerHearts, (byte)savedData.PlayerHP, GameMode, CellCoordinates.Parse(savedData.PlayerPos), savedData.RemainingCoins);
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

		public int SaveSession()
		{
			SavedData savedData = new()
			{

				PlayerName = Player.Name,
				GameMode = GameMode.ToString(),
				PlayerHearts = LevelManager.Health.Lives,
				PlayerHP = LevelManager.Health.HealthPoints,
				Score = LevelManager.Score,
				Keys = LevelManager.Key,
				LevelWidth = LevelManager.LevelMap.GetLength(0),
				LevelHeight = LevelManager.LevelMap.GetLength(1),
				LevelMap = FormatMap(LevelManager.LevelMap),
				RemainingCoins = LevelManager.RemainingCoins,
				StartPos = LevelManager.MazeStartPos.ToString(),
				EndPos = LevelManager.MazeEndPos.ToString(),
				BonusValue = Player.m_Bonuses.FrontEndValue,
				PlayerPos = Player.Position.ToString()
			};
			GameState = GameState.PAUSED;
			return Base.SaveSession(savedData);
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

		private static string FormatMap(Cell[,] levelMap)
		{
			var rows = levelMap.GetLength(0);
			var cols = levelMap.GetLength(1);
			var sb = new StringBuilder();

			for (var i = 0; i < rows; i++)
			{
				for (var j = 0; j < cols; j++)
				{
					sb.Append((int)levelMap[i, j]); // Convert Cell enum value to int
					if (j < cols - 1)
					{
						sb.Append(',');  // Add comma separator between columns
					}
				}
				sb.AppendLine(";");
			}

			return sb.ToString();
		}

		private static Cell[,] ParseMap(string mapString)
		{
			var rows = mapString.Split(';');
			var levelMap = new Cell[rows.Length, rows[0].Split(',').Length]; // Determine the size based on string

			for (var i = 0; i < rows.Length; i++)
			{
				var cols = rows[i].Split(',');
				for (var j = 0; j < cols.Length; j++)
				{
					if (Enum.TryParse(cols[j], true, out Cell cell))
					{
						levelMap[i, j] = cell;
					}
				}
			}
			return levelMap;
		}
	}
}
