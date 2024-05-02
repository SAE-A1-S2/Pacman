// using Engine.utils;

namespace Engine;

public class LevelManager
{
  private static readonly int width = 30;
  private static readonly int height = 20;
  public byte live { get; private set; }
  public int Score { get; private set; }
  public byte Health { get; private set; }
  public byte Key { get; private set; }
  public Cell[,] LevelMap { get; private set; } = new Cell[width, height];
  private MazeGenerator mazeGenerator;
  public CellCoordinates mazeStartPos { get; private set; }
  public Player player { get; private set; }

  public LevelManager(bool isStoryMode = false)
  {
    live = 3;
    Score = 0;
    Health = 2;
    Key = 0;
    mazeGenerator = new(width, height);

    Initializelevel(isStoryMode);
    // Place player
    player = new Player();
    player.SetPlayerPosition(new CellCoordinates(0, 0));
    player.PlacePlayer(LevelMap);
  }

  private void Initializelevel(bool isStoryMode = false)
  {
    if (isStoryMode)
    {
      StoryMode storyMode = new();
      LevelMap = storyMode.maze;
      mazeStartPos = storyMode.startPos;
    }
    else
    {
      LevelMap = mazeGenerator._map;
      mazeStartPos = mazeGenerator.Start;
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

  public void ReduceHealth()
  {
    Health--;
    if (Health <= 0)
    {
      live--;
      Health = 2;
    }
  }

  public void NextLevel()
  {
    live = 3;
    Score = 0;
    Health = 2;
    mazeGenerator = new(width, height);
    LevelMap = mazeGenerator._map;
    mazeStartPos = mazeGenerator.Start;
  }

  public void ResetLevel()
  {
    live = 3;
    Score = 0;
    Health = 2;
    Key = 0;
    LevelMap = mazeGenerator._map;
  }


}
