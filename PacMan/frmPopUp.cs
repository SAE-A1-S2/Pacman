
namespace PacMan
{
    public partial class frmPopUp : Form
    {
        private bool isDragging = false;
        private Point lastCursorPosition;
        public frmPopUp()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Pen pen = new(Color.White, 1))
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
        }

        private void PopUpForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPosition = PointToScreen(e.Location);
            }
        }

        private void PopUpForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentCursorPosition = PointToScreen(e.Location);
                Location = new Point(
                    Location.X + (currentCursorPosition.X - lastCursorPosition.X),
                    Location.Y + (currentCursorPosition.Y - lastCursorPosition.Y));
                lastCursorPosition = currentCursorPosition;
            }
        }

        private void PopUpForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isDragging = false;
        }

        private void btnOKPopup_Click(object sender, EventArgs e)
        {
            if (chkPopup.Checked)
            {
                Properties.Settings.Default.ShowDialogOnLaunch = false;
                Properties.Settings.Default.Save();
            }
            Close();
        }
    }
}
