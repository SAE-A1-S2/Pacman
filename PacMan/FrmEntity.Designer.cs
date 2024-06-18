namespace PacMan
{
	partial class FrmEntity
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
			this.SuspendLayout();
			// 
			// FrmEntity
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Name = "FrmEntity";
			Text = "FrmEntity";
			Load += this.Frm_Load;
			Shown += this.Frm_Shown;
			MouseDown += this.Frm_MouseDown;
			MouseMove += this.Frm_MouseMove;
			MouseUp += this.Frm_MouseUp;
			this.ResumeLayout(false);
		}

		#endregion
	}
}