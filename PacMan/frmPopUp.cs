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
	public partial class FrmPopUp : Form
	{
		// Permet de savoir si la fenêtre est en cours de déplacement
		private bool isDragging = false;
		// Permet de sauvegarder la position de la fenêtre
		private Point lastCursorPosition;

		// Constructeur de la classe frmPopUp
		public FrmPopUp()
		{
			InitializeComponent();
		}

		// Rajoute un contour autour de la fenêtre
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			using Pen pen = new(Color.White, 1);
			e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
		}

		// Événement de clic de la souris sur la fenêtre
		private void PopUpForm_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isDragging = true; // Active le mode de déplacement de la fenêtre
				lastCursorPosition = PointToScreen(e.Location); // Enregistre la position actuelle du curseur
			}
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

		// Événement de déplacement de la souris
		private void PopUpForm_MouseMove(object sender, MouseEventArgs e)
		{
			if (isDragging)
			{
				Point currentCursorPosition = PointToScreen(e.Location);
				Location = new Point(
					Location.X + (currentCursorPosition.X - lastCursorPosition.X),
					Location.Y + (currentCursorPosition.Y - lastCursorPosition.Y)); // Déplace la fenêtre
				lastCursorPosition = currentCursorPosition; // Met à jour la dernière position du curseur
			}
		}

		// Événement de relâchement de la souris
		private void PopUpForm_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				isDragging = false; // Désactive le mode de déplacement de la fenêtre
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

		// Événement appelé lorsque la fenêtre est affichée
		private void frmPopUp_Shown(object sender, EventArgs e)
		{
			Activate(); // Donne le focus à la fenêtre
		}
	}
}