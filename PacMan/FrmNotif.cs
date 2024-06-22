namespace PacMan
{
	public partial class FrmNotif : FrmEntity
	{
		public bool Result { get; private set; } // Résultat de la notification (true pour "Oui", false pour "Non")

		private readonly Form form; // Référence au formulaire appelant la notification

		// Variable indiquant si la notification provient d'un manque de partie sauvegardée dans la base de données
		private readonly bool IsGameDB;

		public FrmNotif(object sender, bool isGame = false)
		{
			InitializeComponent();
			form = (Form)sender; // Stockage du formulaire appelant
			IsGameDB = isGame; // Stockage de l'origine de la notification
		}

		// Gestionnaire de l'événement de chargement du formulaire
		private void FrmNotif_Load(object sender, EventArgs e)
		{
			CenterToParent(); // Centre la fenêtre par rapport à sa fenêtre parent

			// Affichage d'un message différent selon l'origine de la notification
			// Si la notification vient du formulaire de jeu
			if (form.Name == "frmGame")
			{
				lblMsg.Text = "Voulez-vous vraiment quitter la partie ?";
				lblDetails.Text = "Toutes les données non sauvegardées seront perdues.";
			}

			// Si la notification vient du formulaire d'accueil (sans problème de sauvegarde)
			else if (form.Name == "FrmHome" && !IsGameDB)
			{
				lblMsg.Text = "Voulez-vous vraiment quitter le jeu ?";
				lblDetails.Text = "";
			}

			// Si la notification vient du formulaire d'accueil (avec problème de sauvegarde)
			else if ((form.Name == "FrmHome" || form.Name == "frmGame") && IsGameDB)
			{
				lblMsg.Text = "Aucune partie n'a été sauvegardée ou recupérée.";
				lblDetails.Text = "Voulez-vous continuer en mode infini ?";
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
