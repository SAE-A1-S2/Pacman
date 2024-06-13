namespace PacMan
{
	partial class FrmPause
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
			lblPause = new Label();
			btnReprendre = new Button();
			btnQuitSave = new Button();
			this.SuspendLayout();
			// 
			// lblPause
			// 
			lblPause.AutoSize = true;
			lblPause.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblPause.ForeColor = Color.CornflowerBlue;
			lblPause.Location = new Point(148, 42);
			lblPause.Name = "lblPause";
			lblPause.Size = new Size(92, 38);
			lblPause.TabIndex = 0;
			lblPause.Text = "Pause";
			// 
			// btnReprendre
			// 
			btnReprendre.Cursor = Cursors.Hand;
			btnReprendre.FlatAppearance.BorderSize = 0;
			btnReprendre.FlatStyle = FlatStyle.Flat;
			btnReprendre.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnReprendre.ForeColor = Color.DodgerBlue;
			btnReprendre.Location = new Point(89, 141);
			btnReprendre.Name = "btnReprendre";
			btnReprendre.Size = new Size(206, 70);
			btnReprendre.TabIndex = 1;
			btnReprendre.Text = "Reprendre";
			btnReprendre.UseVisualStyleBackColor = true;
			btnReprendre.Click += this.btnReprendre_Click;
			// 
			// btnQuitSave
			// 
			btnQuitSave.BackColor = Color.FromArgb(51, 51, 51);
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
			// FrmPause
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(51, 51, 51);
			ClientSize = new Size(402, 381);
			Controls.Add(btnQuitSave);
			Controls.Add(btnReprendre);
			Controls.Add(lblPause);
			FormBorderStyle = FormBorderStyle.None;
			Icon = Properties.Resources.logo;
			Name = "FrmPause";
			Text = "FrmPause";
			Load += this.FrmPause_Load;
			MouseDown += this.FrmPause_MouseDown;
			MouseMove += this.FrmPause_MouseMove;
			MouseUp += this.FrmPause_MouseUp;
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private Label lblPause;
		private Button btnReprendre;
		private Button btnQuitSave;
	}
}