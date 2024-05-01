using Engine.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Viggo : Enemy
    {
        private int startX, endX, startY, endY;

        public Viggo(CellCoordinates startPosition, Cell[,] maze) : base(startPosition, maze)
        {
            CalculateZoneBounds();
            // PlaceRandomly(startPosition);
        }

        private void CalculateZoneBounds()
        {
            int midX = maze.GetLength(0) / 2;
            int midY = maze.GetLength(1) / 2;
            startX = midX;
            endX = maze.GetLength(0) - 1;
            startY = 0;
            endY = midY - 1;
        }

        protected override void PlaceRandomly(CellCoordinates playerStart)
        {
            int safeDistanceFromPlayer = 5;
            int maxAttempts = 100;
            int attempts = 0;

            bool placed = false;
            while (!placed && attempts < maxAttempts)
            {
                attempts++;
                int x = random.Next(startX, endX + 1);
                int y = random.Next(startY, endY + 1);

                var newPos = new CellCoordinates(x, y);
                if (isPositionValidForEnemy(newPos, playerStart, safeDistanceFromPlayer))
                {
                    position = newPos;
                    maze[x, y] = Cell.Ghost;
                    placed = true;
                }
            }

            if (!placed)
                throw new Exception("Failed to place ghost after maximum attempts.");
        }

        protected override bool isPositionValidForEnemy(CellCoordinates pos, CellCoordinates playerStart, int playerDist)
        {
            if (maze[pos.X, pos.Y] != Cell.Empty) return false;

            // Distance avec joueur
            if (Math.Abs(pos.X - playerStart.X) + Math.Abs(pos.Y - playerStart.Y) < playerDist) return false;

            return pos.X >= startX && pos.X <= endX && pos.Y >= startY && pos.Y <= endY && maze[pos.X, pos.Y] != Cell.Wall;

        }

        protected override bool IsPositionValid(CellCoordinates pos)
        {
            return pos.X >= startX && pos.X <= endX && pos.Y >= startY && pos.Y <= endY && maze[pos.X, pos.Y] != Cell.Wall;
        }


    }
}
