using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.utils
{
    public enum GameState
    {
        GAME_OVER,
        PLAYING,
        PAUSED,
    }

    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        STOP,
    }

    public enum GameMode
    {
        STORY,
        INFINTE,
    }

    public enum EnemyState
    {
        NORMAL,
        FREEZE,
    }
}
