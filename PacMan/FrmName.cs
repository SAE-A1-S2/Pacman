namespace PacMan
{
	public partial class FrmName : Form
	{
		// Propriété publique de la classe pour stocker le résultat de la fenêtre (true si le nom est valide, false sinon)
		public bool Result { get; private set; }
		public string PlayerName { get; private set; }

		// Constructeur de la classe
		public FrmName()
		{
			InitializeComponent();
			PlayerName = string.Empty; // Initialise la propriété Name avec une chaîne vide
		}

		private void TxtPlyName_TextChanged(object sender, EventArgs e)
		{
			BtnCtn.Enabled = !string.IsNullOrWhiteSpace(TxtPlyName.Text); // Active le bouton "Continuer" si le champ de texte n'est pas vide
		}

		// Surcharge de la méthode OnPaint pour ajouter un contour blanc autour de la fenêtre
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e); // Appelle la méthode OnPaint de la classe de base

			// Dessine un rectangle blanc avec une largeur de 1 pixel autour de la fenêtre
			using Pen pen = new(Color.White, 1);
			e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
		}

		// Méthode appelée lors de l'appui sur une touche 
		private void TxtName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) // Si la touche Enter est appuyée
				BtnCtn_Click(sender, e);
		}

		//Méthode appelée au chargement de la fenêtre
		private void FrmName_Load(object sender, EventArgs e)
		{
			CenterToParent(); // Centre la fenêtre par rapport à la fenêtre parente
		}

		//Méthode appelée lors du clic sur le bouton "Annuler"
		private void BtnCancel_Click(object sender, EventArgs e)
		{
			Result = false; // Indique que l'utilisateur a annule la saisie du nom
			Close(); // Ferme la fenêtre
		}

		//Méthode appelée lors du clic sur le bouton "Continuer"
		private void BtnCtn_Click(object sender, EventArgs e)
		{
			if (chkName.Checked)
			{
				Properties.Settings.Default.PlayerName = TxtPlyName.Text; // Enregistre le nom du joueur dans les paramètres de l'application
				Properties.Settings.Default.Save(); // Sauvegarde les paramètres
			}

			Result = true; // Indique que l'utilisateur a valide la saisie du nom
			Close(); // Ferme la fenêtre
		}
	}
}