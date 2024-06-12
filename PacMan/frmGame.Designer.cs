namespace PacMan
{
	partial class frmGame
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
			components = new System.ComponentModel.Container();
			imgLives1 = new PictureBox();
			imgLives3 = new PictureBox();
			imgLives2 = new PictureBox();
			lblBns = new Label();
			lblScoreT = new Label();
			lblScore = new Label();
			btnPause = new Button();
			lblCleT = new Label();
			imgMap = new PictureBox();
			TmrGhost = new System.Windows.Forms.Timer(components);
			TmrPlayer = new System.Windows.Forms.Timer(components);
			pnlKeys = new Panel();
			picKey2 = new PictureBox();
			picKey1 = new PictureBox();
			pnlHealth = new Panel();
			picHealth = new PictureBox();
			PicBonus1 = new PictureBox();
			PicBonus2 = new PictureBox();
			PnlBonuses = new Panel();
			((System.ComponentModel.ISupportInitialize)imgLives1).BeginInit();
			((System.ComponentModel.ISupportInitialize)imgLives3).BeginInit();
			((System.ComponentModel.ISupportInitialize)imgLives2).BeginInit();
			((System.ComponentModel.ISupportInitialize)imgMap).BeginInit();
			pnlKeys.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)picKey2).BeginInit();
			((System.ComponentModel.ISupportInitialize)picKey1).BeginInit();
			pnlHealth.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)picHealth).BeginInit();
			((System.ComponentModel.ISupportInitialize)PicBonus1).BeginInit();
			((System.ComponentModel.ISupportInitialize)PicBonus2).BeginInit();
			PnlBonuses.SuspendLayout();
			this.SuspendLayout();
			// 
			// imgLives1
			// 
			imgLives1.Image = Properties.Resources.redHeart;
			imgLives1.Location = new Point(12, 12);
			imgLives1.Name = "imgLives1";
			imgLives1.Size = new Size(47, 29);
			imgLives1.SizeMode = PictureBoxSizeMode.Zoom;
			imgLives1.TabIndex = 0;
			imgLives1.TabStop = false;
			// 
			// imgLives3
			// 
			imgLives3.Image = Properties.Resources.blackHeart;
			imgLives3.Location = new Point(98, 12);
			imgLives3.Name = "imgLives3";
			imgLives3.Size = new Size(47, 29);
			imgLives3.SizeMode = PictureBoxSizeMode.Zoom;
			imgLives3.TabIndex = 1;
			imgLives3.TabStop = false;
			// 
			// imgLives2
			// 
			imgLives2.Image = Properties.Resources.redHeart;
			imgLives2.Location = new Point(55, 12);
			imgLives2.Name = "imgLives2";
			imgLives2.Size = new Size(47, 29);
			imgLives2.SizeMode = PictureBoxSizeMode.Zoom;
			imgLives2.TabIndex = 2;
			imgLives2.TabStop = false;
			// 
			// lblBns
			// 
			lblBns.AutoSize = true;
			lblBns.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblBns.ForeColor = Color.CornflowerBlue;
			lblBns.Location = new Point(12, 113);
			lblBns.Name = "lblBns";
			lblBns.Size = new Size(71, 23);
			lblBns.TabIndex = 5;
			lblBns.Text = "Bonus : ";
			// 
			// lblScoreT
			// 
			lblScoreT.AutoSize = true;
			lblScoreT.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblScoreT.ForeColor = Color.CornflowerBlue;
			lblScoreT.Location = new Point(12, 320);
			lblScoreT.Name = "lblScoreT";
			lblScoreT.Size = new Size(61, 23);
			lblScoreT.TabIndex = 8;
			lblScoreT.Text = "Score :";
			// 
			// lblScore
			// 
			lblScore.AutoSize = true;
			lblScore.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblScore.ForeColor = SystemColors.ButtonFace;
			lblScore.Location = new Point(79, 320);
			lblScore.Name = "lblScore";
			lblScore.Size = new Size(19, 23);
			lblScore.TabIndex = 9;
			lblScore.Text = "0";
			// 
			// btnPause
			// 
			btnPause.BackColor = Color.FromArgb(51, 51, 51);
			btnPause.Cursor = Cursors.Hand;
			btnPause.FlatStyle = FlatStyle.Flat;
			btnPause.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnPause.ForeColor = Color.White;
			btnPause.Location = new Point(1019, 12);
			btnPause.Name = "btnPause";
			btnPause.Size = new Size(104, 44);
			btnPause.TabIndex = 10;
			btnPause.Text = "Pause";
			btnPause.UseVisualStyleBackColor = false;
			btnPause.Click += this.btnPause_Click;
			// 
			// lblCleT
			// 
			lblCleT.AutoSize = true;
			lblCleT.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblCleT.ForeColor = Color.CornflowerBlue;
			lblCleT.Location = new Point(1011, 92);
			lblCleT.Name = "lblCleT";
			lblCleT.Size = new Size(55, 23);
			lblCleT.TabIndex = 11;
			lblCleT.Text = "Clés : ";
			// 
			// imgMap
			// 
			imgMap.Location = new Point(191, 25);
			imgMap.Name = "imgMap";
			imgMap.Size = new Size(791, 636);
			imgMap.SizeMode = PictureBoxSizeMode.Zoom;
			imgMap.TabIndex = 14;
			imgMap.TabStop = false;
			// 
			// TmrPlayer
			// 
			TmrPlayer.Enabled = true;
			TmrPlayer.Interval = 250;
			TmrPlayer.Tick += this.TmrPlayer_Tick;
			// 
			// pnlKeys
			// 
			pnlKeys.Controls.Add(picKey2);
			pnlKeys.Controls.Add(picKey1);
			pnlKeys.Location = new Point(1038, 127);
			pnlKeys.Name = "pnlKeys";
			pnlKeys.Size = new Size(85, 171);
			pnlKeys.TabIndex = 15;
			pnlKeys.TabIndexChanged += this.pnlKeys_TabIndexChanged;
			// 
			// picKey2
			// 
			picKey2.Image = Properties.Resources.noRessources;
			picKey2.Location = new Point(9, 100);
			picKey2.Name = "picKey2";
			picKey2.Size = new Size(67, 62);
			picKey2.SizeMode = PictureBoxSizeMode.Zoom;
			picKey2.TabIndex = 17;
			picKey2.TabStop = false;
			// 
			// picKey1
			// 
			picKey1.Image = Properties.Resources.noRessources;
			picKey1.Location = new Point(9, 12);
			picKey1.Name = "picKey1";
			picKey1.Size = new Size(67, 62);
			picKey1.SizeMode = PictureBoxSizeMode.Zoom;
			picKey1.TabIndex = 16;
			picKey1.TabStop = false;
			// 
			// pnlHealth
			// 
			pnlHealth.Controls.Add(picHealth);
			pnlHealth.Location = new Point(12, 59);
			pnlHealth.Name = "pnlHealth";
			pnlHealth.Size = new Size(133, 39);
			pnlHealth.TabIndex = 16;
			pnlHealth.TabIndexChanged += this.pnlHealth_TabIndexChanged;
			// 
			// picHealth
			// 
			picHealth.Image = Properties.Resources.fulLife;
			picHealth.Location = new Point(1, 1);
			picHealth.Name = "picHealth";
			picHealth.Size = new Size(131, 37);
			picHealth.SizeMode = PictureBoxSizeMode.Zoom;
			picHealth.TabIndex = 0;
			picHealth.TabStop = false;
			// 
			// PicBonus2
			// 
			PicBonus2.Image = Properties.Resources.noRessources;
			PicBonus2.Location = new Point(9, 100);
			PicBonus2.Name = "PicBonus2";
			PicBonus2.Size = new Size(67, 62);
			PicBonus2.SizeMode = PictureBoxSizeMode.Zoom;
			PicBonus2.TabIndex = 17;
			PicBonus2.TabStop = false;
			// 
			// PicBonus1
			// 
			PicBonus1.Image = Properties.Resources.noRessources;
			PicBonus1.Location = new Point(9, 12);
			PicBonus1.Name = "PicBonus1";
			PicBonus1.Size = new Size(67, 62);
			PicBonus1.SizeMode = PictureBoxSizeMode.Zoom;
			PicBonus1.TabIndex = 16;
			PicBonus1.TabStop = false;
			// 
			// PnlBonuses
			// 
			PnlBonuses.Controls.Add(PicBonus2);
			PnlBonuses.Controls.Add(PicBonus1);
			PnlBonuses.Location = new Point(55, 139);
			PnlBonuses.Name = "PnlBonuses";
			PnlBonuses.Size = new Size(85, 171);
			PnlBonuses.TabIndex = 17;
			PnlBonuses.TabIndexChanged += this.PnlBonuses_TabIndexChanged;
			// 
			// frmGame
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(51, 51, 51);
			ClientSize = new Size(1135, 673);
			Controls.Add(PnlBonuses);
			Controls.Add(pnlHealth);
			Controls.Add(pnlKeys);
			Controls.Add(imgMap);
			Controls.Add(lblCleT);
			Controls.Add(btnPause);
			Controls.Add(lblScore);
			Controls.Add(lblScoreT);
			Controls.Add(lblBns);
			Controls.Add(imgLives2);
			Controls.Add(imgLives3);
			Controls.Add(imgLives1);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Icon = Properties.Resources.logo;
			KeyPreview = true;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "frmGame";
			Text = "Mode Infini | Le Continentale";
			Closed += this.frmGame_Closed;
			FormClosing += this.frmGame_FormClosing;
			Load += this.frmGame_Load;
			((System.ComponentModel.ISupportInitialize)imgLives1).EndInit();
			((System.ComponentModel.ISupportInitialize)imgLives3).EndInit();
			((System.ComponentModel.ISupportInitialize)imgLives2).EndInit();
			((System.ComponentModel.ISupportInitialize)imgMap).EndInit();
			pnlKeys.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)picKey2).EndInit();
			((System.ComponentModel.ISupportInitialize)picKey1).EndInit();
			pnlHealth.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)picHealth).EndInit();
			((System.ComponentModel.ISupportInitialize)PicBonus1).EndInit();
			((System.ComponentModel.ISupportInitialize)PicBonus2).EndInit();
			PnlBonuses.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private PictureBox imgLives1;
		private PictureBox imgLives3;
		private PictureBox imgLives2;
		private Label lblBns;
		private Label lblScoreT;
		private Label lblScore;
		private Button btnPause;
		private Label lblCleT;
		private PictureBox imgMap;
		private System.Windows.Forms.Timer TmrGhost;
		private System.Windows.Forms.Timer TmrPlayer;
		private Panel pnlKeys;
		private PictureBox picKey1;
		private PictureBox picKey2;
		private Panel pnlHealth;
		private PictureBox PicBonus1;
		private PictureBox PicBonus2;
		private Panel PnlBonuses;
		private PictureBox picHealth;
	}
}