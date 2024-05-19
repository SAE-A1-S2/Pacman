// using Engine.utils;

using Engine.utils;

namespace Engine;

public struct Health
{
	public byte Lives { get; private set; } = 3;
	public byte HealthPoints { get; private set; } = 2;

	public Health() { }

	public void ReduceHealth()
	{
		if (--HealthPoints <= 0)
		{
			Lives--;
			HealthPoints = 2;
		}
	}

	public void ResetHealth()
	{
		Lives = 3;
		HealthPoints = 2;
	}

	public bool IsDead()
	{
		return Lives <= 0;
	}
}

public class LevelManager
{
	private static readonly int s_Width = 20;
	private static readonly int s_Height = 30;

	private MazeGenerator m_MazeGenerator;
	private Player m_Player;

	public int Score { get; private set; }

	public Health Health { get; private set; }

	public byte Key { get; private set; }
	public Cell[,] LevelMap { get; private set; } = new Cell[s_Width, s_Height];
	public CellCoordinates MazeStartPos { get; private set; }

	public LevelManager(Player player, GameMode gameMode)
	{
		Health = new();
		Score = 0;
		Key = 0;
		m_MazeGenerator = new(s_Width, s_Height);

		Initializelevel(gameMode);
		// The player should be an outside ref.
		m_Player = player;
		m_Player.PlacePlayer(LevelMap);

		//Test
		var pos = Enemies.FindEmptyPositions(LevelMap, 1);
		Enemies.Cain.SetStartingPosition(pos[0], LevelMap);

	}

	private void Initializelevel(GameMode gameMode = GameMode.INFINTE)
	{
		if (gameMode == GameMode.STORY)
		{
			StoryMode storyMode = new();
			LevelMap = storyMode.Maze;
			MazeStartPos = storyMode.StartPos;
		}
		else
		{
			LevelMap = m_MazeGenerator._map;
			MazeStartPos = m_MazeGenerator.Start;
		}
	}

	public void UpdateScore(int score)
	{
		Score += score;
	}

	public void AddKey()
	{
		if (Key > 2) return;
		Key++;
	}

	// public void NextLevel()
	// {
	//   live = 3;
	//   Score = 0;
	//   Health = 2;
	//   m_MazeGenerator = new(m_Width, m_Height);
	//   LevelMap = m_MazeGenerator._map;
	//   MazeStartPos = m_MazeGenerator.Start;
	// }

	public Cell HasCollidedWith(CellCoordinates nextPosition)
	{
		throw new NotImplementedException();
	}

	public void ResetLevel()
	{
		Health.ResetHealth();
		Score = 0;
		Key = 0;
		LevelMap = m_MazeGenerator._map;
	}


}
