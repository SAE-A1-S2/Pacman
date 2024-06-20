using Engine;
using Engine.utils;

namespace PacMan
{
	public partial class frmGame : Form
	{
		// Dessine un personnage (Pac-Man ou fantôme) sur le graphique
		private void DrawCharacter(Graphics g, int x, int y, ref CellCoordinates prevPos, string characterType, int cellSize)
		{
			// Obtient les coordonnées actuelles du personnage
			var currentPos = new CellCoordinates(x, y);

			// Détermine la direction du personnage
			var direction = characterType == "Player" ? gameManager.Player.CurrentDirection : Entity.GetDirection(prevPos, currentPos);

			// Obtient l'image appropriée pour le personnage et la direction
			var characterImage = GetCharacterImage(characterType, direction, ref animationFrame);

			// Dessine l'image du personnage à la position spécifiée
			g.DrawImage(characterImage, y * cellSize, x * cellSize, cellSize, cellSize);

			// Met à jour la position précédente du personnage
			prevPos = currentPos;
		}

		// Affiche le labyrinthe à l'écran
		public void DisplayMaze(Cell[,] maze)
		{
			const int cellSize = 30; // Taille d'une cellule
			int width = maze.GetLength(1) * cellSize; // Largeur du labyrinthe en pixels
			int height = maze.GetLength(0) * cellSize; // Hauteur du labyrinthe en pixels
			Bitmap bmp = new(width, height); // Crée une image bitmap pour dessiner le labyrinthe

			// Utilise un objet Graphics pour dessiner sur l'image bitmap
			using (Graphics g = Graphics.FromImage(bmp))
			{
				// Parcours toutes les cellules du labyrinthe
				for (int x = 0; x < maze.GetLength(0); x++)
				{
					for (int y = 0; y < maze.GetLength(1); y++)
					{
						// Dessine chaque cellule en fonction de son type
						switch (maze[x, y])
						{
							case Cell.CAIN:
								DrawCharacter(g, x, y, ref CainPrevPos, "Cain", cellSize);
								break;
							case Cell.WINSTON:
								DrawCharacter(g, x, y, ref WinstonPrevPos, "Winston", cellSize);
								break;
							case Cell.VIGGO:
								DrawCharacter(g, x, y, ref ViggoPrevPos, "Viggo", cellSize);
								break;
							case Cell.MARQUIS:
								DrawCharacter(g, x, y, ref MarquisPrevPos, "Marquis", cellSize);
								break;
							case Cell.JOHN:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								DrawCharacter(g, x, y, ref JohnPrevPos, "Player", cellSize);
								break;
							case Cell.WALL:
								g.DrawImage(Wall, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.COIN:
								g.DrawImage(Coin, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.HEALTH_KIT:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								g.DrawImage(HealthKit, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.TORCH:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								g.DrawImage(Torch, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.KEY:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								g.DrawImage(Key, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.EMPTY:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							default:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								break;
						}
					}
				}
			}
			// Affecte l'image bitmap dessinée au contrôle imgMap
			imgMap.Image = bmp;
		}

		// Obtient l'image à afficher pour un personnage en fonction de son type et de sa direction
		private Image GetCharacterImage(string characterType, Direction direction, ref int animationFrame)
		{
			string key = characterType + direction.ToString(); // Crée une clé unique pour l'image (ex: "PlayerUP")
			if (!characterImages.ContainsKey(key)) // Si l'image n'existe pas pour cette direction
				key = characterType + "Stop";       // Utilise l'image d'arrêt

			Image[] images = characterImages[key]; // Récupère le tableau d'images pour l'animation
			Image image = images[animationFrame % images.Length]; // Sélectionne l'image en fonction de l'indice d'animation
			animationFrame++; // Incrémente l'indice d'animation pour la prochaine image
			return image;
		}
	}
}