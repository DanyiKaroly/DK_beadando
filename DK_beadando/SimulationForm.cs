// A* algoritmus implementálása robot mozgáshoz
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public SimulationForm(Form1 form1)
        {
            _form1 = form1;

            InitializeComponent();
            this.panelGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGrid_Paint);
            this.KeyDown += new KeyEventHandler(SimulationForm_KeyDown);
            this.KeyPreview = true;  // Győződj meg róla, hogy ez be van állítva
        }

        private void SimulationForm_KeyDown(object sender, KeyEventArgs e)
        {
            int dx = 0, dy = 0;
            int moveHappend = 0;

if (e.KeyCode == Keys.W && IsValidMove(Maze.member.position.x, Maze.member.position.y - 1))
{
    moveHappend = 1;
    dy--;
}
else if (e.KeyCode == Keys.S && IsValidMove(Maze.member.position.x, Maze.member.position.y + 1))
{
    moveHappend = 1;
    dy++;
}
else if (e.KeyCode == Keys.A && IsValidMove(Maze.member.position.x - 1, Maze.member.position.y))
{
    moveHappend = 1;
    dx--;
}
else if (e.KeyCode == Keys.D && IsValidMove(Maze.member.position.x + 1, Maze.member.position.y))
{
    moveHappend = 1;
    dx++;
}


            if(moveHappend ==1)
            {
                int newX = Maze.member.position.x + dx;
                int newY = Maze.member.position.y + dy;


                // Rögzítjük az előző pozíciót, hogy csak azokat a cellákat frissítsük
                int oldX = Maze.member.position.x;
                int oldY = Maze.member.position.y;

                // Frissítjük a member pozícióját
                Maze.member.position.x = newX;
                Maze.member.position.y = newY;
                matrixk[oldX][oldY] = 0;
                matrixk[newX][newY] = Maze.member.id;


                // Frissítjük a grid-et, és csak az érintett cellákat rajzoljuk újra
                Rectangle oldRect = new Rectangle(oldX * gridSize, oldY * gridSize, gridSize, gridSize);
                Rectangle newRect = new Rectangle(newX * gridSize, newY * gridSize, gridSize, gridSize);

                panelGrid.Invalidate(newRect);
                panelGrid.Invalidate(oldRect);
                moveHappend = 0;
            }

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

        private void RobotMove_Tick(object sender, EventArgs e)
        {
          /*  int oldX = Maze.robots[0].position.x;
            int oldY = Maze.robots[0].position.y;

            if (Maze.robots[0].position.x == Maze.trucks[0].position.x && Maze.robots[0].position.y == Maze.trucks[0].position.y)
            {
                RobotMove.Stop();
                return;
            }
            if (currentPath.Count == 0)
            {
                int waitTime = random.Next(1000, 5001);
                RobotMove.Stop();
                new Thread(() => {
                Thread.Sleep(waitTime);
                this.Invoke((MethodInvoker)delegate {
                        RobotMove.Start();
                        });
                }).Start();
                return;
            }
            

            matrixk[oldX][oldY] = 0;
            matrixk[newX][newY] = Maze.robots[0].id;

            int gridSize = 35;
            Rectangle oldRect = new Rectangle(oldX * gridSize, oldY * gridSize, gridSize, gridSize);
            Rectangle newRect = new Rectangle(newX * gridSize, newY * gridSize, gridSize, gridSize);

            panelGrid.Invalidate(oldRect);
            panelGrid.Invalidate(newRect); */
        }

        private void panelGrid_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;

            Pen gridPen = new Pen(Color.Black);

            for (int i = 0; i <= Maze.matrix.width; i++)
            {
                for (int j = 0; j <= Maze.matrix.height; j++)
                {
                    Rectangle cellRect = new Rectangle(i * gridSize, j * gridSize, gridSize, gridSize);

                    if (matrixk[i][j] == 0)
                    {
                        g.FillRectangle(Brushes.White, cellRect);
                        g.DrawRectangle(Pens.Black, cellRect);
                    }
                    if (matrixk[i][j] <= 10)
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
                    else if (matrixk[i][j] >= 200 && matrixk[i][j]<900)
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
                                break;
                            }
                        }
                    }
                    else if (matrixk[i][j] > 10 && matrixk[i][j] < 200)
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
                    else if ( matrixk[i][j] > 900)
                    {
                            if (matrixk[i][j] == Maze.member.id)
                            {

                                g.FillRectangle(new SolidBrush(Color.FromArgb(144, 238, 144)), cellRect);
                                g.DrawRectangle(Pens.Black, cellRect);

                                string text = "P";
                                var font = this.Font;
                                SizeF textSize = g.MeasureString(text, font);
                                PointF textPos = new PointF(cellRect.X + (gridSize - textSize.Width) / 2,
                                                            cellRect.Y + (gridSize - textSize.Height) / 2);
                                g.DrawString(text, font, Brushes.Black, textPos);
                                break;
                            }
                        
                    }
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
