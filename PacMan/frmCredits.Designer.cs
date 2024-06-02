namespace PacMan
{
	partial class FrmCredits
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
			lblDesc = new Label();
			lblHammed = new Label();
			lblGuerby = new Label();
			lblAdrien = new Label();
			lblCédric = new Label();
			lblAlexis = new Label();
			lblArthur = new Label();
			lblsession = new Label();
			lblSessionDate = new Label();
			this.SuspendLayout();
			// 
			// btnRetour
			// 
			btnRetour.BackColor = Color.CornflowerBlue;
			btnRetour.Cursor = Cursors.Hand;
			btnRetour.FlatStyle = FlatStyle.Flat;
			btnRetour.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnRetour.ForeColor = Color.Black;
			btnRetour.Location = new Point(46, 325);
			btnRetour.Name = "btnRetour";
			btnRetour.Size = new Size(94, 29);
			btnRetour.TabIndex = 0;
			btnRetour.Text = "Retour";
			btnRetour.UseVisualStyleBackColor = false;
			btnRetour.Click += this.btnRetour_Click;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblTitle.ForeColor = Color.CornflowerBlue;
			lblTitle.Location = new Point(261, 9);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(89, 31);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Crédits";
			// 
			// lblDesc
			// 
			lblDesc.AutoSize = true;
			lblDesc.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblDesc.ForeColor = Color.CornflowerBlue;
			lblDesc.Location = new Point(46, 66);
			lblDesc.Name = "lblDesc";
			lblDesc.Size = new Size(459, 23);
			lblDesc.TabIndex = 2;
			lblDesc.Text = "Étudiants de première année en BUT Info à l'IUT d'Amiens ";
			// 
			// lblHammed
			// 
			lblHammed.AutoSize = true;
			lblHammed.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblHammed.ForeColor = SystemColors.ButtonFace;
			lblHammed.Location = new Point(46, 107);
			lblHammed.Name = "lblHammed";
			lblHammed.Size = new Size(135, 23);
			lblHammed.TabIndex = 3;
			lblHammed.Text = "ABASS Hammed";
			// 
			// lblGuerby
			// 
			lblGuerby.AutoSize = true;
			lblGuerby.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblGuerby.ForeColor = SystemColors.ButtonFace;
			lblGuerby.Location = new Point(277, 219);
			lblGuerby.Name = "lblGuerby";
			lblGuerby.Size = new Size(150, 23);
			lblGuerby.TabIndex = 4;
			lblGuerby.Text = "NAHARRO Guerby";
			// 
			// lblAdrien
			// 
			lblAdrien.AutoSize = true;
			lblAdrien.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblAdrien.ForeColor = SystemColors.ButtonFace;
			lblAdrien.Location = new Point(277, 166);
			lblAdrien.Name = "lblAdrien";
			lblAdrien.Size = new Size(120, 23);
			lblAdrien.TabIndex = 5;
			lblAdrien.Text = "GODET Adrien";
			// 
			// lblCédric
			// 
			lblCédric.AutoSize = true;
			lblCédric.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblCédric.ForeColor = SystemColors.ButtonFace;
			lblCédric.Location = new Point(46, 219);
			lblCédric.Name = "lblCédric";
			lblCédric.Size = new Size(98, 23);
			lblCédric.TabIndex = 6;
			lblCédric.Text = "MAS Cédric";
			// 
			// lblAlexis
			// 
			lblAlexis.AutoSize = true;
			lblAlexis.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblAlexis.ForeColor = SystemColors.ButtonFace;
			lblAlexis.Location = new Point(46, 166);
			lblAlexis.Name = "lblAlexis";
			lblAlexis.Size = new Size(114, 23);
			lblAlexis.TabIndex = 7;
			lblAlexis.Text = "DOHER Alexis";
			// 
			// lblArthur
			// 
			lblArthur.AutoSize = true;
			lblArthur.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblArthur.ForeColor = SystemColors.ButtonFace;
			lblArthur.Location = new Point(277, 107);
			lblArthur.Name = "lblArthur";
			lblArthur.Size = new Size(149, 23);
			lblArthur.TabIndex = 8;
			lblArthur.Text = "AURIGNAC Arthur";
			// 
			// lblsession
			// 
			lblsession.AutoSize = true;
			lblsession.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblsession.ForeColor = SystemColors.ButtonFace;
			lblsession.Location = new Point(187, 280);
			lblsession.Name = "lblsession";
			lblsession.Size = new Size(69, 20);
			lblsession.TabIndex = 9;
			lblsession.Text = "session : ";
			// 
			// lblSessionDate
			// 
			lblSessionDate.AutoSize = true;
			lblSessionDate.ForeColor = Color.CornflowerBlue;
			lblSessionDate.Location = new Point(262, 280);
			lblSessionDate.Name = "lblSessionDate";
			lblSessionDate.Size = new Size(79, 20);
			lblSessionDate.TabIndex = 10;
			lblSessionDate.Text = "2023/2024";
			// 
			// FrmCredits
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(51, 51, 51);
			ClientSize = new Size(616, 381);
			Controls.Add(lblSessionDate);
			Controls.Add(lblsession);
			Controls.Add(lblArthur);
			Controls.Add(lblAlexis);
			Controls.Add(lblCédric);
			Controls.Add(lblAdrien);
			Controls.Add(lblGuerby);
			Controls.Add(lblHammed);
			Controls.Add(lblDesc);
			Controls.Add(lblTitle);
			Controls.Add(btnRetour);
			ForeColor = SystemColors.ControlDarkDark;
			FormBorderStyle = FormBorderStyle.None;
			Icon = Properties.Resources.logo;
			Name = "FrmCredits";
			Text = "Credits";
			Load += this.FrmCredits_Load;
			MouseDown += this.Credits_MouseDown;
			MouseMove += this.Credits_MouseMove;
			MouseUp += this.Credits_MouseUp;
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private Button btnRetour;
		private Label lblTitle;
		private Label lblDesc;
		private Label lblHammed;
		private Label lblGuerby;
		private Label lblAdrien;
		private Label lblCédric;
		private Label lblAlexis;
		private Label lblArthur;
		private Label lblsession;
		private Label lblSessionDate;
	}
}