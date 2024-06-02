
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
            lblHistore = new Label();
            lblQuit = new Label();
            lblCredit = new Label();
            lblStat = new Label();
            lblInfini = new Label();
            picBoxHome = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picBoxHome).BeginInit();
            SuspendLayout();
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
            // lblHistore
            // 
            lblHistore.AutoSize = true;
            lblHistore.Cursor = Cursors.Hand;
            lblHistore.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHistore.ForeColor = Color.CornflowerBlue;
            lblHistore.Location = new Point(60, 154);
            lblHistore.Name = "lblHistore";
            lblHistore.Size = new Size(147, 28);
            lblHistore.TabIndex = 1;
            lblHistore.Text = "Mode Histoire";
            lblHistore.Click += lblHistore_Click;
            // 
            // lblQuit
            // 
            lblQuit.AutoSize = true;
            lblQuit.Cursor = Cursors.Hand;
            lblQuit.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblQuit.ForeColor = Color.CornflowerBlue;
            lblQuit.Location = new Point(60, 408);
            lblQuit.Name = "lblQuit";
            lblQuit.Size = new Size(80, 28);
            lblQuit.TabIndex = 2;
            lblQuit.Text = "Quitter";
            lblQuit.Click += lblQuit_Click;
            // 
            // lblCredit
            // 
            lblCredit.AutoSize = true;
            lblCredit.Cursor = Cursors.Hand;
            lblCredit.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCredit.ForeColor = Color.CornflowerBlue;
            lblCredit.Location = new Point(60, 347);
            lblCredit.Name = "lblCredit";
            lblCredit.Size = new Size(78, 28);
            lblCredit.TabIndex = 3;
            lblCredit.Text = "Crédits";
            lblCredit.Click += lblCredit_Click;
            // 
            // lblStat
            // 
            lblStat.AutoSize = true;
            lblStat.Cursor = Cursors.Hand;
            lblStat.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStat.ForeColor = Color.CornflowerBlue;
            lblStat.Location = new Point(60, 287);
            lblStat.Name = "lblStat";
            lblStat.Size = new Size(123, 28);
            lblStat.TabIndex = 4;
            lblStat.Text = "Statistiques";
            lblStat.Click += lblStat_Click;
            // 
            // lblInfini
            // 
            lblInfini.AutoSize = true;
            lblInfini.Cursor = Cursors.Hand;
            lblInfini.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInfini.ForeColor = Color.CornflowerBlue;
            lblInfini.Location = new Point(60, 223);
            lblInfini.Name = "lblInfini";
            lblInfini.Size = new Size(122, 28);
            lblInfini.TabIndex = 5;
            lblInfini.Text = "Mode Infini";
            lblInfini.Click += lblInfini_Click;
            // 
            // picBoxHome
            // 
            picBoxHome.Anchor = AnchorStyles.None;
            picBoxHome.Image = Properties.Resources.homeImage;
            picBoxHome.Location = new Point(353, 154);
            picBoxHome.Name = "picBoxHome";
            picBoxHome.Size = new Size(373, 384);
            picBoxHome.SizeMode = PictureBoxSizeMode.Zoom;
            picBoxHome.TabIndex = 6;
            picBoxHome.TabStop = false;
            // 
            // frmHome
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 51, 51);
            ClientSize = new Size(1024, 665);
            Controls.Add(picBoxHome);
            Controls.Add(lblInfini);
            Controls.Add(lblStat);
            Controls.Add(lblCredit);
            Controls.Add(lblQuit);
            Controls.Add(lblHistore);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = Properties.Resources.logo;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmHome";
            Text = "Le Continentale";
            ((System.ComponentModel.ISupportInitialize)picBoxHome).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Label lblTitle;
        private Label lblHistore;
        private Label lblQuit;
        private Label lblCredit;
        private Label lblStat;
        private Label lblInfini;
        private PictureBox picBoxHome;
    }
}
