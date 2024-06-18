

namespace PacMan
{
	// Classe de base pour les formulaires personnalisés
	public partial class FrmEntity : Form
	{

		protected bool isDragging = false; // Indicateur : la fenêtre est-elle en train d'être déplacée ?
		protected Point lastCursorPosition; // Stocke la dernière position du curseur lors du déplacement
		public FrmEntity()
		{
			InitializeComponent();
		}

		// Gestionnaire de l'événement de chargement du formulaire
		protected void Frm_Load(object sender, EventArgs e)
		{
			// Centre le formulaire par rapport à sa fenêtre parente
			CenterToParent();
		}

		// ----- Gestion du déplacement de la fenêtre -----

		// Gestionnaire : clic de souris enfoncé
		protected void Frm_MouseDown(object sender, MouseEventArgs e)
		{
			// Si c'est le bouton gauche de la souris
			if (e.Button == MouseButtons.Left)
			{
				// Active le mode de déplacement de la fenêtre
				isDragging = true;

				// Enregistre la position actuelle du curseur
				lastCursorPosition = PointToScreen(e.Location);
			}
		}

		// Gestionnaire : déplacement de la souris
		protected void Frm_MouseMove(object sender, MouseEventArgs e)
		{
			// Si le mode de déplacement est actif
			if (isDragging)
			{
				// Position actuelle du curseur
				Point currentCursorPosition = PointToScreen(e.Location);

				// Calculer le nouveau décalage du formulaire en fonction du mouvement du curseur
				Location = new Point(
									   Location.X + (currentCursorPosition.X - lastCursorPosition.X),
														  Location.Y + (currentCursorPosition.Y - lastCursorPosition.Y));

				// Mettre à jour la dernière position du curseur
				lastCursorPosition = currentCursorPosition;
			}
		}

		// Gestionnaire : clic de souris relâché
		protected void Frm_MouseUp(object sender, MouseEventArgs e)
		{
			// Si c'était le bouton gauche
			if (e.Button == MouseButtons.Left)
				isDragging = false;// Désactive le mode de déplacement de la fenêtre
		}

		// ----- Ajout d'un contour autour de la fenêtre -----

		// Surcharge de la méthode de dessin du formulaire
		protected override void OnPaint(PaintEventArgs e)
		{
			// Appeler la méthode de dessin de base (pour dessiner les éléments standards)
			base.OnPaint(e);

			// Dessiner un rectangle blanc (contour) sur le bord du formulaire
			using Pen pen = new(Color.White, 1);
			e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
		}

		private void Frm_Shown(object sender, EventArgs e)
		{
			Activate(); // Donne le focus à la fenêtre	
		}
	}
}
