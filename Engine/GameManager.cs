namespace Engine.utils;

public class GameManager
{
  public LevelManager LevelManager { get; private set; }
  public GameState gameState { get; private set; } = GameState.PLAYING;
  public GameManager(bool isStoryMode = false)
  {
    LevelManager = new LevelManager(isStoryMode);
  }

  public void CheckCollisions(Cell cellType)
  {
    switch (cellType)
    {
      case Cell.Coin:
        LevelManager.UpdateScore(10);
        break;
      case Cell.Kit:
        // Trigger power-up mode
        break;
      case Cell.Ghost:
        CollideWithEnemy();
        break;
      case Cell.Key:
        LevelManager.AddKey();
        break;

      default:
        break;
        // Add more cases as needed
    }
  }

  public void CollideWithEnemy()
  {
    LevelManager.ReduceHealth();
    if (LevelManager.live == 0)
    {
      gameState = GameState.GAME_OVER;
      // Optionally save the game data
    }
    else
    {
      LevelManager.LevelMap[LevelManager.player.Position.X, LevelManager.player.Position.Y] = Cell.Empty;  // Set the old position to empty
      LevelManager.player.SetPlayerPosition(LevelManager.mazeStartPos);
      LevelManager.LevelMap[LevelManager.player.Position.X, LevelManager.player.Position.Y] = Cell.John;
    }
  }
}
