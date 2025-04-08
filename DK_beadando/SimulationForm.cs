// A* algoritmus implementálása robot mozgáshoz
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using DK_beadando;

namespace DK_beadando
{
    public partial class SimulationForm : Form
    {
        public MazeData Maze { get; private set; }
        private Form1 _form1;
        public int[][] matrixk;
        private Queue<Point> currentPath = new Queue<Point>();
        private Random random = new Random();
        public int gridSize = 35;
        public int firstgen=0;

        public SimulationForm(Form1 form1)
        {
            _form1 = form1;
            InitializeComponent();

            this.panelGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGrid_Paint);
            this.KeyDown += new KeyEventHandler(SimulationForm_KeyDown);
            this.KeyPreview = true;

            RobotMove.Start();
        }

        private void SimulationForm_KeyDown(object sender, KeyEventArgs e)
        {
            int oldX = Maze.member.position.x;
            int oldY = Maze.member.position.y;

            int newX = oldX;
            int newY = oldY;

            if (e.KeyCode == Keys.W && IsValidMove(Maze.member.position.x, Maze.member.position.y - 1))
            {
                newY--;
                PlayerMoved(oldX, oldY, newX, newY);
            }
            else if (e.KeyCode == Keys.S && IsValidMove(Maze.member.position.x, Maze.member.position.y + 1))
            {
                newY++;
                PlayerMoved(oldX, oldY, newX, newY);
            }
            else if (e.KeyCode == Keys.A && IsValidMove(Maze.member.position.x - 1, Maze.member.position.y))
            {
                newX--;
                PlayerMoved(oldX, oldY, newX, newY);
            }
            else if (e.KeyCode == Keys.D && IsValidMove(Maze.member.position.x + 1, Maze.member.position.y))
            {
                newX++;
                PlayerMoved(oldX, oldY, newX, newY);
            }

        }
        private void PlayerMoved(int oldX, int oldY, int newX, int newY)
        {

            matrixk[oldX][oldY] = 0;

            matrixk[newX][newY] = Maze.member.id;

            Maze.member.position.x = newX;
            Maze.member.position.y = newY;

            Rectangle oldRect = new Rectangle(oldX * gridSize, oldY * gridSize, gridSize, gridSize);
            Rectangle newRect = new Rectangle(newX * gridSize, newY * gridSize, gridSize, gridSize);
            panelGrid.Invalidate(oldRect);
            panelGrid.Invalidate(newRect);

        }

        // Az IsValidMove ellenőrzi, hogy az új pozíció nem lép-e ki a mátrix határon, és hogy a cella szabad-e
        private bool IsValidMove(int x, int y)
        {
            return x >= 0 && x <= Maze.matrix.width && y >= 0 && y <= Maze.matrix.height && matrixk[x][y] == 0;
        }


