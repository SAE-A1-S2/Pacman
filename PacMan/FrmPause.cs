using Engine;

namespace PacMan
{
	public partial class FrmPause : FrmEntity
	{
		private readonly GameManager gameManager;
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
				Hide(); // ferme la fenêtre si la touche Escape est appuyée
				gameManager.Resume(); // Reprend la partie
				return true; // retourne ici pour indiquer que la touche est bien traitée
			}
			return base.ProcessCmdKey(ref msg, keyData); // retourne ici pour indiquer que la touche n'est pas traitée
		}

		private void btnReprendre_Click(object sender, EventArgs e)
		{
			Hide(); // Ferme la fenêtre
			gameManager.Resume(); // Reprend la partie
		}

		private async void btnQuitSave_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.LastInserterID = gameManager.SaveSession(); // Sauvegarde la partie
			if (Properties.Settings.Default.LastInserterID != -1)
			{
				lblMsgDB.ForeColor = Color.Green;
				lblMsgDB.Text = "Partie sauvegardée !";
			}
			else
			{
				lblMsgDB.ForeColor = Color.Red;
				lblMsgDB.Text = "Erreur lors de la sauvegarde !";
			}
			Properties.Settings.Default.Save(); // Sauvegarde les paramètres
			await Task.Delay(2000);
			Application.Exit();
		}
	}
}
