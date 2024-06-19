using Engine;

namespace PacMan
{
	public partial class FrmPause : FrmEntity // Classe du formulaire de pause, hérite de FrmEntity (formulaire de base)
	{
		private readonly GameManager gameManager; // Référence au gestionnaire de jeu (GameManager)

		public FrmPause(GameManager game)
		{
			InitializeComponent();
			gameManager = game;
		}

		// Correspond à l'evenement KeyDown, permet de récuperer les touches appuyées par l'utilisateur
		// dans notre cas, le jeu ne couvre pas l'entièreté de l'écran, donc il se peut que d'autres applications ouvertes 
		//récupèrent les touches appuyées avant notre application.
		// plus d'infos: https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.processcmdkey?view=windowsdesktop-8.0
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				// ferme la fenêtre si la touche Escape est appuyée
				Hide();

				// Reprend la partie
				gameManager.Resume();

				// retourne ici pour indiquer que la touche est bien traitée
				return true;
			}

			// retourne ici pour indiquer que la touche n'est pas traitée
			return base.ProcessCmdKey(ref msg, keyData);
		}

		// Gestionnaire de l'événement de clic sur le bouton "Reprendre"
		private void btnReprendre_Click(object sender, EventArgs e)
		{
			Hide(); // Ferme la fenêtre
			gameManager.Resume(); // Reprend la partie
		}

		// Gestionnaire de l'événement de clic sur le bouton "Quitter et sauvegarder"
		private async void btnQuitSave_Click(object sender, EventArgs e)
		{
			// Sauvegarde de la session de jeu et récupération de l'ID de la sauvegarde
			Properties.Settings.Default.LastInserterID = gameManager.SaveSession(Properties.Settings.Default.PlayerUID);
			if (Properties.Settings.Default.LastInserterID != -1) // Succès
			{
				lblMsgDB.ForeColor = Color.Green; // texte en vert
				lblMsgDB.Text = "Partie sauvegardée !";
			}
			else // Échec
			{
				lblMsgDB.ForeColor = Color.Red; // texte en rouge
				lblMsgDB.Text = "Erreur lors de la sauvegarde !";
			}
			Properties.Settings.Default.Save(); // Sauvegarde des paramètres (y compris l'ID de la dernière session sauvegardée)
			await Task.Delay(2000); // Attente de 2 secondes pour afficher le message de sauvegarde
			Application.Exit(); // Quitter l'application
		}
	}
}
