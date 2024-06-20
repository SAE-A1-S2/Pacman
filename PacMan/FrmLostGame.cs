namespace PacMan
{
	public partial class FrmLostGame : FrmEntity // Classe du formulaire de fin de partie (en cas de défaite), dérive de FrmEntity (formulaire de base)
	{
		private readonly frmGame GameForm; // Référence au formulaire principal du jeu

		public bool Result { get; private set; } = false; // Indique si le joueur souhaite quitter le jeu


		public FrmLostGame(frmGame gameForm)
		{
			InitializeComponent();
			GameForm = gameForm;
		}

		// Gestionnaire de l'événement de clic sur le bouton "Relancer la partie"
		private void BtnReLvl_Click(object sender, EventArgs e)
		{
			Hide(); // Masquer le formulaire de fin de partie

			// Réinitialiser le niveau actuel en utilisant le gestionnaire de niveau du jeu
			GameForm.gameManager.LevelManager.ResetLevel(GameForm.gameManager.GameMode, Properties.Settings.Default.PlayerUID);

			// Recharger le formulaire de jeu pour afficher le niveau réinitialisé
			GameForm.ReloadForm();

		}

		// Gestionnaire de l'événement de clic sur le bouton "Quitter"
		private void BtnQuit_Click(object sender, EventArgs e)
		{
			Hide(); // Masquer le formulaire de fin de partie

			// Indiquer que le joueur souhaite quitter le jeu
			Result = true;
		}

		// Gestionnaire de l'événement de chargement du formulaire
		private void FrmLostGame_Load(object sender, EventArgs e)
		{
			// Centrer le formulaire par rapport à sa fenêtre parente
			CenterToParent();

			// Afficher le score actuel du joueur dans le label lblScore
			lblScore.Text = GameForm.gameManager.LevelManager.Score.ToString();
		}
	}
}
