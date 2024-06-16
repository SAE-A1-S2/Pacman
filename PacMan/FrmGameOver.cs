namespace PacMan
{
	public partial class FrmGameOver : Form
	{
		private readonly frmGame frmGame;
		public FrmGameOver(frmGame FrmGame)
		{
			InitializeComponent();
			frmGame = FrmGame;
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
				frmGame.gameManager.Resume(); // Reprend la partie
				return true; // retourne ici pour indiquer que la touche est bien traitée
			}
			return base.ProcessCmdKey(ref msg, keyData); // retourne ici pour indiquer que la touche n'est pas traitée
		}

		// Rajoute un contour autour de la fenêtre
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			using Pen pen = new(Color.White, 1);
			e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
		}

		private void BtnNxtLvl_Click(object sender, EventArgs e)
		{
			Hide(); // Ferme la fenêtre
			frmGame.gameManager.NextLevel();
			frmGame.ReloadForm();
		}

		private void FrmPause_Load(object sender, EventArgs e)
		{
			CenterToParent(); // Centre la fenêtre par rapport à la fenêtre parente
		}
	}
}
