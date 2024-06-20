using Engine.utils;

namespace Engine
{
	public static class Algorithms
	{

		/// <summary>
		/// Trouve la cellule cible (clé, kit de soin, torche) la plus proche du joueur dans le labyrinthe en utilisant l'algorithme A*.
		/// </summary>
		/// <param name="maze">Le labyrinthe représenté par un tableau 2D de cellules.</param>
		/// <returns>Les coordonnées de la cellule cible la plus proche, ou (-1, -1) si aucune cible n'est trouvée.</returns>
		public static CellCoordinates FindClosestCell(Cell[,] maze)
		{
			// Liste des types de cellules cibles à rechercher
			List<Cell> targets = [Cell.KEY, Cell.HEALTH_KIT, Cell.TORCH];

			// Position de départ du joueur
			CellCoordinates start = FindPlayer(maze);

			// File de priorité pour stocker les cellules à explorer, avec leur coût estimé comme priorité
			PriorityQueue<CellCoordinates, int> frontier = new();
			frontier.Enqueue(start, 0); // Ajoute la position de départ avec un coût de 0

			// Dictionnaire pour stocker le chemin parcouru jusqu'à chaque cellule
			Dictionary<CellCoordinates, CellCoordinates> cameFrom = [];

			// Dictionnaire pour stocker le coût réel pour atteindre chaque cellule depuis le départ
			Dictionary<CellCoordinates, int> costSoFar = [];

			cameFrom[start] = start; // Le point de départ vient de lui-même
			costSoFar[start] = 0;   // Coût pour atteindre le départ est 0

			// Boucle principale de l'algorithme A*
			while (frontier.Count > 0)
			{
				CellCoordinates current = frontier.Dequeue(); // Récupère la cellule avec la priorité la plus faible (coût estimé le plus bas)


				// Si la cellule actuelle est une cible, on a trouvé le chemin le plus court
				if (targets.Contains(maze[current.Row, current.Col]))
					return current;

				// Explore les cellules voisines de la cellule actuelle
				foreach (CellCoordinates next in Static.GetNeighbors(maze, current))
				{
					// Calcule le nouveau coût pour atteindre la cellule voisine (coût actuel + 1)
					int newCost = costSoFar[current] + 1;

					// Si la cellule voisine n'a pas été visitée ou si le nouveau coût est inférieur au coût précédent
					if (!costSoFar.TryGetValue(next, out int value) || newCost < value)
					{
						costSoFar[next] = newCost;             // Met à jour le coût pour atteindre la cellule voisine
						int priority = newCost + Static.Heuristic(next, start); // Calcule la priorité (coût + heuristique)
						frontier.Enqueue(next, priority);     // Ajoute la cellule voisine à la file de priorité avec sa nouvelle priorité
						cameFrom[next] = current;            // Met à jour le chemin pour indiquer que l'on atteint la cellule voisine depuis la cellule actuelle
					}
				}
			}
			return new CellCoordinates(-1, -1); // Aucune cible trouvée dans la limite de recherche
		}

