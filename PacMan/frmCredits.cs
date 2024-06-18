/*
GROUPE D-06
SAE 2.01
2023-2024

Résumé:
Ce fichier contient le code de la classe frmCredits qui est utilisée pour afficher les crédits du jeu. 
*/

namespace PacMan
{
	public partial class FrmCredits : FrmEntity
	{

		// Constructeur de la classe frmCredits
		public FrmCredits()
		{
			InitializeComponent();
		}

		// Correspond à l'événement KeyDown, permet de récupérer les touches appuyées par l'utilisateur
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				Hide(); // cache la fenêtre si la touche Escape est appuyée
				return true; // Indique que la touche est bien traitée
			}
			return base.ProcessCmdKey(ref msg, keyData); // Indique que la touche n'est pas traitée
		}

		// Événement de clic sur le bouton Retour
		private void btnRetour_Click(object sender, EventArgs e)
		{
			Hide(); // cache la fenêtre
		}
	}
}