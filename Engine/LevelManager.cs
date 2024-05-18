// using Engine.utils;

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

	public bool IsDead()
	{
		return Lives <= 0;
	}
}

public class LevelManager
{
	private static readonly int s_Width = 30;
	private static readonly int s_Height = 20;

	private MazeGenerator m_MazeGenerator;
	private Player m_Player;

	public int Score { get; private set; }

	public Health Health { get; private set; }

	public byte Key { get; private set; }
	public Cell[,] LevelMap { get; private set; } = new Cell[s_Width, s_Height];
	public CellCoordinates MazeStartPos { get; private set; }

	public LevelManager(Player player, bool isStoryMode = false)
	{
		Health = new();
		Score = 0;
		Key = 0;
		m_MazeGenerator = new(s_Width, s_Height);

		Initializelevel(isStoryMode);
		// The player should be an outside ref.
		m_Player = player;
		m_Player.SetPlayerPosition(new CellCoordinates(0, 0));
		m_Player.PlacePlayer(LevelMap);
	}

	private void Initializelevel(bool isStoryMode = false)
	{
		if (isStoryMode)
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
		throw new Exception("not implemented");
	}

	public void ResetLevel()
	{
		Health = new();
		Score = 0;
		Key = 0;
		LevelMap = m_MazeGenerator._map;
	}


}
