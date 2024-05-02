using Engine.utils;

namespace Engine
{
  public sealed class Player : Entity
  {
    public Player(string name = "John")
    {
      Name = name;
      Kind = Cell.John;
    }

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
      if (MazeGenerator.IsInBounds(newPosition, maze))
      {
        gameManager.CheckCollisions(maze[newPosition.X, newPosition.Y]);
        // Check if the cell at the new position is not a wall
        if (maze[newPosition.X, newPosition.Y] != Cell.Wall)
        {
          if (maze[newPosition.X, newPosition.Y] == Cell.Empty || maze[newPosition.X, newPosition.Y] == Cell.Coin)
          {
            UpdatePosition(newPosition, maze);

            HandleCellInteraction(maze[Position.X, Position.Y]);
          }
        }
      }
    }


    private static void HandleCellInteraction(Cell cellType)
    {
      switch (cellType)
      {
        case Cell.Coin:
          // Increment score
          break;
        case Cell.Kit:
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
}
