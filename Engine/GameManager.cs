using Engine.utils;

namespace Engine
{
  public class GameManager
  {
    public LevelManager LevelManager { get; private set; }
    public GameState gameState { get; private set; } = GameState.PLAYING;
    public GameMode GameMode;

    public Player m_Player { get; set; }

    public GameManager(GameMode gameMode = GameMode.STORY)
    {
      m_Player = new();
      GameMode = gameMode;
      LevelManager = new LevelManager(m_Player, false);
    }

    public void Run()
    {
      while (gameState == GameState.PLAYING) {

      }
    }

    public void Pause() 
    {
      gameState = GameState.PAUSED;
    }

    public void Resume() 
    {
      gameState = GameState.PLAYING;
    }

    public void NextLevel() 
    {

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
      LevelManager.Health.ReduceHealth();
      if (LevelManager.Health.IsDead())
      {
        gameState = GameState.GAME_OVER;
        // Optionally save the game data
      }
      else
      {
        LevelManager.LevelMap[m_Player.Position.X, m_Player.Position.Y] = Cell.Empty;  // Set the old position to empty
        m_Player.SetPlayerPosition(LevelManager.MazeStartPos);
        LevelManager.LevelMap[m_Player.Position.X, m_Player.Position.Y] = Cell.John;
      }
    }
  }
}
