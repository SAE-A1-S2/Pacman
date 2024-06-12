namespace PacMan
{
	public partial class FrmSettings : Form
	{
		public FrmSettings()
		{
			InitializeComponent();
		}

		private void FrmSettings_Load(object sender, EventArgs e)
		{
			CenterToParent();
			lblName.Text = Properties.Settings.Default.PlayerName;
			chkShowDialog.Checked = Properties.Settings.Default.ShowDialogOnLaunch;
			chkShowDialog.Text = Properties.Settings.Default.ShowDialogOnLaunch ? "Oui" : "Non";
		}

		// Surcharge de la méthode OnPaint pour ajouter un contour blanc autour de la fenêtre
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);        // Appelle la méthode OnPaint de la classe de base

			// Dessine un rectangle blanc avec une largeur de 1 pixel
			using Pen pen = new(Color.White, 1);
			e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
		}

		private void BtnNoSave_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.ShowDialogOnLaunch = chkShowDialog.Checked;
			if (!string.IsNullOrWhiteSpace(txtNewName.Text))
				Properties.Settings.Default.PlayerName = txtNewName.Text;
			Properties.Settings.Default.Save();
			Close();
		}
	}
}