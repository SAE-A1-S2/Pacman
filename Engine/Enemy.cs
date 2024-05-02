using Engine.utils;

namespace Engine
{
  public static class Enemies
  {
    public static Enemy Winston = new("Winston", new AmbusherBehavior());
    public static Enemy Cain = new("Cain", new ChaserBehavior());
    public static Enemy Viggo = new("Viggo", new WandererBehavior());
    public static Enemy Marquis = new("Marquis", new WhimsicalBehavior());
  }
  public sealed class Enemy : Entity
  {
    public EnemyState State { get; private set; }
    public bool IsFrozen => State == EnemyState.FRIGHTENED; // To be changed
    public IEnemyBehavior EnemyBehavior { get; private set; }
    private int moveCounter;
    private const int randomMoveInterval = 5;
    private Random random = new();
    public Cell[,] Maze { get; private set; }

    public Enemy(CellCoordinates startPosition, Cell[,] maze) // To be removed
    {
      Position = startPosition;
      State = EnemyState.CHASE;
      moveCounter = 0; // Initialize counter
      CurrentDirection = GetRandomDirection();
      Maze = maze;
      Name = "";
      EnemyBehavior = new ChaserBehavior();
    }

    public Enemy(string name, IEnemyBehavior enemyBehavior)
    {
      Name = name;
      EnemyBehavior = enemyBehavior;
      Maze = new Cell[,] { }; // To be removed
      Position = new CellCoordinates(); // TO be removed
    }

    public void SetStartingPosition(CellCoordinates start) { Position = start; }

    public void ChangeState(EnemyState newState)
    {
      State = newState;
    }

    //public void MoveRandom()
    //{
    //    // Always decrease moveCounter
    //    moveCounter--;

    //    // Check if current direction is still valid or if it's time to get a new direction
    //    if (moveCounter <= 0 || !IsPositionValid(GetNextPosition(Position, currentDirection)))
    //    {
    //        currentDirection = GetValidRandomDirection();
    //        moveCounter = randomMoveInterval;
    //    }

    //    // Move to the new position if valid
    //    CellCoordinates newPosition = GetNextPosition(Position, currentDirection);
    //    if (IsPositionValid(newPosition))
    //        UpdatePosition(newPosition);
    //    else if (moveCounter <= 0) // In case the new position is not valid and moveCounter is depleted, force direction update
    //    {
    //        currentDirection = GetValidRandomDirection();
    //        moveCounter = randomMoveInterval; // Reset move counter after a forced direction change
    //    }
    //}

    //private Direction GetValidRandomDirection()
    //{
    //    Direction newDirection;
    //    int attempts = 0;
    //    do
    //    {
    //        newDirection = GetRandomDirection();
    //        attempts++;
    //    } while (!IsPositionValid(GetNextPosition(Position, newDirection)) && attempts < 4);

    //    return newDirection;
    //}

    private Direction GetRandomDirection()
    {
      Array values = Enum.GetValues(typeof(Direction));
      return (Direction)values.GetValue(random.Next(values.Length))!; // To be checked if null
    }

    public void Freeze()
    {
      ChangeState(EnemyState.SCATTER);
      // Additional logic when frozen, e.g., disable collision effects
    }

    public void Unfreeze()
    {
      ChangeState(EnemyState.CHASE);
      // Resume normal movement and interactions
    }
  }
}

