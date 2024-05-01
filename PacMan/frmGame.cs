using Engine;
using Engine.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public partial class frmGame : Form
    {

        private GameManager gameManager;

        public frmGame(bool isStoryMode = false)
        {
            InitializeComponent();
            gameManager = new GameManager(isStoryMode);
        }

        private void frmGame_Closed(object sender, EventArgs e)
        {
            Application.OpenForms[0]?.Show();
        }


        public void DisplayMaze(Cell[,] maze)
        {
            int cellSize = 20;
            Bitmap bmp = new(maze.GetLength(0) * cellSize, maze.GetLength(1) * cellSize);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int x = 0; x < maze.GetLength(0); x++)
                {
                    for (int y = 0; y < maze.GetLength(1); y++)
                    {
                        Color color = maze[x, y] == Cell.Wall ? Color.Black : maze[x, y] == Cell.Ghost ? Color.Blue : maze[x, y] == Cell.John ? Color.Yellow : Color.White;
                        g.FillRectangle(new SolidBrush(color), x * cellSize, y * cellSize, cellSize, cellSize);
                    }
                }
            }
            imgMap.Image = bmp;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            gameManager.HandleKeyInput(keyData);
            DisplayMaze(gameManager.LevelManager.LevelMap);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmGame_Load(object sender, EventArgs e)
        {
            DisplayMaze(gameManager.LevelManager.LevelMap);
        }

        private void frmGame_KeyDown(object sender, KeyEventArgs e)
        {
            // gameManager.HandleKeyInput(e.KeyCode);
            // DisplayMaze(gameManager.LevelManager.LevelMap);
        }

        private void TmrGhost_Tick(object sender, EventArgs e)
        {
            gameManager.LevelManager.losef.MoveRandom();
            gameManager.LevelManager.marquis.MoveRandom();
            gameManager.LevelManager.viggo.MoveRandom();
            gameManager.LevelManager.caine.MoveRandom();
            DisplayMaze(gameManager.LevelManager.LevelMap);
        }
    }
}
