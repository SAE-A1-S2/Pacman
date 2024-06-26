/*
GROUPE D-06
SAE 2.01
2023-2024

Résumé:
Ce fichier contient le code du formulaire  FrmHome qui gère l'interface principale de notre jeu. 
Il inclut des fonctionnalités pour afficher des fenêtres popup, les crédits, les statistiques et pour démarrer différents modes de jeu.
*/

using Engine.utils;

namespace PacMan
{
	public partial class FrmHome : Form // Classe du formulaire principal (écran d'accueil)
	{
		// Déclaration des fenêtres enfants (formulaires secondaires)
		private readonly FrmPopUp popUpForm;     // Fenêtre popup affichée au lancement
		private readonly FrmCredits credits;     // Fenêtre des crédits
		private readonly FrmStats stats;         // Fenêtre des statistiques

		public FrmHome()
		{
			InitializeComponent();

			// Instancie les fenêtres enfants
			popUpForm = new FrmPopUp();
			credits = new FrmCredits();
			stats = new FrmStats();

			// Gestion des événements :
			stats.VisibleChanged += Handle_Visibility;     // Appelé quand la visibilité de la fenêtre stats change
			credits.VisibleChanged += Handle_Visibility;   // Appelé quand la visibilité de la fenêtre crédits change
			popUpForm.FormClosed += popup_FormClosed;     // Appelé quand la fenêtre popup est fermée

			// Génère et stocke un ID unique pour le joueur
			GenerateAndStorePlayerId();

		}

		// Active ou désactive les labels du menu principal (en fonction de l'affichage des fenêtres enfants)
		private void SetLabelStates(bool enabled)
		{
			// Change la couleur du texte des labels en fonction de leur état
			Color labelColor = enabled ? Color.CornflowerBlue : Color.Gray;

			// Active ou désactive les labels et change leur couleur
			btnCredits.Enabled = enabled;
			btnHistore.Enabled = enabled;
			btnInfini.Enabled = enabled;
			btnQuit.Enabled = enabled;
			btnStat.Enabled = enabled;
			BtnSettings.Enabled = enabled;
			BtnDB.Enabled = enabled;

			btnCredits.ForeColor = labelColor;
			btnHistore.ForeColor = labelColor;
			btnInfini.ForeColor = labelColor;
			btnQuit.ForeColor = labelColor;
			btnStat.ForeColor = labelColor;
			BtnSettings.ForeColor = labelColor;
			BtnDB.ForeColor = labelColor;
		}

		// Gère la fermeture de la fenêtre popup
		private void popup_FormClosed(object? sender, FormClosedEventArgs e)
		{
			// Réactive les labels du menu principal
			SetLabelStates(true);

			// Libère les ressources de la fenêtre popup
			popUpForm.Dispose();
		}

		// Gère le changement de visibilité des fenêtres "Crédits" et "Statistiques"
		private void Handle_Visibility(object? sender, EventArgs e)
		{
			// Réactive les labels du menu principal si aucune des deux fenêtres n'est visible
			if (!credits.Visible && !stats.Visible)
				SetLabelStates(true);
		}

		// Gestionnaires d'événements pour les clics sur les labels du menu principal
		private void BtnQuit_Click(object sender, EventArgs e)
		{
			// Ferme l'application (déclenche l'événement FormClosing)
			Close();
		}

		private void BtnCredit_Click(object sender, EventArgs e)
		{
			// Désactive les labels pendant que la fenêtre est affichée
			SetLabelStates(false);

			// Affiche la fenêtre des crédits en tant que boîte de dialogue modale
			credits.ShowDialog(this);
		}

		private void BtnStat_Click(object sender, EventArgs e)
		{
			SetLabelStates(false);

			// Affiche la fenêtre des statistiques en tant que boîte de dialogue modale
			stats.ShowDialog(this);
		}

		// Lance le jeu selon le bouton qui a été cliqué
		private void BtnGame_Click(object sender, EventArgs e)
		{
			// Vérifie si le nom du joueur est défini. Si non, affiche la fenêtre de saisie.
			if (string.IsNullOrEmpty(Properties.Settings.Default.PlayerName))
			{
				// Instancie la fenêtre de saisie de Pseudo
				FrmName frmName = new();
				SetLabelStates(false);

				// Affiche la fenêtre de saisie
				frmName.ShowDialog();

				// Vérification du résultat de la boîte de dialogue 
				if (frmName.Result)
				{
					Hide();
					CreateAndShowGameForm((Button)sender, frmName.PlayerName);
					return;
				}
				SetLabelStates(true);
			}

			// Si le nom est déjà défini, lance directement le jeu
			Hide();
			CreateAndShowGameForm((Button)sender, Properties.Settings.Default.PlayerName);
		}

