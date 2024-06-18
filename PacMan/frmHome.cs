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
Ce fichier contient le code du formulaire  FrmHome qui gère l'interface principale de notre jeu. 
Il inclut des fonctionnalités pour afficher des fenêtres popup, les crédits, les statistiques et pour démarrer différents modes de jeu.
*/

using Engine.utils;

namespace PacMan
{
	public partial class FrmHome : Form
	{
		// Fenêtres enfants de FrmHome
		private readonly FrmPopUp popUpForm;     // Fenêtre popup affichée au lancement
		private readonly FrmCredits credits;     // Fenêtre des crédits
		private readonly FrmStats stats;         // Fenêtre des statistiques

		public FrmHome()
		{
			InitializeComponent(); // Initialise les composants visuels du formulaire

			// Instancie les fenêtres enfants
			popUpForm = new FrmPopUp();
			credits = new FrmCredits();
			stats = new FrmStats();

			// Gestion des événements :
			stats.VisibleChanged += Handle_Visibility;     // Appelé quand la visibilité de la fenêtre stats change
			credits.VisibleChanged += Handle_Visibility;   // Appelé quand la visibilité de la fenêtre crédits change
			popUpForm.FormClosed += popup_FormClosed;     // Appelé quand la fenêtre popup est fermée

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
			SetLabelStates(true);      // Réactive les labels du menu principal
			popUpForm.Dispose();     // Libère les ressources de la fenêtre popup
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
			Close(); // Ferme l'application (déclenche l'événement FormClosing)
		}

		private void BtnCredit_Click(object sender, EventArgs e)
		{
			SetLabelStates(false); // Désactive les labels pendant que la fenêtre est affichée
			credits.ShowDialog(this); // Affiche la fenêtre des crédits en tant que boîte de dialogue modale
		}

		private void BtnStat_Click(object sender, EventArgs e)
		{
			SetLabelStates(false); // Désactive les labels pendant que la fenêtre est affichée
			stats.ShowDialog(this); // Affiche la fenêtre des statistiques en tant que boîte de dialogue modale
		}

		// Lance le mode de jeu infini
		private void BtnGame_Click(object sender, EventArgs e)
		{
			// Vérifie si le nom du joueur est défini. Si non, affiche la fenêtre de saisie.
			if (string.IsNullOrEmpty(Properties.Settings.Default.PlayerName))
			{
				FrmName frmName = new(); // Utilisation du mot clé "using" pour une gestion automatique de la ressource
				SetLabelStates(false);
				frmName.ShowDialog(); // Affiche la fenêtre de saisie
				if (frmName.Result) // Vérification du résultat de la boîte de dialogue
				{
					Hide();
					CreateAndShowGameForm((Button)sender, frmName.PlayerName); // Réutilisation de la logique de création du formulaire
					return;
				}
				SetLabelStates(true);
			}

			// Si le nom est déjà défini, lance directement le jeu
			Hide();
			CreateAndShowGameForm((Button)sender, Properties.Settings.Default.PlayerName);
		}

		// Nouvelle méthode pour créer et afficher le formulaire de jeu
		private void CreateAndShowGameForm(Button senderButton, string playerName)
		{
			GameMode mode = senderButton == btnInfini ? GameMode.INFINITE : GameMode.STORY;
			frmGame game = new(mode, playerName);
			game.Show();
		}

		// Evenement appelé avant la fermeture de la fenêtre principale
		// le but est de demander à l'utilisateur s'il veut vraiment quitter le jeu
		// et aussi de verifer que la fermeture est une erreur ou bien demandée par l'utilisateur
		private void FrmHome_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Affiche la fenêtre de notification
			// Si l'utilisateur clique sur "Non", on annule la fermeture de la fenêtre principale
			FrmNotif frmNotif = new(this);
			frmNotif.ShowDialog(this);
			if (!frmNotif.Result)
				// Si jamais l'utilisateur clique sur "Non", on annule demande de fermeture
				e.Cancel = true;
		}

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
			SetLabelStates(false); // Désactive les labels pendant que la fenêtre est affichée
			FrmSettings settings = new(); // Crée une nouvelle instance de la fenêtre de paramètres
			settings.ShowDialog(this); // Affiche la fenêtre de paramètres en tant que boîte de dialogue modale
			SetLabelStates(true); // Réactive les labels du menu principal
		}

		private void BtnDB_Click(object sender, EventArgs e)
		{
			Hide();
			frmGame frmGame = new(Properties.Settings.Default.LastInserterID);
			frmGame.Show();
		}
	}
}