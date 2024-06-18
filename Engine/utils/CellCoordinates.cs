/*
GROUPE D-06
SAE 2.01
2023-2024

plus d'informations:
https://learn.microsoft.com/fr-fr/dotnet/api/system.object.gethashcode?view=net-8.0

Résumé:
Ce fichier contient la structure CellCoordinates qui représente les coordonnées d'une cellule dans le labyrinthe du jeu. 
Elle inclut des méthodes et des opérateurs pour comparer des instances de CellCoordinates.
*/

namespace Engine
{
	/// <summary>
	/// Structure CellCoordinates représentant les coordonnées d'une cellule dans le labyrinthe.
	/// </summary>
	public struct CellCoordinates(int Row, int Col)
	{
		// Coordonnée de la ligne
		public int row = Row;
		// Coordonnée de la colonne
		public int col = Col;

		/// <summary>
		/// Détermine si l'objet spécifié est égal à l'instance actuelle.
		/// </summary>
		/// <param name="obj">Objet à comparer avec l'instance actuelle.</param>
		/// <returns>True si l'objet spécifié est égal à l'instance actuelle; sinon, False.</returns>
		public override readonly bool Equals(object? obj) => obj is CellCoordinates coordinates && row == coordinates.row && col == coordinates.col;

		/// <summary>
		/// Sert de fonction de hachage par défaut.
		/// </summary>
		/// <returns>Code de hachage pour l'instance actuelle.</returns>
		public override readonly int GetHashCode() => HashCode.Combine(row, col);

		/// <summary>
		/// Détermine si deux instances de CellCoordinates sont égales.
		/// </summary>
		/// <param name="a">Première instance de CellCoordinates.</param>
		/// <param name="b">Deuxième instance de CellCoordinates.</param>
		/// <returns>True si les instances sont égales; sinon, False.</returns>
		public static bool operator ==(CellCoordinates a, CellCoordinates b) => a.Equals(b);

		/// <summary>
		/// Détermine si deux instances de CellCoordinates ne sont pas égales.
		/// </summary>
		/// <param name="a">Première instance de CellCoordinates.</param>
		/// <param name="b">Deuxième instance de CellCoordinates.</param>
		/// <returns>True si les instances ne sont pas égales; sinon, False.</returns>
		public static bool operator !=(CellCoordinates a, CellCoordinates b) => !(a == b);

		/// <summary>
		/// Convertit une instance de CellCoordinates en une chaîne de caractères.
		/// </summary>
		/// <returns>Chaîne de caractères correspondant à l'instance actuelle.</returns>
		public override readonly string ToString() => $"{row},{col}";

		/// <summary>
		/// Convertit une chaîne de caractères en une instance de CellCoordinates.
		/// </summary>
		/// <param name="cellString">Chaîne de caractères à convertir.</param>
		/// <returns>Instance de CellCoordinates correspondant à la chaîne de caractères.</returns>
		public static CellCoordinates Parse(string cellString)
		{
			string[] parts = cellString.Split(',');

			return new CellCoordinates
			{
				row = int.Parse(parts[0]),
				col = int.Parse(parts[1])
			};
		}
	}
}