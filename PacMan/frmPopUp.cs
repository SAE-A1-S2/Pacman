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

Les commentaires devient répétitif, il se passe pratiquement la même chose que dans le fichier frmStats.cs, frmCredits.cs

Résumé:
Ce fichier contient le code de la classe frmPopUp qui est utilisée pour afficher une fenêtre contextuelle dans notre jeu. 
Il inclut des fonctionnalités pour gérer les événements de la fenêtre, permettre le déplacement de la fenêtre par glisser-déposer, et enregistrer les préférences de l'utilisateur.
*/

namespace PacMan
{
	public partial class FrmPopUp : FrmEntity
	{
		// Constructeur de la classe frmPopUp
		public FrmPopUp()
		{
			InitializeComponent();
		}

		// Correspond à l'événement KeyDown, permet de récupérer les touches appuyées par l'utilisateur
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				Close(); // Ferme la fenêtre si la touche Escape est appuyée
				return true; // Indique que la touche est bien traitée
			}
			return base.ProcessCmdKey(ref msg, keyData); // Indique que la touche n'est pas traitée
		}

		// Événement de clic sur le bouton OK de la fenêtre popup
		private void btnOKPopup_Click(object sender, EventArgs e)
		{
			// Si la case Popup est cochee, le popup ne sera pas affiché au prochain lancement
			if (chkPopup.Checked)
			{
				Properties.Settings.Default.ShowDialogOnLaunch = false; // Enregistre la préférence de l'utilisateur
				Properties.Settings.Default.Save(); // Sauvegarde les paramètres
			}
			Close(); // Ferme la fenêtre
		}
	}
}