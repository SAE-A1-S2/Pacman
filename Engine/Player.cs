using Engine.utils;

namespace Engine;

public class Player(CellCoordinates playerPosition, string playerName = "John")
{
    public string Name { get; private set; } = playerName;
    public CellCoordinates Position { get; private set; } = playerPosition;

    public void SetPlayerPosition(CellCoordinates newPosition)
    {
        Position = newPosition;
    }

    public void SetPlayerName(string newName)
    {
        Name = newName;
    }

    public void PlacePlayer(Cell[,] maze)
    {
        for (int x = 0; x < maze.GetLength(0); x++)
            for (int y = 0; y < maze.GetLength(1); y++)
                if (maze[x, y] == Cell.Start)
                {
                    maze[x, y] = Cell.John;
                    Position = new CellCoordinates(x, y);
                }
    }

    public void Move(Direction direction, Cell[,] maze, GameManager gameManager)
    {
        CellCoordinates newPosition = GetNextPosition(Position, direction);

        // Check if the new position is within the bounds of the maze
        if (IsPositionValid(newPosition, maze))
        {
            gameManager.CheckCollisions(maze[newPosition.X, newPosition.Y]);
            // Check if the cell at the new position is not a wall
            if (maze[newPosition.X, newPosition.Y] != Cell.Wall)
            {
                if (maze[newPosition.X, newPosition.Y] == Cell.Empty || maze[newPosition.X, newPosition.Y] == Cell.Coin)
                {
                    maze[Position.X, Position.Y] = Cell.Empty;  // Set the old position to empty
                    Position = newPosition;
                    maze[Position.X, Position.Y] = Cell.John;  // Mark the new position with the player

                    HandleCellInteraction(maze[Position.X, Position.Y]);
                }
            }
        }
    }

    private static bool IsPositionValid(CellCoordinates position, Cell[,] maze)
    {
        // Check if the position is within the maze boundaries
        return position.X >= 0 && position.X < maze.GetLength(0) &&
               position.Y >= 0 && position.Y < maze.GetLength(1);
    }

    private static CellCoordinates GetNextPosition(CellCoordinates currentPosition, Direction direction)
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

    private static void HandleCellInteraction(Cell cellType)
    {
        switch (cellType)
        {
            case Cell.Coin:
                // Increment score
                break;
            case Cell.kit:
                // Store power-up
                break;
            case Cell.Ghost:
                // Handle collision with ghost
                break;

            default:
                break;
                // Add more cases as needed
        }
    }
}