		/// <summary>
		/// Calcule la position d'une entité plusieurs cases en avant dans une direction donnée, en tenant compte des limites du labyrinthe.
		/// </summary>
		/// <param name="maze">Le labyrinthe représenté sous forme de tableau 2D de cellules.</param>
		/// <param name="playerPosition">La position actuelle de l'entité (ligne, colonne).</param>
		/// <param name="direction">La direction dans laquelle l'entité se déplace.</param>
		/// <param name="stepsAhead">Le nombre de cases à avancer.</param>
		/// <returns>La nouvelle position de l'entité après avoir avancé de 'stepsAhead' cases dans la 'direction' donnée, ou la position d'origine si le déplacement sort des limites du labyrinthe.</returns>
		public static CellCoordinates CalculatePositionAhead(Cell[,] maze, CellCoordinates playerPosition, Direction direction, int stepsAhead)
		{
			// Copie les coordonnées actuelles de l'entité pour les modifier
			int newRow = playerPosition.Row;
			int newCol = playerPosition.Col;

			// Ajuste les coordonnées en fonction de la direction
			switch (direction)
			{
				case Direction.UP:    // Si la direction est vers le haut
					newRow -= stepsAhead; // Décrémente la ligne
					break;
				case Direction.DOWN:  // Si la direction est vers le bas
					newRow += stepsAhead; // Incrémente la ligne
					break;
				case Direction.LEFT:  // Si la direction est vers la gauche
					newCol -= stepsAhead; // Décrémente la colonne
					break;
				case Direction.RIGHT: // Si la direction est vers la droite
					newCol += stepsAhead; // Incrémente la colonne
					break;
				default:               // Si la direction est STOP ou invalide
					return playerPosition; // Ne bouge pas, retourne la position d'origine
			}

			// Vérifie si la nouvelle position est dans les limites du labyrinthe
			if (newRow >= 0 && newRow < maze.GetLength(0) && newCol >= 0 && newCol < maze.GetLength(1))
				return new CellCoordinates(newRow, newCol); // Retourne la nouvelle position valide
			else
				return playerPosition; // Retourne la position d'origine si le déplacement est hors limites
		}



		/// <summary>
		/// Recherche une cellule d'un type spécifique dans le labyrinthe, en partant d'une position cible et en explorant en spirale.
		/// </summary>
		/// <param name="maze">Le labyrinthe représenté par un tableau 2D de cellules.</param>
		/// <param name="target">La position de départ de la recherche.</param>
		/// <param name="cellType">Le type de cellule recherchée (par défaut, une cellule vide).</param>
		/// <param name="maxSearchDistance">La distance maximale de recherche (par défaut, 1/4 de la plus grande dimension du labyrinthe).</param>
		/// <returns>Les coordonnées de la première cellule trouvée correspondant au type spécifié, ou une cellule vide aléatoire si aucune n'est trouvée dans la distance de recherche maximale.</returns>
		public static CellCoordinates FindCell(Cell[,] maze, CellCoordinates target, Cell cellType = Cell.EMPTY, int maxSearchDistance = 0)
		{
			// Si la distance de recherche maximale n'est pas spécifiée ou est invalide, la définir par défaut à 1/4 de la plus grande dimension du labyrinthe
			if (maxSearchDistance <= 0)
				maxSearchDistance = Math.Max(maze.GetLength(0), maze.GetLength(1)) / 4;

			int currentDistance = 0;      // Distance actuelle par rapport à la position cible
			int dr = 0, dc = 1;          // Direction de recherche initiale (vers la droite)
			CellCoordinates current = target; // Cellule actuelle à examiner

			// Boucle de recherche en spirale
			while (currentDistance <= maxSearchDistance)
			{
				// Si la cellule actuelle est dans les limites du labyrinthe et correspond au type recherché, la retourner
				if (Entity.IsInBounds(current, maze) && maze[current.Row, current.Col] == cellType)
					return current;

				// Déplacer vers la cellule suivante dans la direction actuelle
				current.Row += dr;
				current.Col += dc;

				// Si on sort des limites, si la cellule n'est pas vide, ou si on a dépassé la distance de recherche, changer de direction et augmenter la distance
				if (!Entity.IsInBounds(current, maze) || maze[current.Row, current.Col] != Cell.EMPTY || Math.Abs(current.Row - target.Row) + Math.Abs(current.Col - target.Col) > currentDistance)
				{
					(dr, dc) = (-dc, dr); // Rotation de 90 degrés
					currentDistance++;    // Incrémente de la distance de recherche
				}
			}

			// Si aucune cellule appropriée n'est trouvée, retourner une cellule vide aléatoire
			return FindEmptyCellNearCorner(maze, target);
		}

