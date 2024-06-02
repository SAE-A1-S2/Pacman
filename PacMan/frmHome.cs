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
		// Fenêtre popup affichée au lancement
		private readonly FrmPopUp popUpForm;
		// Fenêtre des crédits
		private readonly FrmCredits credits;
		// Fenêtre des statistiques
		private readonly FrmStats stats;

		public FrmHome()
		{
			InitializeComponent();
			// Initialisation des fenêtres popup, crédits et statistiques
			popUpForm = new();
			credits = new();
			stats = new();
			// Association des événements aux fonctions correspondantes
			stats.VisibleChanged += Handle_Visibility;
			credits.VisibleChanged += Handle_Visibility;
			popUpForm.FormClosed += popup_FormClosed;
			if (Properties.Settings.Default.ShowDialogOnLaunch)
			{
				SetLabelStates(false);
				popUpForm.ShowDialog(this);
			}
		}

		// Active ou désactive les labels de l'interface principale en fonction de l'etat de la variable enabled.
		private void SetLabelStates(bool enabled)
		{
			Color labelColor = enabled ? Color.CornflowerBlue : Color.Gray; // on essaie de changer la couleur du texte en gris
			lblCredit.Enabled = enabled;
			lblHistore.Enabled = enabled; // active ou désactive les labels, pour qu'on puisse pas cliquer dessus quand les popups sont affichés
			lblInfini.Enabled = enabled;
			lblQuit.Enabled = enabled;
			lblStat.Enabled = enabled;

			lblCredit.ForeColor = labelColor;
			lblHistore.ForeColor = labelColor;
			lblInfini.ForeColor = labelColor;
			lblQuit.ForeColor = labelColor;
			lblStat.ForeColor = labelColor;
		}


		// Gère la fermeture de la fenêtre popup.
		private void popup_FormClosed(object? sender, FormClosedEventArgs e)
		{
			SetLabelStates(true); // Active les étiquettes
			popUpForm.Dispose(); // Libère les ressources utilisées par la fenêtre popup
		}

		private void Handle_Visibility(object? sender, EventArgs e)
		{
			if (!credits.Visible && !stats.Visible)
				SetLabelStates(true);
		}

		private void lblQuit_Click(object sender, EventArgs e)
		{
			Close(); // ferme la fenêtre actuelle
		}

		private void lblCredit_Click(object sender, EventArgs e)
		{
			SetLabelStates(false);
			credits.ShowDialog(this);
		}

		private void lblStat_Click(object sender, EventArgs e)
		{
			SetLabelStates(false); // Désactive les labels
			stats.ShowDialog(this); // Affiche la fenêtre des statistiques

		}

		// Gère le clic sur l'étiquette pour démarrer le mode infini.
		private void lblInfini_Click(object sender, EventArgs e)
		{
			Hide(); // Cache la fenêtre principale
			frmGame game = new(GameMode.INFINITE)
			{
				StartPosition = FormStartPosition.CenterParent
			};
			game.Show(); // Affiche la fenêtre de jeu en mode infini
		}

		// Méthode appelée quand le bouton "Mode Histoire" est cliqué
		private void lblHistore_Click(object sender, EventArgs e)
		{
			Hide(); // Cache la fenêtre principale
			frmGame game = new(GameMode.STORY) // instancie la fenêtre de jeu en mode histoire
			{
				StartPosition = FormStartPosition.CenterParent,
			};
			game.Show(); // Affiche la fenêtre de jeu en mode histoire
		}
	}
}