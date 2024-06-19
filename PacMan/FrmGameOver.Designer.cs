namespace PacMan
{
	partial class FrmGameOver
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
			lblNextLevel = new Label();
			BtnNxtLvl = new Button();
			btnQuit = new Button();
			this.SuspendLayout();
			// 
			// lblNextLevel
			// 
			lblNextLevel.AutoSize = true;
			lblNextLevel.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblNextLevel.ForeColor = Color.CornflowerBlue;
			lblNextLevel.Location = new Point(69, 36);
			lblNextLevel.Name = "lblNextLevel";
			lblNextLevel.Size = new Size(240, 38);
			lblNextLevel.TabIndex = 0;
			lblNextLevel.Text = "Niveau supérieur";
			// 
			// BtnNxtLvl
			// 
			BtnNxtLvl.Cursor = Cursors.Hand;
			BtnNxtLvl.FlatAppearance.BorderSize = 0;
			BtnNxtLvl.FlatStyle = FlatStyle.Flat;
			BtnNxtLvl.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			BtnNxtLvl.ForeColor = Color.DodgerBlue;
			BtnNxtLvl.Location = new Point(89, 141);
			BtnNxtLvl.Name = "BtnNxtLvl";
			BtnNxtLvl.Size = new Size(206, 70);
			BtnNxtLvl.TabIndex = 1;
			BtnNxtLvl.Text = "Prochain niveau";
			BtnNxtLvl.UseVisualStyleBackColor = true;
			BtnNxtLvl.Click += this.BtnNxtLvl_Click;
			// 
			// btnQuit
			// 
			btnQuit.BackColor = Color.FromArgb(51, 51, 51);
			btnQuit.Cursor = Cursors.Hand;
			btnQuit.FlatAppearance.BorderSize = 0;
			btnQuit.FlatStyle = FlatStyle.Flat;
			btnQuit.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnQuit.ForeColor = Color.DodgerBlue;
			btnQuit.Location = new Point(89, 247);
			btnQuit.Name = "btnQuit";
			btnQuit.Size = new Size(206, 74);
			btnQuit.TabIndex = 2;
			btnQuit.Text = "Quitter";
			btnQuit.UseVisualStyleBackColor = false;
			btnQuit.Click += this.btnQuit_Click;
			// 
			// FrmGameOver
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(51, 51, 51);
			ClientSize = new Size(402, 381);
			Controls.Add(btnQuit);
			Controls.Add(BtnNxtLvl);
			Controls.Add(lblNextLevel);
			FormBorderStyle = FormBorderStyle.None;
			Icon = Properties.Resources.logo;
			Name = "FrmGameOver";
			Text = "FrmGameOver";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private Label lblNextLevel;
		private Button BtnNxtLvl;
		private Button btnQuit;
	}
}