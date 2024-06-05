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
		private FrmNotif frmNotif;             // Fenêtre de confirmation de fermeture

		public FrmHome()
		{
			InitializeComponent(); // Initialise les composants visuels du formulaire

			// Instancie les fenêtres enfants
			popUpForm = new();
			credits = new();
			stats = new();
			frmNotif = new(this); // La fenêtre de notification a besoin d'une référence à FrmHome

			// Gestion des événements :
			stats.VisibleChanged += Handle_Visibility;     // Appelé quand la visibilité de la fenêtre stats change
			credits.VisibleChanged += Handle_Visibility;   // Appelé quand la visibilité de la fenêtre crédits change
			popUpForm.FormClosed += popup_FormClosed;     // Appelé quand la fenêtre popup est fermée

			// Affiche la fenêtre popup au lancement si l'option est activée
			if (Properties.Settings.Default.ShowDialogOnLaunch)
			{
				SetLabelStates(false); // Désactive les labels pendant que la popup est affichée
				popUpForm.Show(this); // Affiche la fenêtre popup en tant que fenêtre non modale
			}
		}

		// Active ou désactive les labels du menu principal (en fonction de l'affichage des fenêtres enfants)
		private void SetLabelStates(bool enabled)
		{
			// Change la couleur du texte des labels en fonction de leur état
			Color labelColor = enabled ? Color.CornflowerBlue : Color.Gray;

			// Active ou désactive les labels et change leur couleur
			lblCredit.Enabled = enabled;
			lblHistore.Enabled = enabled;
			lblInfini.Enabled = enabled;
			lblQuit.Enabled = enabled;
			lblStat.Enabled = enabled;

			lblCredit.ForeColor = labelColor;
			lblHistore.ForeColor = labelColor;
			lblInfini.ForeColor = labelColor;
			lblQuit.ForeColor = labelColor;
			lblStat.ForeColor = labelColor;
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
		private void lblQuit_Click(object sender, EventArgs e)
		{
			Close(); // Ferme l'application (déclenche l'événement FormClosing)
		}

		private void lblCredit_Click(object sender, EventArgs e)
		{
			SetLabelStates(false); // Désactive les labels pendant que la fenêtre est affichée
			credits.ShowDialog(this); // Affiche la fenêtre des crédits en tant que boîte de dialogue modale
		}

		private void lblStat_Click(object sender, EventArgs e)
		{
			SetLabelStates(false); // Désactive les labels pendant que la fenêtre est affichée
			stats.ShowDialog(this); // Affiche la fenêtre des statistiques en tant que boîte de dialogue modale
		}

		// Lance le mode de jeu infini
		private void lblInfini_Click(object sender, EventArgs e)
		{
			Hide(); // Cache la fenêtre principale
			frmGame game = new(GameMode.INFINITE) // Crée une nouvelle instance de frmGame en mode infini
			{
				StartPosition = FormStartPosition.CenterParent // Centre la fenêtre de jeu par rapport à FrmHome
			};
			game.Show(); // Affiche la fenêtre de jeu
		}

		// Lance le mode histoire
		private void lblHistore_Click(object sender, EventArgs e)
		{
			Hide(); // Cache la fenêtre principale
			frmGame game = new(GameMode.STORY)  // Crée une nouvelle instance de frmGame en mode histoire
			{
				StartPosition = FormStartPosition.CenterParent // Centre la fenêtre de jeu par rapport à FrmHome
			};
			game.Show(); // Affiche la fenêtre de jeu
		}

		// Evenement appelé avant la fermeture de la fenêtre principale
		// le but est de demander à l'utilisateur s'il veut vraiment quitter le jeu
		// et aussi de verifer que la fermeture est une erreur ou bien demandée par l'utilisateur
		private void FrmHome_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Affiche la fenêtre de notification
			// Si l'utilisateur clique sur "Non", on annule la fermeture de la fenêtre principale
			frmNotif.ShowDialog(this);
			if(!frmNotif.Result)
				// Si jamais l'utilisateur clique sur "Non", on annule demande de fermeture
				e.Cancel = true;
		}
	}
}