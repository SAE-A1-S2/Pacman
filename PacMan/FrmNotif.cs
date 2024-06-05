namespace PacMan
{
	public partial class FrmNotif : Form
	{
		// Propriété publique pour stocker le résultat de la boîte de dialogue (Oui/Non)
		public bool Result { get; private set; } 

		// Références vers les fenêtres de jeu et d'accueil 
		private frmGame? FrmGame; // Peut être null si la notification vient de l'accueil
		private FrmHome? FrmHome; // Peut être null si la notification vient du jeu

		// Constructeur pour une notification venant de FrmHome
		public FrmNotif(FrmHome frmHome)
		{
			InitializeComponent(); 
			FrmHome = frmHome;     // Stocke la référence à FrmHome
		}

		// Constructeur pour une notification venant de frmGame
		public FrmNotif(frmGame frmGame)
		{
			InitializeComponent();
			FrmGame = frmGame;
		}

		// Surcharge de la méthode OnPaint pour ajouter un contour blanc autour de la fenêtre
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);        // Appelle la méthode OnPaint de la classe de base

			// Dessine un rectangle blanc avec une largeur de 1 pixel
			using Pen pen = new(Color.White, 1);
			e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
		}

		// Méthode appelée au chargement de la fenêtre
		private void FrmNotif_Load(object sender, EventArgs e)
		{
			CenterToParent(); // Centre la fenêtre par rapport à sa fenêtre parent

			// Affiche un message différent selon que la notification vient du jeu ou de l'accueil
			if (FrmGame != null)
			{
				lblMsg.Text = "Voulez-vous vraiment quitter la partie ?";
				lblDetails.Text = "Toutes les données non sauvegardées seront perdues.";
			}
			else if (FrmHome != null)
			{
				lblMsg.Text = "Voulez-vous vraiment quitter le jeu ?";
				lblDetails.Text = ""; // Pas de détails supplémentaires pour quitter le jeu
			}
		}

		// Gestionnaire d'événement pour le clic sur le bouton "Oui"
		private void btnOui_Click(object sender, EventArgs e)
		{
			Result = true;  // Indique que l'utilisateur a choisi "Oui"
			Close();       // Ferme la fenêtre de notification
		}

		// Gestionnaire d'événement pour le clic sur le bouton "Non"
		private void btnNon_Click(object sender, EventArgs e)
		{
			Result = false; // Indique que l'utilisateur a choisi "Non"
			Close();       // Ferme la fenêtre de notification
		}
	}
}
