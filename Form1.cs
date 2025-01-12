using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris_2
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer gameTimer;
        private int score;
        private Shape currentShape;
        private Shape nextShape;
        private int[,] grid;
        private const int GridWidth = 20;
        private const int GridHeight = 30;
        private const int CellSize = 20;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            score = 0;
            grid = new int[GridWidth, GridHeight];
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
            SpawnNewShape();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            MoveShapeDown();
            gamePanel.Invalidate();
        }

        private void MoveShapeDown()
        {
            if (CanMove(currentShape, 0, 1))
            {
                currentShape.Move(0, 1);
            }
            else
            {
                MergeShapeToGrid();
                ClearCompletedLines();
                SpawnNewShape();
                if (!CanMove(currentShape, 0, 0))
                {
                    GameOver();
                }
            }
        }

        private void MergeShapeToGrid()
        {
            foreach (Point p in currentShape.GetBlocks())
            {
                grid[p.X, p.Y] = 1;
            }
        }

        private void ClearCompletedLines()
        {
            for (int y = 0; y < GridHeight; y++)
            {
                bool isLineComplete = true;
                for (int x = 0; x < GridWidth; x++)
                {
                    if (grid[x, y] == 0)
                    {
                        isLineComplete = false;
                        break;
                    }
                }
                if (isLineComplete)
                {
                    ClearLine(y);
                    score += 100;
                    scoreLabel.Text = "Score: " + score;
                }
            }
        }

        private void ClearLine(int line)
        {
            for (int y = line; y > 0; y--)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    grid[x, y] = grid[x, y - 1];
                }
            }
            for (int x = 0; x < GridWidth; x++)
            {
                grid[x, 0] = 0;
            }
        }

        private void SpawnNewShape()
        {
            currentShape = nextShape ?? Shape.GetRandomShape();
            nextShape = Shape.GetRandomShape();
            nextShapeLabel.Text = "Next Shape: " + nextShape.GetType().Name;
            nextShapePanel.Invalidate();
        }

        private bool CanMove(Shape shape, int dx, int dy)
        {
            foreach (Point p in shape.GetBlocks())
            {
                int newX = p.X + dx;
                int newY = p.Y + dy;
                if (newX < 0 || newX >= GridWidth || newY < 0 || newY >= GridHeight || grid[newX, newY] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void GameOver()
        {
            gameTimer.Stop();
            MessageBox.Show("Game Over! Your score: " + score);
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    if (CanMove(currentShape, -1, 0)) currentShape.Move(-1, 0);
                    break;
                case Keys.Right:
                    if (CanMove(currentShape, 1, 0)) currentShape.Move(1, 0);
                    break;
                case Keys.Down:
                    if (CanMove(currentShape, 0, 1)) currentShape.Move(0, 1);
                    break;
                case Keys.Up:
                    currentShape.Rotate();
                    if (!CanMove(currentShape, 0, 0)) currentShape.RotateBack();
                    break;
            }
            gamePanel.Invalidate();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    if (grid[x, y] != 0)
                    {
                        g.FillRectangle(Brushes.Blue, x * CellSize, y * CellSize, CellSize, CellSize);
                        g.DrawRectangle(Pens.Black, x * CellSize, y * CellSize, CellSize, CellSize);
                    }
                }
            }
            foreach (Point p in currentShape.GetBlocks())
            {
                g.FillRectangle(Brushes.Red, p.X * CellSize, p.Y * CellSize, CellSize, CellSize);
                g.DrawRectangle(Pens.Black, p.X * CellSize, p.Y * CellSize, CellSize, CellSize);
            }
        }

        private void NextShapePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Point p in nextShape.GetBlocks())
            {
                g.FillRectangle(Brushes.Red, (p.X + 1) * CellSize, (p.Y + 1) * CellSize, CellSize, CellSize);
                g.DrawRectangle(Pens.Black, (p.X + 1) * CellSize, (p.Y + 1) * CellSize, CellSize, CellSize);
            }
        }
    }

    public class Shape
    {
        private List<Point> blocks;
        private Point position;

        public Shape(List<Point> blocks)
        {
            this.blocks = new List<Point>(blocks);
            this.position = new Point(4, 0);
        }

        public void Move(int dx, int dy)
        {
            position.X += dx;
            position.Y += dy;
        }

        public void Rotate()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                Point temp = blocks[i];
                blocks[i] = new Point(temp.Y, -temp.X);
            }
        }

        public void RotateBack()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                Point temp = blocks[i];
                blocks[i] = new Point(-temp.Y, temp.X);
            }
        }

        public List<Point> GetBlocks()
        {
            List<Point> result = new List<Point>();
            foreach (Point p in blocks)
            {
                result.Add(new Point(p.X + position.X, p.Y + position.Y));
            }
            return result;
        }

        public static Shape GetRandomShape()
        {
            Random rand = new Random();
            int shapeType = rand.Next(0, 7);
            switch (shapeType)
            {
                case 0:
                    return new Shape(new List<Point> { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0) });
                case 1:
                    return new Shape(new List<Point> { new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(1, 1) });
                case 2:
                    return new Shape(new List<Point> { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(1, 1) });
                case 3:
                    return new Shape(new List<Point> { new Point(1, 0), new Point(2, 0), new Point(0, 1), new Point(1, 1) });
                case 4:
                    return new Shape(new List<Point> { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(2, 1) });
                case 5:
                    return new Shape(new List<Point> { new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) });
                case 6:
                    return new Shape(new List<Point> { new Point(2, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) });
                default:
                    return new Shape(new List<Point> { new Point(0, 0) });
            }
        }
    }
}
