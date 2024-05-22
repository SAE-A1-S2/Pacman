using System.Diagnostics;
// TODO  i will fix the issue!!!
namespace Engine
{
	public static class Algorithms
	{

		public static CellCoordinates FindClosestCell(Cell[,] maze)
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
						double distance = Math.Sqrt(Math.Pow(x - john.X, 2) + Math.Pow(y - john.Y, 2));
						if (distance < minDistance)
						{
							minDistance = distance;
							closest = new(x, y);
						}
					}
				}
			}

			return closest;
		}


		public static CellCoordinates FindPlayer(Cell[,] maze)
		{
			CellCoordinates pos = new(0, 0);
			for (int x = 0; x < maze.GetLength(0); x++)
				for (int y = 0; y < maze.GetLength(1); y++)
					if (maze[x, y] == Cell.John)
						pos = new CellCoordinates(x, y);
			Debug.WriteLine($"Player is at ({pos.X}, {pos.Y})");
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
				if (current.X == dst.X && current.Y == dst.Y)
					return ReconstructPath(prev, dst, src);

				foreach (var dir in directions)
				{
					var neighbor = new CellCoordinates(current.X + dir.Item1, current.Y + dir.Item2);
					if (IsWithinBounds(neighbor, _maze) && _maze[neighbor.X, neighbor.Y] != Cell.Wall) // Verifier si la cellule est valide
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
						if (dist[u] == int.MaxValue || _maze[u.X, u.Y] == Cell.Wall) continue;

						foreach (var direction in directions)
						{
							var v = new CellCoordinates(x + direction.Item1, y + direction.Item2);
							if (IsWithinBounds(v, _maze) && IsCorrectCell(v, _maze) && dist[u] + 1 < dist[v])
							{
								dist[v] = dist[u] + 1;
								pred[v] = u;
							}
						}
					}
				}
			}

			// Vérification des cycles de poids négatifs
			for (int x = 0; x < _maze.GetLength(0); x++)
			{
				for (int y = 0; y < _maze.GetLength(1); y++)
				{
					var u = new CellCoordinates(x, y);
					if (dist[u] == int.MaxValue || _maze[u.X, u.Y] == Cell.Wall) continue;

					foreach (var direction in directions)
					{
						var v = new CellCoordinates(x + direction.Item1, y + direction.Item2);
						if (IsWithinBounds(v, _maze) && IsCorrectCell(v, _maze) && dist[u] + 1 < dist[v])
							throw new InvalidOperationException("Graph contains a negative weight cycle");
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
			if (path.Count == 1)
				return path.Pop();

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

		private static bool IsCorrectCell(CellCoordinates cell, Cell[,] maze) =>
			maze[cell.X, cell.Y] == Cell.Empty || maze[cell.X, cell.Y] == Cell.Start || maze[cell.X, cell.Y] == Cell.End;

		private static bool IsWithinBounds(CellCoordinates cell, Cell[,] maze) =>
			cell.X >= 0 && cell.X < maze.GetLength(0) && cell.Y >= 0 && cell.Y < maze.GetLength(1);
	}
}
