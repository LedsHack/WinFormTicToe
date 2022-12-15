using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            string line;
            StreamReader f = new StreamReader("config.txt");
            
            line = f.ReadLine();

            while (line != null)
            {
                richTextBox1.AppendText(line + "\n");
                line = f.ReadLine();
            }
            f.Close();
            //richTextBox1.Enabled = false;
        }

    }
}