		// Creer et affiche le jeu
		private void CreateAndShowGameForm(Button senderButton, string playerName)
		{
			// Determine le mode de jeu
			GameMode mode = senderButton == btnInfini ? GameMode.INFINITE : GameMode.STORY;
			frmGame game = new(mode, playerName);
			game.Show();

			// Active les labels du menu principal
			SetLabelStates(true);
		}

		// Evenement appelé avant la fermeture de la fenêtre principale
		// le but est de demander à l'utilisateur s'il veut vraiment quitter le jeu
		// et aussi de verifer que la fermeture est une erreur ou bien demandée par l'utilisateur
		private void FrmHome_FormClosing(object sender, FormClosingEventArgs e)
		{

			if (e.CloseReason == CloseReason.ApplicationExitCall) return;

			// Affiche la fenêtre de notification
			FrmNotif frmNotif = new(this);

			frmNotif.ShowDialog(this);
			if (!frmNotif.Result)
				// Si jamais l'utilisateur clique sur "Non", on annule demande de fermeture
				e.Cancel = true;
		}

		// Gestionnaire d'événement pour le chargement de la fenêtre
		private void FrmHome_Load(object sender, EventArgs e)
		{
			// Affiche la fenêtre popup au lancement si l'option est activée
			if (Properties.Settings.Default.ShowDialogOnLaunch)
			{
				SetLabelStates(false); // Désactive les labels pendant que la popup est affichée
				popUpForm.Show(this); // Affiche la fenêtre popup en tant que fenêtre non modale
			}
		}

		private void BtnSettings_Click(object sender, EventArgs e)
		{
			SetLabelStates(false);

			// Crée une nouvelle instance de la fenêtre de paramètres
			FrmSettings settings = new();
			settings.ShowDialog(this);

			// Réactive les labels du menu principal
			SetLabelStates(true);
		}

		/// <summary>
		/// Gère le clic de la bouton "BtnDB" (Reprendre la partie).
		/// </summary>
		/// <param name="sender">L'objet qui a déclenché l'événement.</param>
		/// <param name="e">Les arguments de l'événement.</param>
		private void BtnDB_Click(object sender, EventArgs e)
		{
			/// Récupère l'ID du dernier de la dernière partie enregistrée depuis les paramètres de l'application et vérifie s'il est égal à -1(signifie qu'aucune partie n'a été sauvegardée).
			int id = Properties.Settings.Default.LastInserterID;
			if (id == -1)
			{
				/// Si c'est le cas, on crée une nouvelle instance de la forme "FrmNotif" pour informer l'utilisateur que aucune partie n'a été sauvegardée et demander s'il souhaite continuer en mode infini.
				/// Si le résultat du dialogue est vrai, il appelle la méthode "BtnGame_Click" avec la bouton "btnInfini" en tant que source et les arguments d'événement fournis, pour lancer  le jeu en mode infini.
				FrmNotif frmNotif = new(this, true);
				frmNotif.ShowDialog(this);
				if (frmNotif.Result)
					BtnGame_Click(btnInfini, e);
				return;
			}

			/// Si l'ID du dernier partie enregistrée n'est pas égal à -1, on masque la forme actuelle et crée une nouvelle instance de la forme "frmGame" avec l'ID du derniere partie en tant que paramètre.
			/// Ensuite, il affiche la forme "frmGame".
			Hide();
			frmGame frmGame = new(id);
			frmGame.Show();
		}

		private static void GenerateAndStorePlayerId() // methode appelée à chaque lancement de l'application
		{
			// On verifie que l'ID du joueur n'est pas vide, ce qui signifie que l'utilisateur n'a pas encore d'ID enregistré
			if (string.IsNullOrEmpty(Properties.Settings.Default.PlayerUID))
			{
				// On crée un nouvel ID pour le joueur
				string newPlayerID = Guid.NewGuid().ToString();

				// On enregistre l'ID du joueur dans les paramètres de l'application
				Properties.Settings.Default.PlayerUID = newPlayerID;
				Properties.Settings.Default.Save();
			}
		}
	}
}