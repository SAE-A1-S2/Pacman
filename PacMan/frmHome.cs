namespace PacMan
{
    public partial class frmHome : Form
    {
        private frmPopUp popUpForm;
        private frmCredits credits;
        protected frmStats stats;

        public frmHome()
        {
            InitializeComponent();
            popUpForm = new();
            credits = new();
            stats = new();
            stats.FormClosing += handle_FormClosing;
            credits.FormClosing += handle_FormClosing;
            popUpForm.FormClosed += popup_FormClosed;
            if (Properties.Settings.Default.ShowDialogOnLaunch)
            {
                SetLabelStates(false);
                popUpForm.StartPosition = FormStartPosition.CenterParent;
                popUpForm.Show(this);
            }
        }

        private void SetLabelStates(bool enabled)
        {
            Color labelColor = enabled ? Color.CornflowerBlue : Color.Gray;
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


        private void popup_FormClosed(object? sender, FormClosedEventArgs e)
        {
            SetLabelStates(true);
            popUpForm.Dispose();
        }

        private void handle_FormClosing(object? sender, FormClosingEventArgs e)
        {
            SetLabelStates(true);
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
            credits.Hide();
            stats.Hide();
        }

        private void lblQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblCredit_Click(object sender, EventArgs e)
        {
            SetLabelStates(false);
            credits.StartPosition = FormStartPosition.CenterParent;
            credits.Show();
        }

        private void lblStat_Click(object sender, EventArgs e)
        {
            SetLabelStates(false);
            stats.StartPosition = FormStartPosition.CenterParent;
            stats.Show();

        }

        private void lblInfini_Click(object sender, EventArgs e)
        {
            Hide();
            frmGame game = new()
            {
                StartPosition = FormStartPosition.CenterParent
            };
            game.Show();
        }

        private void lblHistore_Click(object sender, EventArgs e)
        {
            Hide();
            frmGame game = new(true)
            {
                StartPosition = FormStartPosition.CenterParent,
            };
            game.Show();
        }
    }
}
