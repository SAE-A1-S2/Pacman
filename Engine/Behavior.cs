namespace Engine
{
	public interface IEnemyBehavior
	{
		CellCoordinates NextPositon(Cell[,] maze, CellCoordinates currentPosition);
	}

	public class ChaserBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPositon(Cell[,] maze, CellCoordinates currentPosition)
		{
			CellCoordinates dst = Algorithms.FindPlayer(maze);
			return Algorithms.FindDijkstra(currentPosition, dst, maze);
		}
	}

	public class AmbusherBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPositon(Cell[,] maze, CellCoordinates currentPosition)
		{
			throw new NotImplementedException();
		}
	}

	public class WhimsicalBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPositon(Cell[,] maze, CellCoordinates currentPosition)
		{
			CellCoordinates dst = Algorithms.FindClosestCell(maze);
			return Algorithms.FindDijkstra(dst, currentPosition, maze);
		}
	}

	public class WandererBehavior : IEnemyBehavior
	{
		public CellCoordinates NextPositon(Cell[,] maze, CellCoordinates currentPosition)
		{
			throw new NotImplementedException();
		}
	}
}