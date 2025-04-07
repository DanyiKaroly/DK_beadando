namespace DK_beadando
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Silver;
            button1.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            button1.FlatAppearance.MouseOverBackColor = Color.Green;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(315, 328);
            button1.Name = "button1";
            button1.Size = new Size(160, 59);
            button1.TabIndex = 0;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Silver;
            button2.FlatAppearance.MouseOverBackColor = Color.DarkRed;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            button2.Location = new Point(315, 421);
            button2.Name = "button2";
            button2.Size = new Size(160, 59);
            button2.TabIndex = 1;
            button2.Text = "Exit";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label1.Location = new Point(115, 175);
            label1.Name = "label1";
            label1.Size = new Size(114, 23);
            label1.TabIndex = 2;
            label1.Text = "Welcome To";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Silver;
            label2.Font = new Font("Cascadia Code", 30F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label2.Location = new Point(138, 123);
            label2.Name = "label2";
            label2.Size = new Size(528, 52);
            label2.TabIndex = 3;
            label2.Text = "Interactive  Warehouse";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.BackColor = Color.Silver;
            label3.Font = new Font("Cascadia Code", 30F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label3.Location = new Point(220, 175);
            label3.Name = "label3";
            label3.Size = new Size(367, 52);
            label3.TabIndex = 4;
            label3.Text = "Robot Simulator";
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(784, 511);
            ControlBox = false;
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Robot Simulation";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
