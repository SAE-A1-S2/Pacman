namespace PacMan
{
	public partial class FrmSettings : FrmEntity
	{
		public FrmSettings()
		{
			InitializeComponent();
		}

		// Gestionnaire d'événement pour le chargement du formulaire
		private void FrmSettings_Load(object sender, EventArgs e)
		{
			// Centre le formulaire par rapport à sa fenêtre parente
			CenterToParent();

			// Charge les paramètres depuis les propriétés de l'application
			lblName.Text = Properties.Settings.Default.PlayerName;
			chkShowDialog.Checked = Properties.Settings.Default.ShowDialogOnLaunch;

			// Met à jour le texte de la case à cocher en fonction du paramètre chargé
			chkShowDialog.Text = Properties.Settings.Default.ShowDialogOnLaunch ? "Oui" : "Non";
		}

		// Gestionnaire d'événement pour le clic sur le bouton "Continuer sans sauvegarder"
		private void BtnNoSave_Click(object sender, EventArgs e)
		{
			// Ferme le formulaire sans enregistrer les modifications
			Close();
		}

		// Gestionnaire d'événement pour le clic sur le bouton "Enregistrer"
		private void BtnSave_Click(object sender, EventArgs e)
		{
			// Enregistre les paramètres dans les propriétés de l'application
			Properties.Settings.Default.ShowDialogOnLaunch = chkShowDialog.Checked;

			// Enregistre le nouveau nom seulement s'il n'est pas vide
			if (!string.IsNullOrWhiteSpace(txtNewName.Text))
				Properties.Settings.Default.PlayerName = txtNewName.Text;

			// Sauvegarde les paramètres mis à jour
			Properties.Settings.Default.Save();
			Close();
		}

		// Gestionnaire d'événement pour le changement d'état de la case à cocher
		private void chkShowDialog_CheckedChanged(object sender, EventArgs e)
		{
			// Met à jour le texte de la case à cocher en fonction de son état actuel
			chkShowDialog.Text = chkShowDialog.Checked ? "Oui" : "Non";
		}
	}
}