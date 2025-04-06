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
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            button1.Location = new Point(593, 431);
            button1.Name = "button1";
            button1.Size = new Size(160, 59);
            button1.TabIndex = 1;
            button1.Text = "Menu";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panelGrid
            // 
            panelGrid.Location = new Point(12, 3);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(542, 496);
            panelGrid.TabIndex = 2;
            // 
            // RobotMove
            // 
            RobotMove.Interval = 1000;
            RobotMove.Tick += RobotMove_Tick;
            // 
            // SimulationForm
            // 
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(784, 511);
            ControlBox = false;
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
    }
}