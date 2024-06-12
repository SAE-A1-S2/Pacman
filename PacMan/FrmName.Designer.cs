namespace PacMan
{
	partial class FrmName
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
			BtnCancel = new Button();
			BtnCtn = new Button();
			TxtPlyName = new TextBox();
			label1 = new Label();
			chkName = new CheckBox();
			this.SuspendLayout();
			// 
			// BtnCancel
			// 
			BtnCancel.BackColor = Color.CornflowerBlue;
			BtnCancel.Cursor = Cursors.Hand;
			BtnCancel.FlatStyle = FlatStyle.Flat;
			BtnCancel.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			BtnCancel.ForeColor = SystemColors.ActiveCaptionText;
			BtnCancel.Location = new Point(423, 222);
			BtnCancel.Name = "BtnCancel";
			BtnCancel.Size = new Size(94, 29);
			BtnCancel.TabIndex = 0;
			BtnCancel.Text = "Annuler";
			BtnCancel.UseVisualStyleBackColor = false;
			BtnCancel.Click += this.BtnCancel_Click;
			// 
			// BtnCtn
			// 
			BtnCtn.BackColor = Color.CornflowerBlue;
			BtnCtn.Cursor = Cursors.Hand;
			BtnCtn.Enabled = false;
			BtnCtn.FlatStyle = FlatStyle.Flat;
			BtnCtn.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			BtnCtn.Location = new Point(535, 222);
			BtnCtn.Name = "BtnCtn";
			BtnCtn.Size = new Size(105, 29);
			BtnCtn.TabIndex = 1;
			BtnCtn.Text = "Continuer";
			BtnCtn.UseVisualStyleBackColor = false;
			BtnCtn.Click += this.BtnCtn_Click;
			// 
			// TxtPlyName
			// 
			TxtPlyName.BorderStyle = BorderStyle.FixedSingle;
			TxtPlyName.Location = new Point(50, 112);
			TxtPlyName.MaxLength = 15;
			TxtPlyName.Name = "TxtPlyName";
			TxtPlyName.Size = new Size(515, 27);
			TxtPlyName.TabIndex = 2;
			TxtPlyName.TextChanged += this.TxtPlyName_TextChanged;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.ForeColor = SystemColors.ButtonHighlight;
			label1.Location = new Point(50, 70);
			label1.Name = "label1";
			label1.Size = new Size(355, 20);
			label1.TabIndex = 3;
			label1.Text = "Veuillez rentrer votre Psuedo pour commencer le jeu";
			// 
			// chkName
			// 
			chkName.AutoSize = true;
			chkName.ForeColor = SystemColors.ButtonFace;
			chkName.Location = new Point(12, 225);
			chkName.Name = "chkName";
			chkName.Size = new Size(295, 24);
			chkName.TabIndex = 4;
			chkName.Text = "Utiliser le même pseudo à chaque fois ?";
			chkName.UseVisualStyleBackColor = true;
			// 
			// FrmName
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(51, 51, 51);
			ClientSize = new Size(652, 274);
			Controls.Add(chkName);
			Controls.Add(label1);
			Controls.Add(TxtPlyName);
			Controls.Add(BtnCtn);
			Controls.Add(BtnCancel);
			FormBorderStyle = FormBorderStyle.None;
			Icon = Properties.Resources.logo;
			KeyPreview = true;
			MaximizeBox = false;
			Name = "FrmName";
			Text = "FrmName";
			Load += this.FrmName_Load;
			KeyDown += this.TxtName_KeyDown;
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private Button BtnCancel;
		private Button BtnCtn;
		private TextBox TxtPlyName;
		private Label label1;
		private CheckBox chkName;
	}
}