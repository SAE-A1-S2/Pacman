// using Engine.utils;

using System.ComponentModel;
using System.Diagnostics;
using Engine.utils;

namespace Engine;

public class Health : INotifyPropertyChanged
{
	private static readonly byte _defaultLives = 3;
	private static readonly byte _defaultHP = 2;
	public byte Lives { get; private set; } = _defaultLives;
	public byte HealthPoints { get; private set; } = _defaultHP;

	public event PropertyChangedEventHandler? PropertyChanged;

	public Health() { }

	public void ReduceHealth()
	{
		if (!IsDead() && HealthPoints <= 0)
		{
			Lives--;
			HealthPoints = _defaultHP;
		}
		else
			HealthPoints--;
		Debug.WriteLine($"Lives: {Lives}");
		Debug.WriteLine($"Health: {HealthPoints}");

		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HealthPoints)));
	}

	public void IncrementHealth()
	{
		if (HealthPoints < _defaultHP) HealthPoints++;

		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HealthPoints)));
	}

	public void ResetHealth()
	{
		Lives = _defaultLives;
		HealthPoints = _defaultHP;
	}

	public bool IsDead() => Lives <= 0 && HealthPoints <= 0;
}

public class LevelManager : INotifyPropertyChanged
{
	private static readonly int s_Width = 30;
	private static readonly int s_Height = 20;

	private readonly MazeGenerator m_MazeGenerator;
	public int RemainingCoins { get; private set; }

	public int Score { get; private set; }

	public Health Health { get; private set; }
	public Player Player;

	public byte Key { get; private set; }
	public Cell[,] LevelMap { get; private set; } = new Cell[s_Width, s_Height];
	public CellCoordinates MazeStartPos { get; private set; }
	public CellCoordinates MazeEndPos { get; private set; }
	private readonly Dictionary<Cell, CellCoordinates> ObjectPositions = [];
	public event PropertyChangedEventHandler? PropertyChanged;

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

		PlaceStaticObjects([Cell.Key, Cell.Key, Cell.HealthKit, Cell.Torch]);

		//Test
		var pos = Enemies.FindEmptyPositions(LevelMap, 1);
		Enemies.Cain.SetStartingPosition(pos[0], LevelMap);

		// Place coins
		PlaceCoins();

	}

	private void Initializelevel(GameMode gameMode = GameMode.INFINITE)
	{
		if (gameMode == GameMode.STORY)
		{
			StoryMode storyMode = new();
			LevelMap = storyMode.Maze;
			MazeStartPos = storyMode.StartPos;
			MazeEndPos = storyMode.EndPos;
		}
		else
		{
			LevelMap = m_MazeGenerator._map;
			MazeStartPos = m_MazeGenerator.Start;
			MazeEndPos = m_MazeGenerator.End;
		}
	}

	public void UpdateScore(int score)
	{
		Score += score;
		if (score == 10)
			RemainingCoins--;

		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Score)));
	}

	private void PlaceCoins()
	{
		for (int x = 0; x < LevelMap.GetLength(0); x++)
		{
			for (int y = 0; y < LevelMap.GetLength(1); y++)
			{
				if (LevelMap[x, y] == Cell.Empty)
				{
					LevelMap[x, y] = Cell.Coin;
					RemainingCoins++;
				}
			}
		}
	}

	public void AddKey()
	{
		if (Key > 2) return;
		Key++;
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Key)));
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

	public void PlaceStaticObjects(List<Cell> objects)
	{
		CellCoordinates[] corners = [new(0, 0), new(s_Width - 1, 0), new(0, s_Height - 1), new(s_Width - 1, s_Height - 1)];
		foreach (var obj in objects)
		{
			CellCoordinates corner = corners[objects.IndexOf(obj)];
			CellCoordinates placement = Algorithms.FindCell(LevelMap, corner);
			LevelMap[placement.row, placement.col] = obj;
			ObjectPositions[obj] = placement;
		}
	}
}
