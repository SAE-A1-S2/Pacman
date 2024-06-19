﻿using System.ComponentModel;
using Engine.utils;

namespace Engine;

public class LevelManager : INotifyPropertyChanged
{
	private static readonly int s_Width = 30;
	private static readonly int s_Height = 20;
	private MazeGenerator m_MazeGenerator;
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

	public LevelManager(int score, int key, Cell[,] levelMap, CellCoordinates mazeStartPos, CellCoordinates mazeEndPos, Player player, byte lives, byte healthPoints, GameMode gameMode, CellCoordinates playerPosition, int remainingCoins)
	{
		Health = new(lives, healthPoints);
		Score = score;
		Key = (byte)key;
		RemainingCoins = remainingCoins;
		Player = player;
		m_MazeGenerator = new(s_Width, s_Height, levelMap, mazeStartPos, mazeEndPos);

		InitializeLevel(gameMode);
		Player.PlacePlayer(LevelMap, mazeStartPos, playerPosition);
	}

	public LevelManager(Player player, GameMode gameMode, string PlayerUid = "")
	{
		Health = new Health();
		Score = 0;
		Key = 0;
		m_MazeGenerator = new MazeGenerator(s_Width, s_Height);

		InitializeLevel(gameMode, PlayerUid);
		// The player should be an outside ref.
		Player = player;
		Player.PlacePlayer(LevelMap, MazeStartPos, MazeStartPos);

		PlaceStaticObjects([Cell.KEY, Cell.KEY, Cell.HEALTH_KIT, Cell.TORCH]);

		PlaceEnemies();

		PlaceCoins();

	}

	private void PlaceEnemies()
	{
		var pos = Enemies.FindEmptyPositions(LevelMap, 4, MazeStartPos);
		Enemies.Cain.SetStartingPosition(pos[0], LevelMap);
		Enemies.Viggo.SetStartingPosition(pos[1], LevelMap);
		Enemies.Marquis.SetStartingPosition(pos[2], LevelMap);
		Enemies.Winston.SetStartingPosition(pos[3], LevelMap);
	}

	private void InitializeLevel(GameMode gameMode = GameMode.INFINITE, string PlayerUid = "")
	{
		if (gameMode == GameMode.STORY)
		{
			StoryMode storyMode = new(PlayerUid);
			LevelMap = storyMode.Maze;
			MazeStartPos = storyMode.StartPos;
			MazeEndPos = storyMode.EndPos;
		}
		else
		{
			LevelMap = m_MazeGenerator.Map;
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
		for (var x = 0; x < LevelMap.GetLength(0); x++)
		{
			for (var y = 0; y < LevelMap.GetLength(1); y++)
			{
				if (LevelMap[x, y] != Cell.EMPTY) continue;
				LevelMap[x, y] = Cell.COIN;
				RemainingCoins++;
			}
		}
	}

	public void AddKey()
	{
		if (Key > 2) return;
		Key++;
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Key)));
	}

	public void ResetLevel(GameMode gameMode, string PlayerUid)
	{
		Health.ResetHealth();
		Score = 0;
		Key = 0;

		InitializeLevel(gameMode, PlayerUid);

		Player.PlacePlayer(LevelMap, MazeStartPos, MazeStartPos);
		PlaceStaticObjects([Cell.KEY, Cell.KEY, Cell.HEALTH_KIT, Cell.TORCH]);

		PlaceCoins();
		RemainingCoins = 0;

		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Key)));

		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Score)));
	}

	public void PlaceStaticObjects(List<Cell> objects)
	{
		CellCoordinates[] corners = [new CellCoordinates(0, 0), new CellCoordinates(s_Width - 1, 0), new CellCoordinates(0, s_Height - 1), new CellCoordinates(s_Width - 1, s_Height - 1)];
		foreach (var obj in objects)
		{
			var corner = corners[objects.IndexOf(obj)];
			var placement = Algorithms.FindCell(LevelMap, corner);
			LevelMap[placement.Row, placement.Col] = obj;
			ObjectPositions[obj] = placement;
		}
	}
}
