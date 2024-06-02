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

Résumé:
Ce fichier contient le code de la classe frmCredits qui est utilisée pour afficher les crédits du jeu. 
Il inclut des fonctionnalités pour gérer les événements de la fenêtre et permettre le déplacement de la fenêtre par glisser-déposer.
*/

namespace PacMan
{
	public partial class FrmCredits : Form
	{
		// Permet de savoir si la fenêtre est en cours de déplacement
		private bool isDragging = false;
		// Permet de sauvegarder la position de la fenêtre
		private Point lastCursorPosition;

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

		// Rajoute un contour autour de la fenêtre
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			using Pen pen = new(Color.White, 1);
			e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
		}

		// Événement de clic sur le bouton Retour
		private void btnRetour_Click(object sender, EventArgs e)
		{
			Hide(); // cache la fenêtre
		}

		// Événement de clic de la souris sur la fenêtre
		private void Credits_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isDragging = true; // Active le mode de déplacement de la fenêtre
				lastCursorPosition = PointToScreen(e.Location); // Enregistre la position actuelle du curseur
			}
		}

		// Événement de déplacement de la souris
		private void Credits_MouseMove(object sender, MouseEventArgs e)
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
		private void Credits_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				isDragging = false; // Désactive le mode de déplacement de la fenêtre
		}

		// Événement appelé lorsque la fenêtre est affichée
		private void frmCredits_Shown(object sender, EventArgs e)
		{
			Activate(); // Donne le focus à la fenêtre
		}

		private void FrmCredits_Load(object sender, EventArgs e)
		{
			CenterToParent(); // Centre la fenêtre par rapport à la fenêtre parente
		}
	}
}