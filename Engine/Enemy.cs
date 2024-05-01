namespace Engine.utils
{
    public static class Enemies
    {
        public static Enemy Winston = new("Winston", new AmbusherBehavior());
        public static Enemy Cain = new("Cain", new ChaserBehavior());
        public static Enemy Viggo = new("Viggo", new WandererBehavior());
        public static Enemy Marquis = new("Marquis", new WhimsicalBehavior());
    }
    public class Enemy
    {
        public string Name { get; set; }
        public CellCoordinates Position { get; protected set; }
        public EnemyState State { get; protected set; }
        public bool IsFrozen => State == EnemyState.FRIGHTENED; // To be changed
        public IEnemyBehavior EnemyBehavior { get; protected set; }
        protected Direction currentDirection;
        protected int moveCounter;
        protected const int randomMoveInterval = 5;
        protected Random random = new();
        public Cell[,] Maze { get; private set; }

        public Enemy(CellCoordinates startPosition, Cell[,] maze) // To be removed
        {
            Position = startPosition;
            State = EnemyState.CHASE;
            moveCounter = 0; // Initialize counter
            currentDirection = GetRandomDirection();
            Maze = maze;
            Name = "";
            EnemyBehavior = new ChaserBehavior();
        }

        public Enemy(string name, IEnemyBehavior enemyBehavior)
        {
            Name = name;
            EnemyBehavior = enemyBehavior;
            Maze = new Cell[,] {}; // To be removed
            Position = new CellCoordinates(); // TO be removed
        }

        public void SetStartingPostion(CellCoordinates start) { Position = start; }

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
            return (Direction)values.GetValue(random.Next(values.Length));
        }

        private CellCoordinates GetNextPosition(CellCoordinates currentPosition, Direction direction)
        {
            return direction switch
            {
                Direction.UP => new CellCoordinates(currentPosition.X, currentPosition.Y - 1),
                Direction.DOWN => new CellCoordinates(currentPosition.X, currentPosition.Y + 1),
                Direction.LEFT => new CellCoordinates(currentPosition.X - 1, currentPosition.Y),
                Direction.RIGHT => new CellCoordinates(currentPosition.X + 1, currentPosition.Y),
                _ => currentPosition,
            };
        }

        private void UpdatePosition(CellCoordinates newPosition)
        {
            Maze[Position.X, Position.Y] = Cell.Empty;
            Position = newPosition;
            Maze[Position.X, Position.Y] = Cell.Ghost;
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

