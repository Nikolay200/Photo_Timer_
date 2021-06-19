using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoTimer_WindowsFormsApp
{
    public partial class MainForm : Form
    {
        public static string nameFolder = "TestFolder";

        public static string path = $@"C:\Program Files\{nameFolder}";

        public static string subpath = $@"Photo{DateTime.Now.ToString("(dd MMMM yyyy  hh.mm.ss)")}";

        public MainForm()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {

        }

        private void pauseButton_Click(object sender, EventArgs e)
        {

        }

        private void showButton_Click(object sender, EventArgs e)
        {

        }

        private void resetButton_Click(object sender, EventArgs e)
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

        }

        private void countLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
