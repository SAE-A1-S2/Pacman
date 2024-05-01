using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.utils
{

    public abstract class Enemy
    {
        public CellCoordinates position { get; protected set; }
        public EnemyState State { get; protected set; }
        public bool IsFrozen => State == EnemyState.FREEZE;
        protected Direction currentDirection;
        protected int moveCounter;
        protected const int randomMoveInterval = 5;
        protected Random random = new();
        protected Cell[,] maze;

        public Enemy(CellCoordinates startPosition, Cell[,] maze)
        {
            position = startPosition;
            State = EnemyState.NORMAL;
            moveCounter = 0; // Initialize counter
            currentDirection = GetRandomDirection();
            this.maze = maze;
        }

        public void ChangeState(EnemyState newState)
        {
            State = newState;
        }

        abstract protected void PlaceRandomly(CellCoordinates playerStart);

        abstract protected bool isPositionValidForEnemy(CellCoordinates pos, CellCoordinates playerStart, int playerDist);

        public void MoveRandom()
        {
            // Always decrease moveCounter
            moveCounter--;

            // Check if current direction is still valid or if it's time to get a new direction
            if (moveCounter <= 0 || !IsPositionValid(GetNextPosition(position, currentDirection)))
            {
                currentDirection = GetValidRandomDirection();
                moveCounter = randomMoveInterval;
            }

            // Move to the new position if valid
            CellCoordinates newPosition = GetNextPosition(position, currentDirection);
            if (IsPositionValid(newPosition))
                UpdatePosition(newPosition);
            else if (moveCounter <= 0) // In case the new position is not valid and moveCounter is depleted, force direction update
            {
                currentDirection = GetValidRandomDirection();
                moveCounter = randomMoveInterval; // Reset move counter after a forced direction change
            }
        }

        abstract protected bool IsPositionValid(CellCoordinates newPosition);


        private Direction GetValidRandomDirection()
        {
            Direction newDirection;
            int attempts = 0;
            do
            {
                newDirection = GetRandomDirection();
                attempts++;
            } while (!IsPositionValid(GetNextPosition(position, newDirection)) && attempts < 4);

            return newDirection;
        }

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
            maze[position.X, position.Y] = Cell.Empty;
            position = newPosition;
            maze[position.X, position.Y] = Cell.Ghost;
        }


        public void Freeze()
        {
            ChangeState(EnemyState.FREEZE);
            // Additional logic when frozen, e.g., disable collision effects
        }

        public void Unfreeze()
        {
            ChangeState(EnemyState.NORMAL);
            // Resume normal movement and interactions
        }
    }
}