		/// <summary>
		/// Trouve une cellule vide proche d'un coin donné dans le labyrinthe.
		/// </summary>
		/// <param name="maze">Le labyrinthe représenté par un tableau 2D de cellules.</param>
		/// <param name="corner">Les coordonnées du coin à partir duquel chercher.</param>
		/// <param name="maxAttempts">Le nombre maximum de tentatives pour trouver une cellule vide (optionnel, par défaut 100).</param>
		/// <returns>Les coordonnées d'une cellule vide proche du coin, ou une cellule vide aléatoire si aucune n'est trouvée.</returns>
		private static CellCoordinates FindEmptyCellNearCorner(Cell[,] maze, CellCoordinates corner, int maxAttempts = 100)
		{
			Random random = new();
			int attempts = 0;
			CellCoordinates cell;
			while (attempts < maxAttempts)
			{
				// Calcule une distance maximale de recherche à partir du coin (augmente à chaque tentative)
				int searchDistance = attempts / 10 + 1;

				// Génère des coordonnées aléatoires dans une zone autour du coin
				int row = Math.Clamp(corner.Row + random.Next(-searchDistance, searchDistance + 1), 0, maze.GetLength(0) - 1);
				int col = Math.Clamp(corner.Col + random.Next(-searchDistance, searchDistance + 1), 0, maze.GetLength(1) - 1);
				cell = new CellCoordinates(row, col);

				// Vérifie si la cellule est vide
				if (maze[cell.Row, cell.Col] == Cell.EMPTY)
					return cell; // Trouvé !

				attempts++;
			}

			do
			{
				cell = new CellCoordinates(random.Next(maze.GetLength(0)), random.Next(maze.GetLength(1)));
			} while (maze[cell.Row, cell.Col] != Cell.EMPTY);

			return cell; // Retourne les coordonnées de la cellule vide trouvée
		}



		public static CellCoordinates FindPlayer(Cell[,] maze)
		{
			CellCoordinates pos = new(0, 0);
			for (int x = 0; x < maze.GetLength(0); x++)
				for (int y = 0; y < maze.GetLength(1); y++)
					if (maze[x, y] == Cell.JOHN)
						pos = new CellCoordinates(x, y);
			return pos;
		}

		/// <summary>
		/// Trouve le chemin le plus court entre deux cellules dans le labyrinthe en utilisant l'algorithme de Dijkstra.
		/// </summary>
		/// <param name="src">La cellule de départ (source).</param>
		/// <param name="dst">La cellule d'arrivée (destination).</param>
		/// <param name="_maze">Le labyrinthe représenté par un tableau 2D de cellules.</param>
		/// <returns>La cellule suivante à atteindre pour suivre le chemin le plus court, ou la cellule de départ si aucun chemin n'est trouvé.</returns>
		public static CellCoordinates FindDijkstra(CellCoordinates src, CellCoordinates dst, Cell[,] _maze)
		{
			// Tableau des directions possibles (haut, bas, gauche, droite)
			var directions = new (int, int)[] { (1, 0), (0, 1), (-1, 0), (0, -1) };

			// Dictionnaire pour stocker la distance minimale connue pour atteindre chaque cellule
			var distance = new Dictionary<CellCoordinates, int>();

			// Dictionnaire pour stocker la cellule précédente dans le chemin le plus court vers chaque cellule
			var prev = new Dictionary<CellCoordinates, CellCoordinates?>();

			// File de priorité pour stocker les cellules à explorer, triées par distance croissante
			var priorityQueue = new SortedDictionary<int, Queue<CellCoordinates>>();

			// Initialisation : la distance pour atteindre la cellule de départ est 0
			distance[src] = 0;
			Enqueue(priorityQueue, 0, src); // Ajoute la cellule de départ à la file de priorité

			// Boucle principale de Dijkstra
			while (priorityQueue.Count > 0)
			{
				var current = Dequeue(priorityQueue); // Récupère la cellule la plus proche non explorée

				// Si la cellule actuelle est la destination, on a trouvé le chemin
				if (current.Row == dst.Row && current.Col == dst.Col)
					return ReconstructPath(prev, dst, src); // Reconstruit et retourne le chemin complet

				// Explore les voisins de la cellule actuelle
				foreach (var dir in directions)
				{
					var neighbor = new CellCoordinates(current.Row + dir.Item1, current.Col + dir.Item2); // Calcule les coordonnées du voisin

					// Vérifie si le voisin est dans les limites du labyrinthe et n'est pas un mur
					if (Entity.IsInBounds(neighbor, _maze) && _maze[neighbor.Row, neighbor.Col] != Cell.WALL)
					{
						int newDist = distance[current] + 1; // Calcule la nouvelle distance pour atteindre le voisin

						// Si le voisin n'a pas encore été visité ou si la nouvelle distance est plus courte, met à jour les informations
						if (!distance.TryGetValue(neighbor, out int value) || newDist < value)
						{
							distance[neighbor] = newDist;  // Met à jour la distance minimale
							prev[neighbor] = current;      // Met à jour le prédécesseur dans le chemin
							Enqueue(priorityQueue, newDist, neighbor); // Ajoute le voisin à la file de priorité
						}
					}
				}
			}

			return src; // Si aucun chemin n'a été trouvé, retourne la cellule de départ
		}


