using System.Diagnostics;
// TODO  i will fix the issue!!!
namespace Engine
{
	public static class Algorithms
	{

		public static CellCoordinates FindClosestCell(Cell[,] maze) // this will be changed
		{
			var john = FindPlayer(maze);
			List<Cell> targets = [Cell.HealthKit, Cell.Key, Cell.SpeedBoost, Cell.Torch, Cell.InvisibilityCloack];
			CellCoordinates closest = new(-1, -1);
			double minDistance = double.MaxValue;

			for (int y = 0; y < maze.GetLength(0); y++)
			{
				for (int x = 0; x < maze.GetLength(1); x++)
				{
					if (targets.Contains(maze[y, x]))
					{
						double distance = Math.Sqrt(Math.Pow(x - john.row, 2) + Math.Pow(y - john.col, 2));
						if (distance < minDistance)
						{
							minDistance = distance;
							closest = new CellCoordinates(x, y);
						}
					}
				}
			}

			return closest;
		}

		public static CellCoordinates FindCell(Cell[,] maze, CellCoordinates target, Cell cellType = Cell.Empty, int maxSearchDistance = 0)
		{
			if (maxSearchDistance <= 0)
				maxSearchDistance = Math.Max(maze.GetLength(0), maze.GetLength(1)) / 4;

			(int dr, int dc)[] directions = [(1, 0), (0, 1), (-1, 0), (0, -1)];

			int currentDistance = 0;
			int dr = 0, dc = 1;
			CellCoordinates current = target;

			while (currentDistance <= maxSearchDistance)
			{
				if (Entity.IsInBounds(current, maze) && maze[current.row, current.col] == cellType)
					return current;

				current.row += dr;
				current.col += dc;

				if (!Entity.IsInBounds(current, maze) || maze[current.row, current.col] != Cell.Empty || Math.Abs(current.row - target.row) + Math.Abs(current.col - target.col) > currentDistance)
				{
					(dr, dc) = (-dc, dr);
					currentDistance++;
				}
			}

			return FindRandomEmptyCell(maze);
		}

		private static CellCoordinates FindRandomEmptyCell(Cell[,] maze)
		{
			Random random = new();
			CellCoordinates cell = new(random.Next(maze.GetLength(0)), random.Next(maze.GetLength(1)));

			while (maze[cell.row, cell.col] != Cell.Empty)
				cell = new CellCoordinates(random.Next(maze.GetLength(0)), random.Next(maze.GetLength(1)));

			return cell;
		}


		public static CellCoordinates FindPlayer(Cell[,] maze)
		{
			CellCoordinates pos = new(0, 0);
			for (int x = 0; x < maze.GetLength(0); x++)
				for (int y = 0; y < maze.GetLength(1); y++)
					if (maze[x, y] == Cell.John)
						pos = new CellCoordinates(x, y);
			return pos;
		}

		public static CellCoordinates FindDijkstra(CellCoordinates src, CellCoordinates dst, Cell[,] _maze)
		{
			var directions = new (int, int)[] { (1, 0), (0, 1), (-1, 0), (0, -1) };
			var distance = new Dictionary<CellCoordinates, int>();
			var prev = new Dictionary<CellCoordinates, CellCoordinates?>();
			var priorityQueue = new SortedDictionary<int, Queue<CellCoordinates>>();

			distance[src] = 0;
			Enqueue(priorityQueue, 0, src);

			while (priorityQueue.Count > 0)
			{
				var current = Dequeue(priorityQueue);
				if (current.row == dst.row && current.col == dst.col)
					return ReconstructPath(prev, dst, src);

				foreach (var dir in directions)
				{
					var neighbor = new CellCoordinates(current.row + dir.Item1, current.col + dir.Item2);
					if (IsWithinBounds(neighbor, _maze) && _maze[neighbor.row, neighbor.col] != Cell.Wall) // Verifier si la cellule est valide
					{
						int newDist = distance[current] + 1;
						if (!distance.TryGetValue(neighbor, out int value) || newDist < value)
						{
							value = newDist;
							distance[neighbor] = value;
							prev[neighbor] = current;
							Enqueue(priorityQueue, newDist, neighbor);
						}
					}
				}
			}

			return src;
		}

		public static CellCoordinates FindBellManFord(CellCoordinates src, CellCoordinates dst, Cell[,] _maze)
		{
			var dist = new Dictionary<CellCoordinates, int>();
			var pred = new Dictionary<CellCoordinates, CellCoordinates?>();
			var directions = new[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

			// Initialisation des distances et prédécesseurs
			for (int x = 0; x < _maze.GetLength(0); x++)
			{
				for (int y = 0; y < _maze.GetLength(1); y++)
				{
					dist[new CellCoordinates(x, y)] = int.MaxValue;
					pred[new CellCoordinates(x, y)] = null;
				}
			}
			dist[src] = 0;

			// Relaxation des arêtes
			for (int i = 0; i < _maze.GetLength(0) * _maze.GetLength(1) - 1; i++)
			{
				for (int x = 0; x < _maze.GetLength(0); x++)
				{
					for (int y = 0; y < _maze.GetLength(1); y++)
					{
						var u = new CellCoordinates(x, y);
						if (dist[u] == int.MaxValue || _maze[u.row, u.col] == Cell.Wall) continue;

						foreach (var direction in directions)
						{
							var v = new CellCoordinates(x + direction.Item1, y + direction.Item2);
							if (IsWithinBounds(v, _maze) && (_maze[v.row, v.col] != Cell.Wall) && dist[u] + 1 < dist[v])
							{
								dist[v] = dist[u] + 1;
								pred[v] = u;
							}
						}
					}
				}
			}

			// Reconstruction du chemin
			return ReconstructPath(pred, dst, src);
		}

		private static CellCoordinates ReconstructPath(Dictionary<CellCoordinates, CellCoordinates?> pred, CellCoordinates dst, CellCoordinates src)
		{
			var current = dst;
			// Construct the path backwards from the destination to the source
			var path = new Stack<CellCoordinates>();

			while (!current.Equals(src))
			{
				path.Push(current);
				if (pred.TryGetValue(current, out CellCoordinates? value) && value.HasValue)
					current = value.Value;
				else
					return src;
			}

			return path.Pop();
		}

		private static void Enqueue(SortedDictionary<int, Queue<CellCoordinates>> pq, int cost, CellCoordinates cell)
		{
			if (!pq.ContainsKey(cost))
				pq[cost] = new Queue<CellCoordinates>();
			pq[cost].Enqueue(cell);
		}

		private static CellCoordinates Dequeue(SortedDictionary<int, Queue<CellCoordinates>> pq)
		{
			var firstKey = pq.Keys.First();
			var queue = pq[firstKey];
			var cell = queue.Dequeue();
			if (queue.Count == 0)
				pq.Remove(firstKey);
			return cell;
		}

		private static bool IsWithinBounds(CellCoordinates cell, Cell[,] maze) =>
			cell.row >= 0 && cell.row < maze.GetLength(0) && cell.col >= 0 && cell.col < maze.GetLength(1);
	}
}
