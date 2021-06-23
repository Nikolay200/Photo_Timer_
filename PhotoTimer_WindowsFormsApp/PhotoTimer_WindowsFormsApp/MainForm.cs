using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PhotoTimer_WindowsFormsApp
{
    public partial class MainForm : Form
    {
        private static string nameFolder = "TestFolder";
        private static string path = $@"C:\Program Files\{nameFolder}";
        private static string subpath = $@"Photo{DateTime.Now.ToString("(dd MMMM yyyy  hh.mm.ss)")}";

        public System.Windows.Forms.Timer numberTimer = new System.Windows.Forms.Timer();
        public static System.Threading.Timer timer;

        public static int CountPhotos = 0;
        
        public MainForm()
        {
            InitializeComponent();          
        }       

        private void StartButton_Click(object sender, EventArgs e)
        {
            var tm = new TimerCallback(MakePhoto);
            
             timer = new System.Threading.Timer(tm, 0, 500, 1000);

            var result = new Result(CountPhotos);
            numberTimer.Interval = 500;
            numberTimer.Tick += NumberTimer_Tick;
            numberTimer.Start();
        }

        private void NumberTimer_Tick(object sender, EventArgs e)
        {
            countLabel.Text = CountPhotos.ToString();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            timer.Dispose();
            MessageBox.Show(@"Чтобы продолжить, нажмите ""Старт""");
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception)
            {
                MessageBox.Show(@"В данной директории нет указанной папки. Возможно она не была создана. Нажмите ""Старт"", чтобы создать папку для хранения снимков");
                throw;
            }            
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            DialogResult warning = MessageBox.Show($@"Вы действительно хотите очистить папку ""{nameFolder}""?", "Очистка", MessageBoxButtons.YesNo);
            if (warning == DialogResult.Yes)
            {
                DirectoryInfo dirInfo = new DirectoryInfo($@"{path}\{subpath}");
                dirInfo.Delete(true);
                CountPhotos = 0;
                MessageBox.Show($@"Папка ""{subpath}"" успешно удалена.");
            }

            else if (warning == DialogResult.No)
            {
                warning = DialogResult.Cancel;
            }
                                 
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DialogResult warning = MessageBox.Show($@"Вы действительно хотите удалить папку ""{nameFolder}"" со всем содержимым?", "Удаление объекта", MessageBoxButtons.YesNo);

            if (warning == DialogResult.Yes)
            {
                DeleteFolder(path);
            }

            else if (warning == DialogResult.No)
            {
                warning = DialogResult.Cancel;
            }
        }
              
        private static void DeleteFolder(string path)

        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                dirInfo.Delete(true); // папку надо удаляется со всем содержимым
                MessageBox.Show($@"Папка ""{nameFolder}"" успешно удалена.");
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

            var photo = new PhotoView();
            photo.addPhoto += Photo_addPhoto;            
            photo.InvokeEvent();
            
            Bitmap printscreen = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            var randomX = new Random();
            var randomY = new Random();
            graphics.CopyFromScreen(randomX.Next(0, 200), randomY.Next(0, 200), 0, 0, printscreen.Size);

            printscreen.Save($@"C:\Program Files\TestFolder\{subpath}\printscreen №{CountPhotos} {DateTime.Now.ToString("(hh.mm.ss)")}.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

        }

        private static void Photo_addPhoto()

        {
            CountPhotos++;           

            if (CountPhotos % 10 == 0)

            {
                timer.Dispose();
                DialogResult overflow = MessageBox.Show($@"В папку ""{nameFolder}"" добавлено {CountPhotos} фотографий. Продолжить?", "Осторожно! Возможно переполнение папки.", MessageBoxButtons.YesNo);

                if (overflow == DialogResult.Yes)

                {
                    MessageBox.Show(@"Чтобы продолжить, нажмите кнопку ""Старт""");
                }

                if (overflow == DialogResult.No)

                {
                    overflow = DialogResult.Cancel;
                }
            }
        }       
    }
}
