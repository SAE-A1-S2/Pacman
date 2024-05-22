namespace Engine;

public struct CellCoordinates(int row, int col)
{
	public int row = row;
	public int col = col;

	public override bool Equals(object? obj) => obj is CellCoordinates coordinates && row == coordinates.row && col == coordinates.col;

	public override int GetHashCode() => HashCode.Combine(row, col);

	public static bool operator ==(CellCoordinates a, CellCoordinates b)
	{
		return a.Equals(b);
	}

	public static bool operator !=(CellCoordinates a, CellCoordinates b)
	{
		return !(a == b);
	}
}
