using Engine.utils;
namespace Engine;

public struct CellCoordinates
{
	public int X;
	public int Y;

	public CellCoordinates(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

	public override bool Equals(object? obj)
	{
		if (!(obj is CellCoordinates))
			return false;

		var other = (CellCoordinates)obj;
		return this.X == other.X && this.Y == other.Y;
	}

	public override int GetHashCode()
	{
		unchecked
		{
			int hash = 17;
			hash = hash * 23 + X.GetHashCode();
			hash = hash * 23 + Y.GetHashCode();
			return hash;
		}
	}

	public static bool operator ==(CellCoordinates a, CellCoordinates b)
	{
		return a.Equals(b);
	}

	public static bool operator !=(CellCoordinates a, CellCoordinates b)
	{
		return !(a == b);
	}

	public static Direction GetDirection(CellCoordinates src, CellCoordinates dst)
	{
		if (src.X < dst.X)
			return Direction.RIGHT;
		if (src.X > dst.X)
			return Direction.LEFT;
		if (src.Y < dst.Y)
			return Direction.DOWN;
		if (src.Y > dst.Y)
			return Direction.UP;
		return Direction.STOP;
	}
}
