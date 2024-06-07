
namespace PacMan
{
	partial class FrmHome
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			lblTitle = new Label();
			picBoxHome = new PictureBox();
			btnHistore = new Button();
			btnInfini = new Button();
			btnStat = new Button();
			btnCredits = new Button();
			btnQuit = new Button();
			((System.ComponentModel.ISupportInitialize)picBoxHome).BeginInit();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblTitle.ForeColor = Color.CornflowerBlue;
			lblTitle.Location = new Point(402, 26);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(222, 38);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Le Continentale";
			// 
			// picBoxHome
			// 
			picBoxHome.Anchor = AnchorStyles.None;
			picBoxHome.Image = Properties.Resources.home;
			picBoxHome.Location = new Point(353, 86);
			picBoxHome.Name = "picBoxHome";
			picBoxHome.Size = new Size(612, 519);
			picBoxHome.SizeMode = PictureBoxSizeMode.Zoom;
			picBoxHome.TabIndex = 6;
			picBoxHome.TabStop = false;
			// 
			// btnHistore
			// 
			btnHistore.FlatAppearance.BorderSize = 0;
			btnHistore.FlatStyle = FlatStyle.Flat;
			btnHistore.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnHistore.ForeColor = Color.CornflowerBlue;
			btnHistore.Location = new Point(60, 156);
			btnHistore.Name = "btnHistore";
			btnHistore.Size = new Size(174, 52);
			btnHistore.TabIndex = 7;
			btnHistore.Text = "Mode Histoire";
			btnHistore.TextAlign = ContentAlignment.MiddleLeft;
			btnHistore.UseVisualStyleBackColor = true;
			btnHistore.Click += this.BtnHistore_Click;
			btnHistore.Cursor = Cursors.Hand;
			// 
			// btnInfini
			// 
			btnInfini.FlatAppearance.BorderSize = 0;
			btnInfini.FlatStyle = FlatStyle.Flat;
			btnInfini.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnInfini.ForeColor = Color.CornflowerBlue;
			btnInfini.Location = new Point(60, 214);
			btnInfini.Name = "btnInfini";
			btnInfini.Size = new Size(174, 52);
			btnInfini.TabIndex = 8;
			btnInfini.Text = "Mode Infini";
			btnInfini.TextAlign = ContentAlignment.MiddleLeft;
			btnInfini.UseVisualStyleBackColor = true;
			btnInfini.Click += this.BtnInfini_Click;
			btnInfini.Cursor = Cursors.Hand;
			// 
			// btnStats
			// 
			btnStat.FlatAppearance.BorderSize = 0;
			btnStat.FlatStyle = FlatStyle.Flat;
			btnStat.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnStat.ForeColor = Color.CornflowerBlue;
			btnStat.Location = new Point(60, 272);
			btnStat.Name = "btnStats";
			btnStat.Size = new Size(174, 52);
			btnStat.TabIndex = 9;
			btnStat.Text = "Statistiques";
			btnStat.TextAlign = ContentAlignment.MiddleLeft;
			btnStat.UseVisualStyleBackColor = true;
			btnStat.Click += this.BtnStat_Click;
			btnStat.Cursor = Cursors.Hand;
			// 
			// btnCredits
			// 
			btnCredits.FlatAppearance.BorderSize = 0;
			btnCredits.FlatStyle = FlatStyle.Flat;
			btnCredits.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnCredits.ForeColor = Color.CornflowerBlue;
			btnCredits.Location = new Point(60, 330);
			btnCredits.Name = "btnCredits";
			btnCredits.Size = new Size(174, 52);
			btnCredits.TabIndex = 10;
			btnCredits.Text = "Crédits";
			btnCredits.TextAlign = ContentAlignment.MiddleLeft;
			btnCredits.UseVisualStyleBackColor = true;
			btnCredits.Click += this.BtnCredit_Click;
			btnCredits.Cursor = Cursors.Hand;
			// 
			// btnQuit
			// 
			btnQuit.FlatAppearance.BorderSize = 0;
			btnQuit.FlatStyle = FlatStyle.Flat;
			btnQuit.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnQuit.ForeColor = Color.CornflowerBlue;
			btnQuit.Location = new Point(60, 388);
			btnQuit.Name = "btnQuit";
			btnQuit.Size = new Size(174, 52);
			btnQuit.TabIndex = 11;
			btnQuit.Text = "Quitter";
			btnQuit.TextAlign = ContentAlignment.MiddleLeft;
			btnQuit.UseVisualStyleBackColor = true;
			btnQuit.Click += this.BtnQuit_Click;
			btnQuit.Cursor = Cursors.Hand;
			// 
			// FrmHome
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(51, 51, 51);
			ClientSize = new Size(1024, 665);
			Controls.Add(btnQuit);
			Controls.Add(btnCredits);
			Controls.Add(btnStat);
			Controls.Add(btnInfini);
			Controls.Add(btnHistore);
			Controls.Add(picBoxHome);
			Controls.Add(lblTitle);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Icon = Properties.Resources.logo;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FrmHome";
			Text = "Le Continentale";
			FormClosing += this.FrmHome_FormClosing;
			((System.ComponentModel.ISupportInitialize)picBoxHome).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}



		#endregion

		private Label lblTitle;
		private PictureBox picBoxHome;
		private Button btnHistore;
		private Button btnInfini;
		private Button btnStat;
		private Button btnCredits;
		private Button btnQuit;
	}
}
