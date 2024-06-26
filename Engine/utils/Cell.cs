/*
GROUPE D-06
SAE 2.01
2023-2024

Résumé:
Ce fichier contient l'énumération Cell qui définit les différents types de cellules pouvant être présentes dans le labyrinthe du jeu. 
Chaque type de cellule est associé à une valeur entière unique.
*/

namespace Engine;
public enum Cell
{
	EMPTY = 0,
	WALL,
	START,
	END,
	JOHN,
	COIN,
	KEY,
	WINSTON,
	CAIN,
	VIGGO,
	MARQUIS,
	HEALTH_KIT,
	TORCH,
}