using Engine.utils;

namespace Engine
{
    public class Entity
    {
        public CellCoordinates Position { get; protected set }
        public string Name { get; protected set; }
        public Cell Kind { get; protected set; }
        protected Direction CurrentDirection { get; protected set; }

        protected static CellCoordinates GetNextPosition(CellCoordinates currentPosition, Direction direction)
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
        
        public void UpdatePosition(CellCoordinates newCell, Cell[,] maze)
        {
            maze[Position.X, Position.Y] = Cell.Empty;
            Position = newCell;
            maze[Position.X, Position.Y] = Kind;

        }

    }
}