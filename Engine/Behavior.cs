namespace Engine.utils
{
	public interface IEnemyBehavior
	{
		CellCoordinates NextPositon(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP);
	}

	public class ChaserBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPositon(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP)
		{
			CellCoordinates dst = Algorithms.FindPlayer(maze);
			return Algorithms.FindBellManFord(currentPosition, dst, maze);
		}
	}

	public class AmbusherBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPositon(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP)
		{
			throw new NotImplementedException();
		}
	}

	public class WhimsicalBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPositon(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP)
		{
			CellCoordinates dst = Algorithms.FindClosestCell(maze);
			return Algorithms.FindDijkstra(dst, currentPosition, maze);
		}
	}

	public class WandererBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPositon(Cell[,] maze, CellCoordinates currentPosition, Direction direction = Direction.STOP)
		{
			throw new NotImplementedException();
		}
	}
}