        private void SimulationForm_Load(object sender, EventArgs e)
        {
            string jsonFilePath = "input.json";
            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                Maze = JsonSerializer.Deserialize<MazeData>(jsonContent);

                InitializeMatrixFromData();
                panelGrid.Invalidate();
            }
            else
            {
                MessageBox.Show("A JSON fájl nem található: " + jsonFilePath);
            }
            RobotMove.Start();
        }

        private void InitializeMatrixFromData()
        {
            int width = Maze.matrix.width;
            int height = Maze.matrix.height;
            int t = 0;
            int[][] grid = new int[width + 1][];
            for (int i = 0; i <= width; i++)
            {
                grid[i] = new int[height + 1];
                for (int j = 0; j <= height; j++)
                {
                    t = 0;
                    if (t == 0)
                    {
                        foreach (var robot in Maze.robots)
                        {
                            if (i == robot.position.x && j == robot.position.y)
                            {
                                grid[i][j] = robot.id;
                                t = 1;
                                break;
                            }
                        }
                    }
                    if (t == 0)
                    {
                        foreach (var truck in Maze.trucks)
                        {
                            if (i == truck.position.x && j == truck.position.y)
                            {
                                grid[i][j] = truck.id;
                                t = 1;
                                break;
                            }
                        }
                    }
                    if (t == 0)
                    {
                        foreach (var shelf in Maze.shelves)
                        {
                            if (i == shelf.position.x && j == shelf.position.y)
                            {
                                grid[i][j] = shelf.id;
                                t = 1;
                                break;
                            }
                        }
                    }
                    if (t == 0)
                    {
                        var member = Maze.member;
                        
                            if (i == member.position.x && j == member.position.y)
                            {
                                grid[i][j] = member.id;
                                t = 1;
                                break;
                            }
                        
                    }
                }
            }
            matrixk = grid;
        }

        private bool fin = false;

        private void RobotMove_Tick(object sender, EventArgs e)
        {
            if (fin) return; // Ha kész, kilépünk

            int oldX = Maze.robots[9].position.x;
            int oldY = Maze.robots[9].position.y;
            int newX = oldX;
            int newY = oldY;

            if (IsValidMove(oldX, oldY - 1) && oldY > Maze.trucks[0].position.y)
            {
                newY--;
            }
            else if (IsValidMove(oldX, oldY + 1) && oldY < Maze.trucks[0].position.y)
            {
                newY++;
            }
            else if (IsValidMove(oldX - 1, oldY) && oldX > Maze.trucks[0].position.x)
            {
                newX--;
            }
            else if (IsValidMove(oldX + 1, oldY) && oldX < Maze.trucks[0].position.x)
            {
                newX++;
            }

            if (newX != oldX || newY != oldY)
            {
                Maze.robots[9].position.x = newX;
                Maze.robots[9].position.y = newY;

                matrixk[oldX][oldY] = 0;
                matrixk[newX][newY] = Maze.robots[9].id;

                Rectangle oldRect = new Rectangle(oldX * gridSize, oldY * gridSize, gridSize, gridSize);
                Rectangle newRect = new Rectangle(newX * gridSize, newY * gridSize, gridSize, gridSize);

                panelGrid.Invalidate(oldRect);
                panelGrid.Invalidate(newRect);
            }

            if (newX == Maze.trucks[0].position.x && newY == Maze.trucks[0].position.y)
            {
                fin = true;
                RobotMove.Stop();
            }
        }
        private void panelGrid_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;

            Pen gridPen = new Pen(Color.Black);

            if (firstgen == 0)
            {
                for (int i = 0; i <= Maze.matrix.width; i++)
                {
                    for (int j = 0; j <= Maze.matrix.height; j++)
                    {
                        Rectangle cellRect = new Rectangle(i * gridSize, j * gridSize, gridSize, gridSize);

                        if (matrixk[i][j] == 0)
                        {
                            ZerokPaint(i, j, cellRect, g);
                        }
                        if (matrixk[i][j] <= 10)
                        {
                            RobotPaint(i, j, cellRect, g);
                        }
                        else if (matrixk[i][j] >= 200 && matrixk[i][j] < 900)
                        {
                            truckPaint(i, j, cellRect, g);
                        }
                        else if (matrixk[i][j] > 10 && matrixk[i][j] < 200)
                        {
                            ShelvesPaint(i, j, cellRect, g);
                        }
                        else if (matrixk[i][j] > 900)
                        {
                            PlayerPaint(i, j, cellRect, g);
                        }
                    }
                }
                firstgen = 1;
            }
            else
            {
                Rectangle clip = e.ClipRectangle;
                int startX = clip.X / gridSize;
                int startY = clip.Y / gridSize;
                int endX = (clip.X + clip.Width) / gridSize;
                int endY = (clip.Y + clip.Height) / gridSize;

                for (int i = startX; i <= endX && i <= Maze.matrix.width; i++)
                {
                    for (int j = startY; j <= endY && j <= Maze.matrix.height; j++)
                    {
                        Rectangle cellRect = new Rectangle(i * gridSize, j * gridSize, gridSize, gridSize);
                        int cellValue = matrixk[i][j];

                        if (cellValue == 0)
                            ZerokPaint(i, j, cellRect, g);
                        else if (cellValue <= 10)
                            RobotPaint(i, j, cellRect, g);
                        else if (cellValue >= 200 && cellValue < 900)
                            truckPaint(i, j, cellRect, g);
                        else if (cellValue > 10 && cellValue < 200)
                            ShelvesPaint(i, j, cellRect, g);
                        else if (cellValue > 900)
                            PlayerPaint(i, j, cellRect, g);
                    }
                }
            }
        }
        private void ZerokPaint(int i, int j, Rectangle cellRect, Graphics g)
        {
            g.FillRectangle(Brushes.White, cellRect);
            g.DrawRectangle(Pens.Black, cellRect);
        }
        private void truckPaint(int i, int j, Rectangle cellRect, Graphics g)
        {
            for (int r = 0; r < Maze.trucks.Count; r++)
            {
                if (matrixk[i][j] == Maze.trucks[r].id)
                {

                    g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 153)), cellRect);
                    g.DrawRectangle(Pens.Black, cellRect);

                    string text = "T" + Maze.trucks[r].id.ToString();
                    var font = this.Font;
                    SizeF textSize = g.MeasureString(text, font);
                    PointF textPos = new PointF(cellRect.X + (gridSize - textSize.Width) / 2,
                                                cellRect.Y + (gridSize - textSize.Height) / 2);
                    g.DrawString(text, font, Brushes.Black, textPos);
                }
            }
        }

        private void RobotPaint(int i, int j, Rectangle cellRect, Graphics g)
        {
            for (int r = 0; r < Maze.robots.Count; r++)
            {
                if (matrixk[i][j] == Maze.robots[r].id)
                {

                    g.FillRectangle(Brushes.LightBlue, cellRect);
                    g.DrawRectangle(Pens.Black, cellRect);

                    string text = "R" + Maze.robots[r].id.ToString();
                    var font = this.Font;
                    SizeF textSize = g.MeasureString(text, font);
                    PointF textPos = new PointF(cellRect.X + (gridSize - textSize.Width) / 2,
                                                cellRect.Y + (gridSize - textSize.Height) / 2);
                    g.DrawString(text, font, Brushes.Black, textPos);
                    break;
                }
            }
        }
        private void PlayerPaint(int i, int j, Rectangle cellRect, Graphics g)
        {
                g.FillRectangle(new SolidBrush(Color.FromArgb(144, 238, 144)), cellRect);
                g.DrawRectangle(Pens.Black, cellRect);

                string text = "P";
                var font = this.Font;
                SizeF textSize = g.MeasureString(text, font);
                PointF textPos = new PointF(cellRect.X + (gridSize - textSize.Width) / 2,
                                            cellRect.Y + (gridSize - textSize.Height) / 2);
                g.DrawString(text, font, Brushes.Black, textPos);
        }


        private void ShelvesPaint(int i, int j, Rectangle cellRect, Graphics g)
        {
            for (int r = 0; r < Maze.shelves.Count; r++)
            {
                if (matrixk[i][j] == Maze.shelves[r].id)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(255, 153, 0)), cellRect);
                    g.DrawRectangle(Pens.Black, cellRect);

                    string text = "S" + Maze.shelves[r].id.ToString();
                    var font = this.Font;
                    SizeF textSize = g.MeasureString(text, font);
                    PointF textPos = new PointF(cellRect.X + (gridSize - textSize.Width) / 2,
                                                cellRect.Y + (gridSize - textSize.Height) / 2);
                    g.DrawString(text, font, Brushes.Black, textPos);
                    break;
                }
            } 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _form1.Location = this.Location;
            _form1.Show();
            this.Close();
        }
    }
}
