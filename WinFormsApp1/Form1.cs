namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Form Game = new Game(true);
                Game.Show();
            }
            else
            {
                Form Game = new Game(false);
                Game.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form frm2 = new Form2();
            frm2.Show();
        }
    }
}