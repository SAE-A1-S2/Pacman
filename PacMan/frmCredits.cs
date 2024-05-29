using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public partial class frmCredits : Form
    {

        private bool isDragging = false;
        private Point lastCursorPosition;
        public frmCredits()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

			using Pen pen = new(Color.White, 1);
			e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
		}

        private void btnRetour_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Credits_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPosition = PointToScreen(e.Location);
            }
        }

        private void Credits_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentCursorPosition = PointToScreen(e.Location);
                Location = new Point(
                                       Location.X + (currentCursorPosition.X - lastCursorPosition.X),
                                                          Location.Y + (currentCursorPosition.Y - lastCursorPosition.Y));
                lastCursorPosition = currentCursorPosition;
            }
        }

        private void Credits_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
                isDragging = false; 
        }
    }
}
