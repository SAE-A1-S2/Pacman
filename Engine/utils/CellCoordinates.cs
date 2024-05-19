namespace Engine;

public struct CellCoordinates
{
	public int X;
	public int Y;

	public CellCoordinates(int x, int y)
	{
		X = x;
		Y = y;
	}

	public override bool Equals(object? obj) => obj is CellCoordinates coordinates && X == coordinates.X && Y == coordinates.Y;

	public override int GetHashCode() => HashCode.Combine(X, Y);

	public static bool operator ==(CellCoordinates a, CellCoordinates b)
	{
		return a.Equals(b);
	}

	public static bool operator !=(CellCoordinates a, CellCoordinates b)
	{
		return !(a == b);
	}
}