		/// <summary>
		/// Trouve le chemin le plus court entre deux cellules dans le labyrinthe en utilisant l'algorithme de Bellman-Ford.
		/// </summary>
		/// <param name="src">Coordonnées de la cellule de départ.</param>
		/// <param name="dst">Coordonnées de la cellule d'arrivée.</param>
		/// <param name="_maze">Le labyrinthe représenté sous forme de tableau 2D de cellules.</param>
		/// <returns>Coordonnées de la prochaine cellule à atteindre pour suivre le chemin le plus court, ou la cellule de départ si aucun chemin n'est trouvé.</returns>
		public static CellCoordinates FindBellmanFord(CellCoordinates src, CellCoordinates dst, Cell[,] _maze)
		{
			// Dictionnaire pour stocker la distance minimale estimée pour atteindre chaque cellule depuis la source
			var dist = new Dictionary<CellCoordinates, int>();

			// Dictionnaire pour stocker le prédécesseur de chaque cellule dans le chemin le plus court
			var pred = new Dictionary<CellCoordinates, CellCoordinates?>();

			// Tableau contenant les déplacements possibles (haut, bas, gauche, droite)
			var directions = new[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

			// Initialisation des distances et des prédécesseurs
			for (int x = 0; x < _maze.GetLength(0); x++)
			{
				for (int y = 0; y < _maze.GetLength(1); y++)
				{
					// Initialise la distance de chaque cellule à l'infini (représenté par int.MaxValue)
					dist[new CellCoordinates(x, y)] = int.MaxValue;

					// Initialise le prédécesseur de chaque cellule à null (indiquant qu'aucun chemin n'a encore été trouvé)
					pred[new CellCoordinates(x, y)] = null;
				}
			}
			dist[src] = 0; // La distance pour atteindre la source est 0

			// Relaxation des arêtes (itérations de Bellman-Ford)
			// On répète V-1 fois (V étant le nombre de sommets), car dans le pire des cas, le chemin le plus court peut avoir V-1 arêtes
			for (int i = 0; i < _maze.GetLength(0) * _maze.GetLength(1) - 1; i++)
			{
				for (int x = 0; x < _maze.GetLength(0); x++)
				{
					for (int y = 0; y < _maze.GetLength(1); y++)
					{
						// Coordonnées de la cellule actuelle
						var u = new CellCoordinates(x, y);

						// Ignore les cellules non atteignables ou qui sont des murs
						if (dist[u] == int.MaxValue || _maze[u.Row, u.Col] == Cell.WALL) continue;

						// Explore les voisins de la cellule actuelle
						foreach (var direction in directions)
						{
							// Coordonnées du voisin
							var v = new CellCoordinates(x + direction.Item1, y + direction.Item2);

							// Vérifie si le voisin est valide (dans les limites du labyrinthe et pas un mur) 
							// et si la distance en passant par u est plus courte que la distance connue pour v
							if (Entity.IsInBounds(v, _maze) && (_maze[v.Row, v.Col] != Cell.WALL) && dist[u] + 1 < dist[v])
							{
								dist[v] = dist[u] + 1; // Met à jour la distance minimale pour atteindre v
								pred[v] = u;           // Met à jour le prédécesseur de v dans le chemin le plus court
							}
						}
					}
				}
			}

			// Reconstruction du chemin à partir des prédécesseurs
			return ReconstructPath(pred, dst, src);
		}

		/// <summary>
		/// Reconstruit le chemin le plus court entre deux cellules à partir du dictionnaire des prédécesseurs.
		/// </summary>
		/// <param name="pred">Dictionnaire associant chaque cellule à son prédécesseur dans le chemin le plus court.</param>
		/// <param name="dst">La cellule de destination.</param>
		/// <param name="src">La cellule de départ.</param>
		/// <returns>La cellule suivante à atteindre pour suivre le chemin le plus court, ou la cellule de départ si aucun chemin n'est trouvé.</returns>
		private static CellCoordinates ReconstructPath(Dictionary<CellCoordinates, CellCoordinates?> pred, CellCoordinates dst, CellCoordinates src)
		{
			var current = dst;  // Commence à partir de la destination
			var path = new Stack<CellCoordinates>(); // Pile pour stocker le chemin en sens inverse

			while (!current.Equals(src)) // Tant qu'on n'a pas atteint la source
			{
				path.Push(current);         // Ajoute la cellule actuelle au chemin
											// Obtient le prédécesseur de la cellule actuelle
				if (pred.TryGetValue(current, out CellCoordinates? value) && value.HasValue)
					current = value.Value;  // Passe à la cellule précédente
				else
					return src;             // Si aucun prédécesseur n'est trouvé, aucun chemin n'existe, retourne la source
			}

			return path.Pop(); // Dépile la première cellule du chemin (la prochaine étape)
		}


		/// <summary>
		/// Ajoute une cellule à la file de priorité triée par coût.
		/// </summary>
		/// <param name="pq">La file de priorité (SortedDictionary) où la cellule sera ajoutée.</param>
		/// <param name="cost">Le coût associé à la cellule.</param>
		/// <param name="cell">Les coordonnées de la cellule.</param>
		private static void Enqueue(SortedDictionary<int, Queue<CellCoordinates>> pq, int cost, CellCoordinates cell)
		{
			// Si la clé de coût n'existe pas encore dans le dictionnaire, créez une nouvelle file d'attente pour cette clé.
			if (!pq.ContainsKey(cost))
				pq[cost] = new Queue<CellCoordinates>();

			// Ajoute la cellule à la file d'attente correspondant à son coût.
			pq[cost].Enqueue(cell);
		}


		/// <summary>
		/// Retire et retourne la cellule avec le coût le plus bas de la file de priorité.
		/// </summary>
		/// <param name="pq">La file de priorité (SortedDictionary) d'où la cellule sera retirée.</param>
		/// <returns>Les coordonnées de la cellule avec le coût le plus bas.</returns>
		private static CellCoordinates Dequeue(SortedDictionary<int, Queue<CellCoordinates>> pq)
		{
			var firstKey = pq.Keys.First();    // Obtient la première clé (coût le plus bas) du dictionnaire trié
			var queue = pq[firstKey];         // Obtient la file d'attente associée à ce coût
			var cell = queue.Dequeue();      // Retire la première cellule de la file d'attente

			if (queue.Count == 0)             // Si la file d'attente est vide après la suppression
				pq.Remove(firstKey);           // Supprime la clé du dictionnaire

			return cell;                       // Retourne la cellule retirée
		}

	}
}
