﻿namespace PacMan
{
	public partial class FrmGameOver : FrmEntity // Classe du formulaire de fin de partie, hérite de FrmEntity (formulaire de base)
	{
		private readonly frmGame frmGame; // Référence au formulaire de jeu principal

		public FrmGameOver(frmGame FrmGame)
		{
			InitializeComponent();
			frmGame = FrmGame;
		}

		// Gestionnaire de l'événement de clic sur le bouton "Prochain niveau"
		private void BtnNxtLvl_Click(object sender, EventArgs e)
		{
			Hide();  // Masquer le formulaire de fin de partie

			// Passer au niveau suivant dans le gestionnaire de jeu
			frmGame.gameManager.NextLevel();

			// Recharger le formulaire de jeu (pour afficher le nouveau niveau)
			// Le rechargement est necessaire pour "deconnecter"  et puis "reconnecter" les données liées par le DataBinding
			// car le DataBinding est associer avec l'objet et non la classe ou le source de donnée en elle même
			// Pour passer au prochain niveau, on reinstancie la classe "LevelManager"
			// nous avons remarqué que si on "reinitialise" pas les DataBinding, les elements de Jeu ne sont pas reinitialisés
			frmGame.ReloadForm();
		}

		private async void btnQuit_Click(object sender, EventArgs e)
		{
			Close();

			await Task.Delay(2000); // Attendre 2 secondes
			frmGame.Close();
		}
	}
}
