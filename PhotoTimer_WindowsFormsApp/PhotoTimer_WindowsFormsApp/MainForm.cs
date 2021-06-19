using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            TimerCallback tm = new TimerCallback(MakePhoto);
            System.Threading.Timer timer = new System.Threading.Timer(tm, 0, 1000, 3000);
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {

        }

        private void showButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(path);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            DeleteFolder(subpath);

            Application.Restart();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult warning = MessageBox.Show($"Вы действительно хотите удалить папку {nameFolder} со всем содержимым?", "Удаление объекта", MessageBoxButtons.YesNo);

            if (warning == DialogResult.Yes)

            {

                DeleteFolder(path);

            }



            else if (warning == DialogResult.No)

            {

                warning = DialogResult.Cancel;

            }
        }

        private void countLabel_Click(object sender, EventArgs e)
        {

        }

        private static void DeleteFolder(string path)

        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                dirInfo.Delete(true); // папку надо удалять со всем содержимым
                MessageBox.Show($"Папка \"{nameFolder}\" успешно удалена.");
            }

            catch (Exception ex)

            {

                Console.WriteLine(ex.Message);

            }

        }
        public static void MakePhoto(object obj)

        {

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            dirInfo.Create();


            dirInfo.CreateSubdirectory(subpath);

            var instance = new PhotoView();

            instance.myEvent += Instance_myEvent;

            instance.InvokeEvent();

            Bitmap printscreen = new Bitmap(100, 100);

            Graphics graphics = Graphics.FromImage(printscreen as Image);

            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

            printscreen.Save($@"C:\Program Files\TestFolder\{subpath}\printscreen {DateTime.Now.ToString("(hh.mm.ss)")}.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

        }

        private static void Instance_myEvent()

        {

            MessageBox.Show("В папку добавлено новое фото.");

        }
    }
}
