

using System.Diagnostics;
using Engine;

namespace PacMan
{
	public partial class FrmPause : Form
	{
		private bool isDragging = false; // Indique si la fenêtre est en cours de déplacement
		private Point lastCursorPosition; // Permet de sauvegarder la position de la souris
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

		// Rajoute un contour autour de la fenêtre
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			using Pen pen = new(Color.White, 1);
			e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
		}


		// Événement de clic de la souris sur la fenêtre
		private void FrmPause_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isDragging = true; // Active le mode de déplacement de la fenêtre
				lastCursorPosition = PointToScreen(e.Location); // Enregistre la position actuelle du curseur
			}
		}

		// Événement de relachement de la souris
		private void FrmPause_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				isDragging = false;// Désactive le mode de déplacement de la fenêtre
		}

		// Événement de déplacement de la souris
		private void FrmPause_MouseMove(object sender, MouseEventArgs e)
		{
			if (isDragging)
			{
				Point currentCursorPosition = PointToScreen(e.Location);
				Location = new Point(
									   Location.X + (currentCursorPosition.X - lastCursorPosition.X),
														  Location.Y + (currentCursorPosition.Y - lastCursorPosition.Y));
				lastCursorPosition = currentCursorPosition;
			}
		}

		private void btnReprendre_Click(object sender, EventArgs e)
		{
			Hide(); // Ferme la fenêtre
			gameManager.Resume(); // Reprend la partie
		}

		private void FrmPause_Load(object sender, EventArgs e)
		{
			CenterToParent(); // Centre la fenêtre par rapport à la fenêtre parente
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
			Hide(); // Ferme la fenêtre
			gameManager.Resume(); // Reprend la partie
		}
	}
}
