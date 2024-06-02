namespace PacMan
{
	partial class FrmPopUp
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
			btnOKPopup = new Button();
			chkPopup = new CheckBox();
			lblPopWelcome = new Label();
			lblListeGroup = new Label();
			lblUp = new Label();
			lblDown = new Label();
			lblLeft = new Label();
			lblUpC = new Label();
			lblDownC = new Label();
			lblRight = new Label();
			lblLeftC = new Label();
			lblRightC = new Label();
			lblPause = new Label();
			lblPauseC = new Label();
			lblUseBonus = new Label();
			lblUseBonusC = new Label();
			this.SuspendLayout();
			// 
			// btnOKPopup
			// 
			btnOKPopup.BackColor = Color.CornflowerBlue;
			btnOKPopup.Cursor = Cursors.Hand;
			btnOKPopup.FlatStyle = FlatStyle.Flat;
			btnOKPopup.Location = new Point(474, 323);
			btnOKPopup.Name = "btnOKPopup";
			btnOKPopup.Size = new Size(94, 29);
			btnOKPopup.TabIndex = 0;
			btnOKPopup.Text = "OK";
			btnOKPopup.UseVisualStyleBackColor = false;
			btnOKPopup.Click += this.btnOKPopup_Click;
			// 
			// chkPopup
			// 
			chkPopup.AutoSize = true;
			chkPopup.Cursor = Cursors.Hand;
			chkPopup.ForeColor = Color.CornflowerBlue;
			chkPopup.Location = new Point(12, 328);
			chkPopup.Name = "chkPopup";
			chkPopup.Size = new Size(232, 24);
			chkPopup.TabIndex = 1;
			chkPopup.Text = "Ne plus afficher au démarrage";
			chkPopup.UseVisualStyleBackColor = true;
			// 
			// lblPopWelcome
			// 
			lblPopWelcome.AutoSize = true;
			lblPopWelcome.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblPopWelcome.ForeColor = Color.CornflowerBlue;
			lblPopWelcome.Location = new Point(166, 9);
			lblPopWelcome.Name = "lblPopWelcome";
			lblPopWelcome.Size = new Size(243, 23);
			lblPopWelcome.TabIndex = 2;
			lblPopWelcome.Text = "Bienvenue sur Le Continentale";
			lblPopWelcome.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblListeGroup
			// 
			lblListeGroup.AutoSize = true;
			lblListeGroup.ForeColor = Color.CornflowerBlue;
			lblListeGroup.Location = new Point(35, 67);
			lblListeGroup.Name = "lblListeGroup";
			lblListeGroup.Size = new Size(131, 20);
			lblListeGroup.TabIndex = 3;
			lblListeGroup.Text = "Liste des contrôles";
			// 
			// lblUp
			// 
			lblUp.AutoSize = true;
			lblUp.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblUp.ForeColor = SystemColors.ButtonFace;
			lblUp.Location = new Point(35, 107);
			lblUp.Name = "lblUp";
			lblUp.Size = new Size(54, 20);
			lblUp.TabIndex = 4;
			lblUp.Text = "Haut : ";
			// 
			// lblDown
			// 
			lblDown.AutoSize = true;
			lblDown.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblDown.ForeColor = SystemColors.ButtonFace;
			lblDown.Location = new Point(293, 107);
			lblDown.Name = "lblDown";
			lblDown.Size = new Size(44, 20);
			lblDown.TabIndex = 5;
			lblDown.Text = "Bas : ";
			// 
			// lblLeft
			// 
			lblLeft.AutoSize = true;
			lblLeft.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblLeft.ForeColor = SystemColors.ButtonFace;
			lblLeft.Location = new Point(35, 159);
			lblLeft.Name = "lblLeft";
			lblLeft.Size = new Size(72, 20);
			lblLeft.TabIndex = 6;
			lblLeft.Text = "Gauche : ";
			// 
			// lblUpC
			// 
			lblUpC.AutoSize = true;
			lblUpC.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblUpC.ForeColor = Color.CornflowerBlue;
			lblUpC.Location = new Point(95, 107);
			lblUpC.Name = "lblUpC";
			lblUpC.Size = new Size(106, 20);
			lblUpC.TabIndex = 7;
			lblUpC.Text = "Z ou ArrowUp";
			// 
			// lblDownC
			// 
			lblDownC.AutoSize = true;
			lblDownC.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblDownC.ForeColor = Color.CornflowerBlue;
			lblDownC.Location = new Point(343, 107);
			lblDownC.Name = "lblDownC";
			lblDownC.Size = new Size(125, 20);
			lblDownC.TabIndex = 8;
			lblDownC.Text = "S ou ArrowDown";
			// 
			// lblRight
			// 
			lblRight.AutoSize = true;
			lblRight.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblRight.ForeColor = SystemColors.ButtonFace;
			lblRight.Location = new Point(293, 159);
			lblRight.Name = "lblRight";
			lblRight.Size = new Size(64, 20);
			lblRight.TabIndex = 9;
			lblRight.Text = "Droite : ";
			// 
			// lblLeftC
			// 
			lblLeftC.AutoSize = true;
			lblLeftC.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblLeftC.ForeColor = Color.CornflowerBlue;
			lblLeftC.Location = new Point(113, 159);
			lblLeftC.Name = "lblLeftC";
			lblLeftC.Size = new Size(113, 20);
			lblLeftC.TabIndex = 10;
			lblLeftC.Text = "Q ou ArrowLeft";
			// 
			// lblRightC
			// 
			lblRightC.AutoSize = true;
			lblRightC.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblRightC.ForeColor = Color.CornflowerBlue;
			lblRightC.Location = new Point(363, 159);
			lblRightC.Name = "lblRightC";
			lblRightC.Size = new Size(124, 20);
			lblRightC.TabIndex = 11;
			lblRightC.Text = "D ou ArrowRight";
			// 
			// lblPause
			// 
			lblPause.AutoSize = true;
			lblPause.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblPause.ForeColor = SystemColors.ButtonFace;
			lblPause.Location = new Point(35, 213);
			lblPause.Name = "lblPause";
			lblPause.Size = new Size(61, 20);
			lblPause.TabIndex = 12;
			lblPause.Text = "Pause : ";
			// 
			// lblPauseC
			// 
			lblPauseC.AutoSize = true;
			lblPauseC.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblPauseC.ForeColor = Color.CornflowerBlue;
			lblPauseC.Location = new Point(102, 213);
			lblPauseC.Name = "lblPauseC";
			lblPauseC.Size = new Size(65, 20);
			lblPauseC.TabIndex = 13;
			lblPauseC.Text = "P ou Esc";
			// 
			// lblUseBonus
			// 
			lblUseBonus.AutoSize = true;
			lblUseBonus.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblUseBonus.ForeColor = SystemColors.ButtonFace;
			lblUseBonus.Location = new Point(293, 213);
			lblUseBonus.Name = "lblUseBonus";
			lblUseBonus.Size = new Size(59, 20);
			lblUseBonus.TabIndex = 14;
			lblUseBonus.Text = "Bonus :";
			// 
			// lblUseBonusC
			// 
			lblUseBonusC.AutoSize = true;
			lblUseBonusC.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblUseBonusC.ForeColor = Color.CornflowerBlue;
			lblUseBonusC.Location = new Point(358, 213);
			lblUseBonusC.Name = "lblUseBonusC";
			lblUseBonusC.Size = new Size(47, 20);
			lblUseBonusC.TabIndex = 15;
			lblUseBonusC.Text = "space";
			// 
			// FrmPopUp
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(51, 51, 51);
			ClientSize = new Size(595, 364);
			ControlBox = false;
			Controls.Add(lblUseBonusC);
			Controls.Add(lblUseBonus);
			Controls.Add(lblPauseC);
			Controls.Add(lblPause);
			Controls.Add(lblRightC);
			Controls.Add(lblLeftC);
			Controls.Add(lblRight);
			Controls.Add(lblDownC);
			Controls.Add(lblUpC);
			Controls.Add(lblLeft);
			Controls.Add(lblDown);
			Controls.Add(lblUp);
			Controls.Add(lblListeGroup);
			Controls.Add(lblPopWelcome);
			Controls.Add(chkPopup);
			Controls.Add(btnOKPopup);
			FormBorderStyle = FormBorderStyle.None;
			Name = "FrmPopUp";
			Icon = Properties.Resources.logo;
			Load += this.FrmPopUp_Load;
			MouseDown += this.PopUpForm_MouseDown;
			MouseMove += this.PopUpForm_MouseMove;
			MouseUp += this.PopUpForm_MouseUp;
			this.ResumeLayout(false);
			this.PerformLayout();
		}



		#endregion

		private Button btnOKPopup;
		private CheckBox chkPopup;
		private Label lblPopWelcome;
		private Label lblListeGroup;
		private Label lblUp;
		private Label lblDown;
		private Label lblLeft;
		private Label lblUpC;
		private Label lblDownC;
		private Label lblRight;
		private Label lblLeftC;
		private Label lblRightC;
		private Label lblPause;
		private Label lblPauseC;
		private Label lblUseBonus;
		private Label lblUseBonusC;
	}
}