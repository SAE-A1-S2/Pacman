namespace PacMan
{
	partial class FrmLostGame
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
			lblGameLost = new Label();
			BtnReLvl = new Button();
			BtnQuit = new Button();
			lblCScore = new Label();
			lblScore = new Label();
			this.SuspendLayout();
			// 
			// lblGameLost
			// 
			lblGameLost.AutoSize = true;
			lblGameLost.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblGameLost.ForeColor = Color.CornflowerBlue;
			lblGameLost.Location = new Point(104, 36);
			lblGameLost.Name = "lblGameLost";
			lblGameLost.Size = new Size(179, 38);
			lblGameLost.TabIndex = 0;
			lblGameLost.Text = "Partie perdu";
			// 
			// BtnReLvl
			// 
			BtnReLvl.Cursor = Cursors.Hand;
			BtnReLvl.FlatAppearance.BorderSize = 0;
			BtnReLvl.FlatStyle = FlatStyle.Flat;
			BtnReLvl.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			BtnReLvl.ForeColor = Color.DodgerBlue;
			BtnReLvl.Location = new Point(89, 195);
			BtnReLvl.Name = "BtnReLvl";
			BtnReLvl.Size = new Size(206, 70);
			BtnReLvl.TabIndex = 1;
			BtnReLvl.Text = "Relancer la partie";
			BtnReLvl.UseVisualStyleBackColor = true;
			BtnReLvl.Click += this.BtnReLvl_Click;
			// 
			// BtnQuit
			// 
			BtnQuit.BackColor = Color.FromArgb(51, 51, 51);
			BtnQuit.Cursor = Cursors.Hand;
			BtnQuit.FlatAppearance.BorderSize = 0;
			BtnQuit.FlatStyle = FlatStyle.Flat;
			BtnQuit.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			BtnQuit.ForeColor = Color.DodgerBlue;
			BtnQuit.Location = new Point(89, 284);
			BtnQuit.Name = "BtnQuit";
			BtnQuit.Size = new Size(206, 59);
			BtnQuit.TabIndex = 2;
			BtnQuit.Text = "Quitter";
			BtnQuit.UseVisualStyleBackColor = false;
			BtnQuit.Click += this.BtnQuit_Click;
			// 
			// lblCScore
			// 
			lblCScore.AutoSize = true;
			lblCScore.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblCScore.ForeColor = Color.RoyalBlue;
			lblCScore.Location = new Point(65, 124);
			lblCScore.Name = "lblCScore";
			lblCScore.Size = new Size(132, 28);
			lblCScore.TabIndex = 3;
			lblCScore.Text = "Votre score : ";
			// 
			// lblScore
			// 
			lblScore.AutoSize = true;
			lblScore.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblScore.ForeColor = SystemColors.ButtonFace;
			lblScore.Location = new Point(203, 124);
			lblScore.Name = "lblScore";
			lblScore.Size = new Size(23, 28);
			lblScore.TabIndex = 4;
			lblScore.Text = "0";
			// 
			// FrmLostGame
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(51, 51, 51);
			ClientSize = new Size(402, 381);
			Controls.Add(lblScore);
			Controls.Add(lblCScore);
			Controls.Add(BtnQuit);
			Controls.Add(BtnReLvl);
			Controls.Add(lblGameLost);
			FormBorderStyle = FormBorderStyle.None;
			Icon = Properties.Resources.logo;
			Name = "FrmLostGame";
			Text = "FrmLostGame";
			Load += this.FrmLostGame_Load;
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private Label lblGameLost;
		private Button BtnReLvl;
		private Button BtnQuit;
		private Label lblCScore;
		private Label lblScore;
	}
}