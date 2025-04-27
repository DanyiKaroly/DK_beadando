namespace DK_beadando
{
    partial class SimulationForm
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
            components = new System.ComponentModel.Container();
            button1 = new Button();
            panelGrid = new Panel();
            RobotMove = new System.Windows.Forms.Timer(components);
            richTextBox1 = new RichTextBox();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            button1.Location = new Point(623, 452);
            button1.Name = "button1";
            button1.Size = new Size(160, 59);
            button1.TabIndex = 1;
            button1.Text = "Menu";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panelGrid
            // 
            panelGrid.Location = new Point(4, 3);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(516, 496);
            panelGrid.TabIndex = 2;
            // 
            // RobotMove
            // 
            RobotMove.Interval = 1000;
            RobotMove.Tick += RobotMove_Tick;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(526, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(224, 389);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            // 
            // button2
            // 
            button2.BackColor = Color.Silver;
            button2.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            button2.FlatAppearance.MouseOverBackColor = Color.Green;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            button2.ForeColor = Color.Black;
            button2.Location = new Point(526, 407);
            button2.Name = "button2";
            button2.Size = new Size(91, 32);
            button2.TabIndex = 5;
            button2.Text = "Shelves";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Silver;
            button3.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            button3.FlatAppearance.MouseOverBackColor = Color.Green;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            button3.ForeColor = Color.Black;
            button3.Location = new Point(623, 407);
            button3.Name = "button3";
            button3.Size = new Size(72, 32);
            button3.TabIndex = 6;
            button3.Text = "Tasks";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.Silver;
            button4.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            button4.FlatAppearance.MouseOverBackColor = Color.Green;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            button4.ForeColor = Color.Black;
            button4.Location = new Point(701, 407);
            button4.Name = "button4";
            button4.Size = new Size(82, 32);
            button4.TabIndex = 7;
            button4.Text = "Robots";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // SimulationForm
            // 
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(784, 511);
            ControlBox = false;
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(richTextBox1);
            Controls.Add(panelGrid);
            Controls.Add(button1);
            DoubleBuffered = true;
            KeyPreview = true;
            Name = "SimulationForm";
            Text = "Running simulation";
            Load += SimulationForm_Load;
            ResumeLayout(false);

        }

        #endregion

        private Button button1;
        private Panel panelGrid;
        private System.Windows.Forms.Timer RobotMove;
        private RichTextBox richTextBox1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}