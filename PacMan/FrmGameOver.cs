namespace PacMan
{
	public partial class FrmGameOver : FrmEntity // Classe du formulaire de fin de partie, hérite de FrmEntity (formulaire de base)
	{
		private readonly frmGame frmGame; // Référence au formulaire de jeu principal
		public bool Result { get; private set; } = false;

		public FrmGameOver(frmGame FrmGame)
		{
			InitializeComponent();
			frmGame = FrmGame;
		}

		// Gestionnaire de l'événement de clic sur le bouton "Prochain niveau"
		private void BtnNxtLvl_Click(object sender, EventArgs e)
		{
			Hide();  // Masquer le formulaire de fin de partie

			frmGame.stopWatch.Stop(); // Arreter le chronomètre
			frmGame.timeElapsed = frmGame.stopWatch.Elapsed; // Mettre le temps de jeu dans la variable timeElapsed
			frmGame.gameManager.SaveLevelData(Properties.Settings.Default.PlayerUID, frmGame.timeElapsed.Minutes); // Sauvegarder les données du niveau (temps, score, etc.

			// Passer au niveau suivant dans le gestionnaire de jeu
			frmGame.gameManager.NextLevel();

			// Recharger le formulaire de jeu (pour afficher le nouveau niveau)
			// Le rechargement est necessaire pour "deconnecter"  et puis "reconnecter" les données liées par le DataBinding
			// car le DataBinding est associer avec l'objet et non la classe ou le source de donnée en elle même
			// Pour passer au prochain niveau, on reinstancie la classe "LevelManager"
			// nous avons remarqué que si on "reinitialise" pas les DataBinding, les elements de Jeu ne sont pas reinitialisés
			frmGame.ReloadForm();
			frmGame.gameManager.Resume(); // Reprendre le jeu
		}

		private void btnQuit_Click(object sender, EventArgs e)
		{
			Hide();
			frmGame.stopWatch.Stop(); // Arreter le chronomètre
			frmGame.timeElapsed = frmGame.stopWatch.Elapsed; // Mettre le temps de jeu dans la variable timeElapsed
			frmGame.gameManager.SaveLevelData(Properties.Settings.Default.PlayerUID, frmGame.timeElapsed.Minutes); // Sauvegarder les données du niveau (temps, score, etc.

			Result = true; // Indiquer que le joueur souaite quitter le jeu
		}
	}
}
