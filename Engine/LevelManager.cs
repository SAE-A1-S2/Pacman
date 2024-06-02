// using Engine.utils;

using Engine.utils;

namespace Engine;

public struct Health
{
	static private byte _defaultLives = 3;
	static private byte _defaultHP = 2;
	public byte Lives { get; private set; } = _defaultLives;
	public byte HealthPoints { get; private set; } = _defaultHP;

	public Health() { }

	public void ReduceHealth()
	{
		if (IsDead() && --HealthPoints > 0) return;
		Lives--;
		HealthPoints = _defaultHP;
	}

	public void IncrementHealth()
	{
		if (HealthPoints < _defaultHP) HealthPoints++;
	}

	public void ResetHealth()
	{
		Lives = _defaultLives;
		HealthPoints = _defaultHP;
	}

	public bool IsDead() => Lives <= 0;
}

public class LevelManager
{
	private static readonly int s_Width = 30;
	private static readonly int s_Height = 20;

	private MazeGenerator m_MazeGenerator;

	public int Score { get; private set; }

	public Health Health { get; private set; }
	public Player Player;

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
		Player = player;
		Player.PlacePlayer(LevelMap);

		//Test
		var pos = Enemies.FindEmptyPositions(LevelMap, 1);
		Enemies.Cain.SetStartingPosition(pos[0], LevelMap);

	}

	private void Initializelevel(GameMode gameMode = GameMode.INFINITE)
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
