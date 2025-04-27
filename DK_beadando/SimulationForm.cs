// A* algoritmus implementálása robot mozgáshoz
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using DK_beadando;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace DK_beadando
{
    public partial class SimulationForm : Form
    {
        public MazeData Maze { get; private set; }
        private Form1 _form1;
        public int[][] matrixk;
        public int gridSize = 35;
        public int firstgen = 0;
        public Random rand = new Random();

        public SimulationForm(Form1 form1)
        {
            _form1 = form1;
            InitializeComponent();

            this.panelGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGrid_Paint);
            this.KeyDown += new KeyEventHandler(SimulationForm_KeyDown);
            this.KeyPreview = true;


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


        private bool IsValidMove(int x, int y)
        {
            return x >= 0 && x <= Maze.matrix.width && y >= 0 && y <= Maze.matrix.height && (matrixk[x][y] == 0 || matrixk[x][y] >= 1000);
        }


        private void SimulationForm_Load(object sender, EventArgs e)
        {


            string jsonFilePath = "input.json";
            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
                };

                Maze = JsonSerializer.Deserialize<MazeData>(jsonContent, options);
                InitializeMatrixFromData();
                panelGrid.Invalidate();
            }
            else
            {
                MessageBox.Show("A JSON fájl nem található: " + jsonFilePath);
            }


            int a = 0;
            int b = 0;
            int c = 0;
            a = AddItemsToShelves();
            if (a == 1)
            {
                b = TaskMaker();
            }
            if (b == 1)
            {
                c = AddTasksToRobots();
            }
            if (c == 1)
            {
                foreach (var r in Maze.robots)
                {
                    if(r.task != null)
                    {
                        r.targetShelves = SelectTargetShelves(r);
                        r.status = RobotStatus.active;
                    }

                }
                RobotMove.Start();
            }

        }
        

        private int TaskMaker()
        {
            Targy newitem = new Targy();
            foreach (var truck in Maze.trucks)
            {
                Maze.tasks.Add(new Task
                {
                    TruckId = truck.id,
                    Items = new List<Targy> { }
                });

                foreach (var shelf in Maze.shelves)
                {
                    foreach (var item in shelf.Targyak)
                    {
                        if (rand.NextDouble() < 0.5)
                        {
                            if (!Maze.tasks.Last().Items.Any(x => x.TargyId == item.TargyId))
                            {
                                newitem = new Targy
                                {
                                    TargyId = item.TargyId,
                                    Mennyiseg = rand.Next(1, item.Mennyiseg + 1),
                                    Status = ItemsStatus.AVAILABLE
                                };

                                Maze.tasks.Last().Items.Add(newitem);
                            }
                        }
                    }
                }
            }
            return 1;
        }
        private int AddTasksToRobots()
        {
            int i = 0;
            foreach (var robot in Maze.robots)
            {
                if (i < Maze.tasks.Count)
                {
                    robot.task = Maze.tasks[i];
                    i++;
                }
                else
                {
                    robot.task = null;
                }
            }
            return 1;
        }

        private void ShelfPrint()
        {
            foreach (var szekreny in Maze.shelves)
            {
                if (szekreny.Targyak.Count != 0)
                {
                    szekreny.Targyak = szekreny.Targyak.OrderBy(item => item.TargyId).ToList();
                    richTextBox1.AppendText($"Shelf: {szekreny.id}\n");
                    foreach (var item in szekreny.Targyak)
                    {
                        richTextBox1.AppendText($"\tItem: {item.TargyId}, DB: {item.Mennyiseg} {item.Status}\n");
                    }
                }
            }
        }
        private void TasksPrint()
        {
            foreach (var robot in Maze.robots)
            {
                if (robot.task != null)
                {
                    robot.task.Items = robot.task.Items.OrderBy(item => item.TargyId).ToList();
                    richTextBox1.AppendText($"T{robot.task.TruckId}, R{robot.id}:\n");
                    foreach (var RobotItem in robot.task.Items)
                    {
                        richTextBox1.AppendText($"\tItem: {RobotItem.TargyId}, Mennyiség: {RobotItem.Mennyiseg}\n");
                    }
                }
                else { break; }
                ;
            }
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
                                robot.BatteryLevel = rand.Next(1, 101);
                                t = 1;
                                break;
                            }
                        }
                    }
                    if (t == 0)
                    {
                        foreach (var chargingpad in Maze.chargingpads)
                        {
                            if (i == chargingpad.position.x && j == chargingpad.position.y)
                            {
                                grid[i][j] = chargingpad.id;
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
        private int AddItemsToShelves()
        {
            foreach (var s in Maze.shelves)
            {
                if (rand.NextDouble() < 0.2)
                {
                    int ItemsAmount = rand.Next(1, 4);
                    for (int i = 0; i < ItemsAmount; i++)
                    {
                        var newitem = new Targy
                        {
                            TargyId = rand.Next(1, 10),
                            Mennyiseg = rand.Next(1, 100),
                            Status = ItemsStatus.AVAILABLE,
                        };

                        s.Targyak.Add(newitem);

                    }
                }
            }
            return 1;
        }
        private List<Shelf> SelectTargetShelves(Robot robot)
        {
            if (robot.task == null)
            {
                return null;
            }

            List<Shelf> targetShelves = new List<Shelf>();
            int itemsCount = robot.task.Items.Count;
            int foundItems = 0;

            foreach (var taskItem in robot.task.Items)
            {
                bool itemFound = false;

                foreach (var shelf in Maze.shelves)
                {
                    foreach (var shelfItem in shelf.Targyak)
                    {
                        if (taskItem.TargyId == shelfItem.TargyId &&
                            taskItem.Mennyiseg <= shelfItem.Mennyiseg &&
                            shelfItem.Status == ItemsStatus.AVAILABLE &&
                            taskItem.Status == ItemsStatus.AVAILABLE)
                        {
                            if (shelfItem.Mennyiseg > taskItem.Mennyiseg)
                            {
                                Targy newItem = new Targy
                                {
                                    TargyId = shelfItem.TargyId,
                                    Mennyiseg = shelfItem.Mennyiseg - taskItem.Mennyiseg,
                                    Status = ItemsStatus.AVAILABLE
                                };
                                shelf.Targyak.Add(newItem);
                            }

                            shelfItem.Mennyiseg = taskItem.Mennyiseg;
                            shelfItem.Status = ItemsStatus.RESERVED;
                            taskItem.Status = ItemsStatus.FOUND;

                            foundItems++;
                            itemFound = true;

                            if (!targetShelves.Contains(shelf))
                            {
                                targetShelves.Add(shelf);
                            }

                            break;
                        }
                    }

                    if (itemFound)
                        break;
                }
            }

            if (foundItems > 0)
            {
                return targetShelves;
            }
            else
            {
                return null;
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
                        else if (matrixk[i][j] <= 10)
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
                        else if (matrixk[i][j] == 999)
                        {
                            PlayerPaint(i, j, cellRect, g);
                        }
                        else if (matrixk[i][j] >= 1000)
                        {
                            ChargingpadPaint(i, j, cellRect, g);
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
                        else if (cellValue == 999)
                            PlayerPaint(i, j, cellRect, g);
                        else if (cellValue > 1000)
                            ChargingpadPaint(i, j, cellRect, g);
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
        private void ChargingpadPaint(int i, int j, Rectangle cellRect, Graphics g)
        {
            for (int r = 0; r < Maze.chargingpads.Count; r++)
            {
                if (matrixk[i][j] == Maze.chargingpads[r].id)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(102, 178, 255)), cellRect);
                    g.DrawRectangle(Pens.Black, cellRect);
                    string text = "C";
                    switch (Maze.chargingpads[r].id)
                    {
                        case 1001:
                            text = text + "1";
                            break;
                        case 1002:
                            text = text + "2";
                            break;
                        case 1003:
                            text = text + "3";
                            break;
                        case 1004:
                            text = text + "4";
                            break;
                        case 1005:
                            text = text + "5";
                            break;
                        case 1006:
                            text = text + "6";
                            break;
                        case 1007:
                            text = text + "7";
                            break;
                        case 1008:
                            text = text + "8";
                            break;
                        case 1009:
                            text = text + "9";
                            break;
                        case 1010:
                            text = text + "10";
                            break;
                    }

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

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            ShelfPrint();
        }
        private void PrintTargetShelves(List<Shelf> shelves)
        {
            if (shelves != null)
                foreach (var shelf in shelves)
                {
                    richTextBox1.AppendText($"\tShelf ID: {shelf.id}\n");
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            TasksPrint();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();


            foreach (var r in Maze.robots)
            {
                if (r.task != null)
                {
                    richTextBox1.AppendText($"Robot: {r.id}\n");
                    PrintTargetShelves(r.targetShelves);
                }
            }
        }


    }
}
