using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Engine.utils;

public class GameManager
{
    public LevelManager LevelManager { get; private set; }
    public GameState gameState { get; private set; } = GameState.PLAYING;
    public GameManager(bool isStoryMode = false)
    {
        LevelManager = new LevelManager(isStoryMode);
    }

    public void HandleKeyInput(Keys key)
    {
        var keyToDirectionMap = new Dictionary<Keys, Direction>
    {
        { Keys.Z, Direction.UP },
        { Keys.Up, Direction.UP },
        { Keys.Down, Direction.DOWN },
        { Keys.S, Direction.DOWN },
        { Keys.Left, Direction.LEFT },
        { Keys.Q, Direction.LEFT },
        { Keys.Right, Direction.RIGHT },
        { Keys.D, Direction.RIGHT }
    };

        if (keyToDirectionMap.TryGetValue(key, out Direction direction))
            LevelManager.player.Move(direction, LevelManager.LevelMap, this);
    }


    public void CheckCollisions(Cell cellType)
    {
        switch (cellType)
        {
            case Cell.Coin:
                LevelManager.UpdateScore(10);
                break;
            case Cell.kit:
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