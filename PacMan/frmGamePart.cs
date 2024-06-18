using Engine;
using Engine.utils;

namespace PacMan
{
	public partial class frmGame : Form
	{
		private void DrawCharacter(Graphics g, int x, int y, ref CellCoordinates prevPos, string characterType, int cellSize)
		{
			var currentPos = new CellCoordinates(x, y);
			var direction = characterType == "Player" ? currentDirection : GetDirection(prevPos, currentPos);
			var characterImage = GetCharacterImage(characterType, direction, ref animationFrame);
			g.DrawImage(characterImage, y * cellSize, x * cellSize, cellSize, cellSize);
			prevPos = currentPos;
		}

		public void DisplayMaze(Cell[,] maze)
		{
			const int cellSize = 30;
			int width = maze.GetLength(1) * cellSize;
			int height = maze.GetLength(0) * cellSize;
			Bitmap bmp = new(width, height);
			using (Graphics g = Graphics.FromImage(bmp))
			{
				for (int x = 0; x < maze.GetLength(0); x++)
				{
					for (int y = 0; y < maze.GetLength(1); y++)
					{
						switch (maze[x, y])
						{
							case Cell.Cain:
								DrawCharacter(g, x, y, ref CainPrevPos, "Cain", cellSize);
								break;
							case Cell.Winston:
								DrawCharacter(g, x, y, ref WinstonPrevPos, "Winston", cellSize);
								break;
							case Cell.Viggo:
								DrawCharacter(g, x, y, ref ViggoPrevPos, "Viggo", cellSize);
								break;
							case Cell.Marquis:
								DrawCharacter(g, x, y, ref MarquisPrevPos, "Marquis", cellSize);
								break;
							case Cell.John:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								DrawCharacter(g, x, y, ref JohnPrevPos, "Player", cellSize);
								break;
							case Cell.Wall:
								g.DrawImage(Wall, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.Coin:
								g.DrawImage(Coin, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.HealthKit:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								g.DrawImage(HealthKit, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.Torch:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								g.DrawImage(Torch, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.Key:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								g.DrawImage(Key, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.Empty:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							default:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								break;
						}
					}
				}
			}
			imgMap.Image = bmp;
		}

		private Image GetCharacterImage(string characterType, Direction direction, ref int animationFrame)
		{
			string key = characterType + direction.ToString();
			if (!characterImages.ContainsKey(key))
				key = characterType + "Stop";

			Image[] images = characterImages[key];
			Image image = images[animationFrame % images.Length];
			animationFrame++;
			return image;
		}

		private static Direction GetDirection(CellCoordinates src, CellCoordinates dst)
		{
			if (src.col < dst.col)
				return Direction.RIGHT;
			if (src.col > dst.col)
				return Direction.LEFT;
			if (src.row < dst.row)
				return Direction.DOWN;
			if (src.row > dst.row)
				return Direction.UP;
			return Direction.STOP;
		}
	}
}