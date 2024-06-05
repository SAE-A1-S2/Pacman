namespace PacMan
{
	partial class FrmNotif
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
			btnOui = new Button();
			btnNon = new Button();
			lblMsg = new Label();
			lblDetails = new Label();
			this.SuspendLayout();
			// 
			// btnOui
			// 
			btnOui.BackColor = Color.CornflowerBlue;
			btnOui.Cursor = Cursors.Hand;
			btnOui.FlatStyle = FlatStyle.Flat;
			btnOui.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnOui.ForeColor = SystemColors.ActiveCaptionText;
			btnOui.Location = new Point(241, 224);
			btnOui.Name = "btnOui";
			btnOui.Size = new Size(94, 29);
			btnOui.TabIndex = 0;
			btnOui.Text = "Oui";
			btnOui.UseVisualStyleBackColor = false;
			btnOui.Click += this.btnOui_Click;
			// 
			// btnNon
			// 
			btnNon.BackColor = Color.CornflowerBlue;
			btnNon.Cursor = Cursors.Hand;
			btnNon.FlatStyle = FlatStyle.Flat;
			btnNon.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnNon.ForeColor = SystemColors.ActiveCaptionText;
			btnNon.Location = new Point(358, 224);
			btnNon.Name = "btnNon";
			btnNon.Size = new Size(94, 29);
			btnNon.TabIndex = 1;
			btnNon.Text = "Non";
			btnNon.UseVisualStyleBackColor = false;
			btnNon.Click += this.btnNon_Click;
			// 
			// lblMsg
			// 
			lblMsg.AutoSize = true;
			lblMsg.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblMsg.ForeColor = SystemColors.ButtonFace;
			lblMsg.Location = new Point(12, 74);
			lblMsg.Name = "lblMsg";
			lblMsg.Size = new Size(388, 28);
			lblMsg.TabIndex = 2;
			lblMsg.Text = "Vous voulez vraiment quitter la partie ?";
			// 
			// lblDetails
			// 
			lblDetails.AutoSize = true;
			lblDetails.Font = new Font("Segoe UI", 10.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
			lblDetails.ForeColor = SystemColors.ButtonHighlight;
			lblDetails.Location = new Point(12, 113);
			lblDetails.Name = "lblDetails";
			lblDetails.Size = new Size(249, 23);
			lblDetails.TabIndex = 3;
			lblDetails.Text = "La partie ne sera pas sauvegardé";
			// 
			// FrmNotif
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(51, 51, 51);
			ClientSize = new Size(464, 265);
			Controls.Add(lblDetails);
			Controls.Add(lblMsg);
			Controls.Add(btnNon);
			Controls.Add(btnOui);
			FormBorderStyle = FormBorderStyle.None;
			Icon = Properties.Resources.logo;
			Name = "FrmNotif";
			Text = "FrmNotif";
			Load += this.FrmNotif_Load;
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private Button btnOui;
		private Button btnNon;
		private Label lblMsg;
		private Label lblDetails;
	}
}