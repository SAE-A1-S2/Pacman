/*
- ABASS Hammed
- AURIGNAC Arthur
- DOHER Alexis
- GODET Adrien
- MAS Cédric
- NAHARRO Guerby

GROUPE D-06
SAE 2.01
2023-2024

Résumé:
Ce fichier contient le code de la classe frmStats qui est utilisée pour afficher les statistiques de notre jeu. 
Il inclut des fonctionnalités pour afficher les scores des joueurs, gérer les événements de la fenêtre, et permettre le déplacement de la fenêtre par glisser-déposer.
*/

using Engine;

namespace PacMan
{
	public partial class FrmStats : FrmEntity
	{
		public FrmStats()
		{
			InitializeComponent();
		}

		// Correspond à l'evenement KeyDown, permet de récuperer les touches appuyées par l'utilisateur
		// dans notre cas, le jeu ne couvre pas l'entièreté de l'écran, donc il se peut que d'autres applications ouvertes 
		//récupèrent les touches appuyées avant notre application.
		// plus d'infos: https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.processcmdkey?view=windowsdesktop-8.0
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				Hide(); // ferme la fenêtre si la touche Escape est appuyée
				return true; // retourne ici pour indiquer que la touche est bien traitée
			}
			return base.ProcessCmdKey(ref msg, keyData); // retourne ici pour indiquer que la touche n'est pas traitée
		}

		// méthode appélée lorsque l'utilisateur clique sur le bouton "Retour"
		private void btnRetour_Click(object sender, EventArgs e)
		{
			Hide(); // ferme la fenêtre
		}

		// Evénement appelé lors du chargement de la fenêtre
		private void frmStats_Load(object sender, EventArgs e)
		{
			CenterToParent(); // Centre la fenêtre par rapport à la fenêtre parente
			try
			{
				// Chargement des scores
				FileManager fileManager = new();
				List<PlayerData> allData = fileManager.LoadGameData();
				// Affiche les statistiques des joueurs
				lblHighScore.Text = allData.Max(x => x.Score).ToString();
				lblTScoreC.Text = allData.Sum(x => x.Score).ToString();
				lblLowestScore.Text = allData.Min(x => x.Score).ToString();
				lblAvgScore.Text = allData.Average(x => x.Score).ToString();
				int SumMinutes = allData.Sum(x => x.TimeSpentInMinutes);
				lblHoursSpent.Text = SumMinutes / 60 + "h " + SumMinutes % 60 + "m";
			}
			catch (InvalidOperationException)
			{
				// vous vous demandez probablement pourquoi nous ecrivons 0.ToString() au lieu de "0"
				// voila pourquoi: https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1303
				// le programme fonctionnera correctement, mais les bonnes pratique

				// Gère les cas où il n'y a pas de données disponibles
				lblHighScore.Text = 0.ToString();
				lblTScoreC.Text = 0.ToString();
				lblLowestScore.Text = 0.ToString();
				lblAvgScore.Text = 0.ToString();
				lblHoursSpent.Text = 0.ToString();
			}
		}
	}
}