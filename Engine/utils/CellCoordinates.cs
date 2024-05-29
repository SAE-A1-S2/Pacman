namespace Engine;

public struct CellCoordinates(int _row, int _col)
{
	public int row = _row;
	public int col = _col;

	public override bool Equals(object? obj) => obj is CellCoordinates coordinates && row == coordinates.row && col == coordinates.col;

	public override int GetHashCode() => HashCode.Combine(row, col);

	public static bool operator ==(CellCoordinates a, CellCoordinates b) => a.Equals(b);

	public static bool operator !=(CellCoordinates a, CellCoordinates b) => !(a == b);

}
