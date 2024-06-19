namespace Engine.utils
{
	public interface IEnemyBehavior
	{
		CellCoordinates NextPosition(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP);
	}

	public class ChaserBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPosition(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP)
		{
			CellCoordinates dst = Algorithms.FindPlayer(maze);
			return Algorithms.FindBellmanFord(currentPosition, dst, maze);
		}
	}

	public class AmbusherBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPosition(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP)
		{
			CellCoordinates playerPosition = Algorithms.FindPlayer(maze);
			CellCoordinates dst = Algorithms.CalculatePositionAhead(maze, playerPosition, direction, 2);
			return Algorithms.FindBellmanFord(currentPosition, dst, maze);
		}
	}

	public class WhimsicalBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPosition(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP)
		{
			CellCoordinates dst = Algorithms.FindClosestCell(maze);
			return Algorithms.FindDijkstra(currentPosition, dst, maze);
		}
	}

	public class WandererBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPosition(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP)
		{
			throw new NotImplementedException();
		}
	}
}