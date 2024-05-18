using Engine.utils;

namespace Engine
{
	public interface IEnemyBehavior
	{
		Direction NextPosition(Cell[,] maze, CellCoordinates currentPosition);
	}

	public class ChaserBehavior : IEnemyBehavior
	{
		public Direction
		NextPosition(Cell[,] maze, CellCoordinates currentPosition)
		{
			throw new NotImplementedException();
		}
	}

	public class AmbusherBehavior : IEnemyBehavior
	{
		public Direction
		NextPosition(Cell[,] maze, CellCoordinates currentPosition)
		{
			throw new NotImplementedException();
		}
	}

	public class WhimsicalBehavior : IEnemyBehavior
	{
		public Direction
		NextPosition(Cell[,] maze, CellCoordinates currentPosition)
		{
			throw new NotImplementedException();
		}
	}

	public class WandererBehavior : IEnemyBehavior
	{
		public Direction
		NextPosition(Cell[,] maze, CellCoordinates currentPosition)
		{
			throw new NotImplementedException();
		}
	}
}
