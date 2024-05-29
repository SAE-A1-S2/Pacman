using Engine;

namespace PacMan
{
	public partial class frmStats : Form
	{
		private bool isDragging = false;
		private Point lastCursorPosition;
		public frmStats()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			using Pen pen = new(Color.White, 1);
			e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
		}

		private void btnRetour_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void frmStats_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isDragging = true;
				lastCursorPosition = PointToScreen(e.Location);
			}
		}

		private void frmStats_MouseMove(object sender, MouseEventArgs e)
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

		private void frmStats_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				isDragging = false;
		}

		private void frmStats_Load(object sender, EventArgs e)
		{
			try
			{

				FileManager fileManager = new();
				List<PlayerData> allData = fileManager.LoadGameData();
				lblHighScore.Text = allData.Max(x => x.Score).ToString();
				lblTScoreC.Text = allData.Sum(x => x.Score).ToString();
				lblLowestScore.Text = allData.Min(x => x.Score).ToString();
				lblAvgScore.Text = allData.Average(x => x.Score).ToString();
				lblHoursSpent.Text = allData.Sum(x => x.TimeSpentInMinutes) / 60 + "h " + allData.Sum(x => x.TimeSpentInMinutes) % 60 + "m";
			}
			catch (InvalidOperationException)
			{

				lblHighScore.Text = "0";
				lblTScoreC.Text = "0";
				lblLowestScore.Text = "0";
				lblAvgScore.Text = "0";
				lblHoursSpent.Text = "0h 0m";
			}
		}
	}
}
