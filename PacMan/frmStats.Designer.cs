namespace PacMan
{
	partial class FrmStats
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			btnRetour = new Button();
			lblTitle = new Label();
			lblTScore = new Label();
			lblLowS = new Label();
			lblHighS = new Label();
			lblAvgS = new Label();
			lblHourSpent = new Label();
			lblTScoreC = new Label();
			lblAvgScore = new Label();
			lblHighScore = new Label();
			lblLowestScore = new Label();
			lblHoursSpent = new Label();
			SuspendLayout();
			// 
			// btnRetour
			// 
			btnRetour.BackColor = Color.CornflowerBlue;
			btnRetour.Cursor = Cursors.Hand;
			btnRetour.FlatStyle = FlatStyle.Flat;
			btnRetour.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnRetour.ForeColor = SystemColors.ActiveCaptionText;
			btnRetour.Location = new Point(44, 330);
			btnRetour.Name = "btnRetour";
			btnRetour.Size = new Size(94, 29);
			btnRetour.TabIndex = 0;
			btnRetour.Text = "Retour";
			btnRetour.UseVisualStyleBackColor = false;
			btnRetour.Click += btnRetour_Click;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblTitle.ForeColor = Color.CornflowerBlue;
			lblTitle.Location = new Point(226, 9);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(139, 31);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Statistiques";
			// 
			// lblTScore
			// 
			lblTScore.AutoSize = true;
			lblTScore.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblTScore.ForeColor = Color.CornflowerBlue;
			lblTScore.Location = new Point(44, 69);
			lblTScore.Name = "lblTScore";
			lblTScore.Size = new Size(101, 23);
			lblTScore.TabIndex = 2;
			lblTScore.Text = "Score total :";
			// 
			// lblLowS
			// 
			lblLowS.AutoSize = true;
			lblLowS.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblLowS.ForeColor = Color.CornflowerBlue;
			lblLowS.Location = new Point(44, 191);
			lblLowS.Name = "lblLowS";
			lblLowS.Size = new Size(117, 23);
			lblLowS.TabIndex = 3;
			lblLowS.Text = "Lowest score :";
			// 
			// lblHighS
			// 
			lblHighS.AutoSize = true;
			lblHighS.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblHighS.ForeColor = Color.CornflowerBlue;
			lblHighS.Location = new Point(44, 148);
			lblHighS.Name = "lblHighS";
			lblHighS.Size = new Size(123, 23);
			lblHighS.TabIndex = 4;
			lblHighS.Text = "Highest score :";
			// 
			// lblAvgS
			// 
			lblAvgS.AutoSize = true;
			lblAvgS.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblAvgS.ForeColor = Color.CornflowerBlue;
			lblAvgS.Location = new Point(44, 108);
			lblAvgS.Name = "lblAvgS";
			lblAvgS.Size = new Size(221, 23);
			lblAvgS.TabIndex = 5;
			lblAvgS.Text = "Average score per session : ";
			// 
			// lblHourSpent
			// 
			lblHourSpent.AutoSize = true;
			lblHourSpent.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblHourSpent.ForeColor = Color.CornflowerBlue;
			lblHourSpent.Location = new Point(44, 239);
			lblHourSpent.Name = "lblHourSpent";
			lblHourSpent.Size = new Size(112, 23);
			lblHourSpent.TabIndex = 6;
			lblHourSpent.Text = "Hours spent :";
			// 
			// lblTScoreC
			// 
			lblTScoreC.AutoSize = true;
			lblTScoreC.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblTScoreC.ForeColor = SystemColors.ButtonFace;
			lblTScoreC.Location = new Point(151, 69);
			lblTScoreC.Name = "lblTScoreC";
			lblTScoreC.Size = new Size(19, 23);
			lblTScoreC.TabIndex = 7;
			lblTScoreC.Text = "0";
			// 
			// lblAvgScore
			// 
			lblAvgScore.AutoSize = true;
			lblAvgScore.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblAvgScore.ForeColor = SystemColors.ButtonFace;
			lblAvgScore.Location = new Point(271, 108);
			lblAvgScore.Name = "lblAvgScore";
			lblAvgScore.Size = new Size(19, 23);
			lblAvgScore.TabIndex = 8;
			lblAvgScore.Text = "0";
			// 
			// lblHighScore
			// 
			lblHighScore.AutoSize = true;
			lblHighScore.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblHighScore.ForeColor = SystemColors.ButtonFace;
			lblHighScore.Location = new Point(173, 148);
			lblHighScore.Name = "lblHighScore";
			lblHighScore.Size = new Size(19, 23);
			lblHighScore.TabIndex = 9;
			lblHighScore.Text = "0";
			// 
			// lblLowestScore
			// 
			lblLowestScore.AutoSize = true;
			lblLowestScore.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblLowestScore.ForeColor = SystemColors.ButtonFace;
			lblLowestScore.Location = new Point(167, 191);
			lblLowestScore.Name = "lblLowestScore";
			lblLowestScore.Size = new Size(19, 23);
			lblLowestScore.TabIndex = 10;
			lblLowestScore.Text = "0";
			// 
			// lblHoursSpent
			// 
			lblHoursSpent.AutoSize = true;
			lblHoursSpent.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblHoursSpent.ForeColor = SystemColors.ButtonFace;
			lblHoursSpent.Location = new Point(162, 239);
			lblHoursSpent.Name = "lblHoursSpent";
			lblHoursSpent.Size = new Size(19, 23);
			lblHoursSpent.TabIndex = 11;
			lblHoursSpent.Text = "0";
			// 
			// frmStats
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(51, 51, 51);
			ClientSize = new Size(616, 381);
			Controls.Add(lblHoursSpent);
			Controls.Add(lblLowestScore);
			Controls.Add(lblHighScore);
			Controls.Add(lblAvgScore);
			Controls.Add(lblTScoreC);
			Controls.Add(lblHourSpent);
			Controls.Add(lblAvgS);
			Controls.Add(lblHighS);
			Controls.Add(lblLowS);
			Controls.Add(lblTScore);
			Controls.Add(lblTitle);
			Controls.Add(btnRetour);
			ForeColor = SystemColors.ControlDarkDark;
			FormBorderStyle = FormBorderStyle.None;
			Name = "frmStats";
			Text = "frmStats";
			Icon = Properties.Resources.logo;
			Load += frmStats_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button btnRetour;
		private Label lblTitle;
		private Label lblTScore;
		private Label lblLowS;
		private Label lblHighS;
		private Label lblAvgS;
		private Label lblHourSpent;
		private Label lblTScoreC;
		private Label lblAvgScore;
		private Label lblHighScore;
		private Label lblLowestScore;
		private Label lblHoursSpent;
	}
}