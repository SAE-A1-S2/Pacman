namespace PacMan
{
	partial class FrmSettings
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
			lblTitle = new Label();
			lblContShowDialog = new Label();
			chkShowDialog = new CheckBox();
			lblPlayerNameHead = new Label();
			lblPA = new Label();
			lblNP = new Label();
			txtNewName = new TextBox();
			lblName = new Label();
			BtnNoSave = new Button();
			BtnSave = new Button();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblTitle.ForeColor = Color.CornflowerBlue;
			lblTitle.Location = new Point(264, 23);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(147, 29);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Paramètres";
			// 
			// lblContShowDialog
			// 
			lblContShowDialog.AutoSize = true;
			lblContShowDialog.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblContShowDialog.ForeColor = SystemColors.ButtonFace;
			lblContShowDialog.Location = new Point(35, 100);
			lblContShowDialog.Name = "lblContShowDialog";
			lblContShowDialog.Size = new Size(376, 20);
			lblContShowDialog.TabIndex = 1;
			lblContShowDialog.Text = "Afficher la fenetre de bienvenue au lancement du jeu ";
			// 
			// chkShowDialog
			// 
			chkShowDialog.AutoSize = true;
			chkShowDialog.Checked = true;
			chkShowDialog.CheckState = CheckState.Checked;
			chkShowDialog.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			chkShowDialog.ForeColor = Color.CornflowerBlue;
			chkShowDialog.Location = new Point(459, 100);
			chkShowDialog.Name = "chkShowDialog";
			chkShowDialog.Size = new Size(55, 24);
			chkShowDialog.TabIndex = 2;
			chkShowDialog.Text = "Oui";
			chkShowDialog.UseVisualStyleBackColor = true;
			// 
			// lblPlayerNameHead
			// 
			lblPlayerNameHead.AutoSize = true;
			lblPlayerNameHead.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblPlayerNameHead.ForeColor = SystemColors.ButtonFace;
			lblPlayerNameHead.Location = new Point(35, 142);
			lblPlayerNameHead.Name = "lblPlayerNameHead";
			lblPlayerNameHead.Size = new Size(220, 20);
			lblPlayerNameHead.TabIndex = 3;
			lblPlayerNameHead.Text = "Changer le pseudo du joueur : ";
			// 
			// lblPA
			// 
			lblPA.AutoSize = true;
			lblPA.ForeColor = SystemColors.ActiveCaption;
			lblPA.Location = new Point(267, 143);
			lblPA.Name = "lblPA";
			lblPA.Size = new Size(110, 20);
			lblPA.TabIndex = 4;
			lblPA.Text = "Pseudo Actuel :";
			// 
			// lblNP
			// 
			lblNP.AutoSize = true;
			lblNP.ForeColor = SystemColors.ActiveCaption;
			lblNP.Location = new Point(267, 175);
			lblNP.Name = "lblNP";
			lblNP.Size = new Size(127, 20);
			lblNP.TabIndex = 5;
			lblNP.Text = "Nouveau Pseudo :";
			// 
			// txtNewName
			// 
			txtNewName.BorderStyle = BorderStyle.FixedSingle;
			txtNewName.Location = new Point(421, 173);
			txtNewName.MaxLength = 15;
			txtNewName.Name = "txtNewName";
			txtNewName.Size = new Size(153, 27);
			txtNewName.TabIndex = 6;
			// 
			// lblName
			// 
			lblName.AutoSize = true;
			lblName.ForeColor = SystemColors.ButtonFace;
			lblName.Location = new Point(421, 142);
			lblName.Name = "lblName";
			lblName.Size = new Size(0, 20);
			lblName.TabIndex = 7;
			// 
			// BtnNoSave
			// 
			BtnNoSave.BackColor = Color.CornflowerBlue;
			BtnNoSave.Cursor = Cursors.Hand;
			BtnNoSave.FlatAppearance.BorderSize = 0;
			BtnNoSave.FlatStyle = FlatStyle.Flat;
			BtnNoSave.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			BtnNoSave.ForeColor = SystemColors.ActiveCaptionText;
			BtnNoSave.Location = new Point(12, 266);
			BtnNoSave.Name = "BtnNoSave";
			BtnNoSave.Size = new Size(265, 29);
			BtnNoSave.TabIndex = 8;
			BtnNoSave.Text = "Continuer sans sauvegarder";
			BtnNoSave.UseVisualStyleBackColor = false;
			BtnNoSave.Click += this.BtnNoSave_Click;
			// 
			// BtnSave
			// 
			BtnSave.BackColor = Color.CornflowerBlue;
			BtnSave.Cursor = Cursors.Hand;
			BtnSave.FlatAppearance.BorderSize = 0;
			BtnSave.FlatStyle = FlatStyle.Flat;
			BtnSave.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			BtnSave.ForeColor = SystemColors.ActiveCaptionText;
			BtnSave.Location = new Point(421, 266);
			BtnSave.Name = "BtnSave";
			BtnSave.Size = new Size(235, 29);
			BtnSave.TabIndex = 9;
			BtnSave.Text = "Sauvegarder et continuer";
			BtnSave.UseVisualStyleBackColor = false;
			BtnSave.Click += this.BtnSave_Click;
			// 
			// FrmSettings
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(51, 51, 51);
			ClientSize = new Size(668, 307);
			Controls.Add(BtnSave);
			Controls.Add(BtnNoSave);
			Controls.Add(lblName);
			Controls.Add(txtNewName);
			Controls.Add(lblNP);
			Controls.Add(lblPA);
			Controls.Add(lblPlayerNameHead);
			Controls.Add(chkShowDialog);
			Controls.Add(lblContShowDialog);
			Controls.Add(lblTitle);
			FormBorderStyle = FormBorderStyle.None;
			Icon = Properties.Resources.logo;
			KeyPreview = true;
			MaximizeBox = false;
			Name = "FrmSettings";
			Text = "FrmSettings";
			Load += this.FrmSettings_Load;
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private Label lblTitle;
		private Label lblContShowDialog;
		private CheckBox chkShowDialog;
		private Label lblPlayerNameHead;
		private Label lblPA;
		private Label lblNP;
		private TextBox txtNewName;
		private Label lblName;
		private Button BtnNoSave;
		private Button BtnSave;
	}
}