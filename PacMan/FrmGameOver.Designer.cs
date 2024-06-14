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
			btnQuitSave = new Button();
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
			// btnQuitSave
			// 
			btnQuitSave.BackColor = Color.FromArgb(51, 51, 51);
			btnQuitSave.Cursor = Cursors.Hand;
			btnQuitSave.FlatAppearance.BorderSize = 0;
			btnQuitSave.FlatStyle = FlatStyle.Flat;
			btnQuitSave.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnQuitSave.ForeColor = Color.DodgerBlue;
			btnQuitSave.Location = new Point(89, 247);
			btnQuitSave.Name = "btnQuitSave";
			btnQuitSave.Size = new Size(206, 74);
			btnQuitSave.TabIndex = 2;
			btnQuitSave.Text = "Sauvegarder et quitter";
			btnQuitSave.UseVisualStyleBackColor = false;
			// 
			// FrmGameOver
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(51, 51, 51);
			ClientSize = new Size(402, 381);
			Controls.Add(btnQuitSave);
			Controls.Add(BtnNxtLvl);
			Controls.Add(lblNextLevel);
			FormBorderStyle = FormBorderStyle.None;
			Icon = Properties.Resources.logo;
			Name = "FrmGameOver";
			Text = "FrmPause";
			Load += this.FrmPause_Load;
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private Label lblNextLevel;
		private Button BtnNxtLvl;
		private Button btnQuitSave;
	}
}