namespace DK_beadando
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SimulationForm newForm = new SimulationForm(this);

            newForm.StartPosition = FormStartPosition.Manual;
            newForm.Location = this.Location;
            this.Hide();
            newForm.Show();
        }

    }
}
