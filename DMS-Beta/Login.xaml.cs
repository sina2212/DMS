using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Media.Effects;
using System.Data;
using DMS_Beta.Controllers;
using DMS_Beta.Models;

namespace DMS_Beta
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        #region variables
        private DispatcherTimer timerImageChange;
        private Image[] ImageControls;
        private List<ImageSource> Images = new List<ImageSource>();
        private static string[] ValidImageExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
        private static string[] TransitionEffects = new[] { "Fade" };
        private string TransitionType, strImagePath = "D:/DMS/Rasa/Sorces/Resorces/Background";
        private int CurrentSourceIndex, CurrentCtrlIndex, EffectIndex = 0, IntervalTimer = 3;
        #endregion
        public Login()
        {
            InitializeComponent();
            ImageControls = new[] { myImage, myImage2 };
            LoadImageFolder(strImagePath);
            timerImageChange = new DispatcherTimer();
            timerImageChange.Interval = new TimeSpan(0, 0, IntervalTimer);
            timerImageChange.Tick += new EventHandler(timerImageChange_Tick);
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            date.Text = dt.ToLongDateString();
            clock.Text = dt.ToShortTimeString();
            PlaySlideShow();
            timerImageChange.IsEnabled = true;
        }

        private void Click(object sender, EventArgs e)
        {
            loginpanel.Visibility = Visibility.Visible;
            datepanel.Visibility = Visibility.Hidden;
            timerImageChange.Stop();
            timerImageChange.IsEnabled = false;
            BlurEffect blur = new BlurEffect();
            blur.Radius = 15;
            myImage.Effect = blur;
            myImage2.Effect = blur;
        }

        private void Close(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void login(object sender, RoutedEventArgs e)
        {
            Database db = new Database();
            if (db.checkconnection() == false)
            {
                MessageBox.Show("عدم اتصال به شبکه");
                return;
            }
            EmployeeController ec = new EmployeeController();
            DataTable result = new DataTable();
            result = ec.Login(txtUsername.Text, txtPassword.Password);
            if (result != null && result.Rows.Count != 0)
            {
                this.Hide();
                MainWindow main = new MainWindow(result);
                main.ShowDialog();
            }
            else if (result != null && result.Rows.Count == 0)
            {
                MessageBox.Show("رمز عبور پسورد اشتباه است، دوباره تلاش کنید");
            }
            else
            {
                MessageBox.Show("خطایی رخ داده");
            }
        }
        
        private void keypress(object sender, System.Windows.Input.KeyEventArgs e)
        {
            loginpanel.Visibility = Visibility.Visible;
            datepanel.Visibility = Visibility.Hidden;
            timerImageChange.Stop();
            timerImageChange.IsEnabled = false;
            BlurEffect blur = new BlurEffect();
            blur.Radius = 15;
            myImage.Effect = blur;
            myImage2.Effect = blur;
        }

        #region back ground slide show
        /// <summary>
        /// choose a random image from repository and show them as slideshow
        /// </summary>
        /// <param name="file"></param>
        /// <param name="forcePreLoad"></param>
        /// <returns></returns>
        private ImageSource CreateImageSource(string file, bool forcePreLoad)
        {
            if (forcePreLoad)
            {
                var src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(file, UriKind.Absolute);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                src.Freeze();
                return src;
            }
            else
            {
                var src = new BitmapImage(new Uri(file, UriKind.Absolute));
                src.Freeze();
                return src;
            }
        }
        private void LoadImageFolder(string folder)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            if (!System.IO.Path.IsPathRooted(folder))
                folder = System.IO.Path.Combine(Environment.CurrentDirectory, folder);
            Random r = new Random();
            var sources = from file in new System.IO.DirectoryInfo(strImagePath).GetFiles().AsParallel()
                          where ValidImageExtensions.Contains(file.Extension, StringComparer.InvariantCultureIgnoreCase)
                          orderby r.Next()
                          select CreateImageSource(file.FullName, true);
            Images.Clear();
            Images.AddRange(sources);
            sw.Stop();
        }
        private void PlaySlideShow()
        {
            if (Images.Count == 0)
                return;
            var oldCtrlIndex = CurrentCtrlIndex;
            CurrentCtrlIndex = (CurrentCtrlIndex + 1) % 2;
            CurrentSourceIndex = (CurrentSourceIndex + 1) % Images.Count;

            Image imgFadeOut = ImageControls[oldCtrlIndex];
            Image imgFadeIn = ImageControls[CurrentCtrlIndex];
            ImageSource newSource = Images[CurrentSourceIndex];
            imgFadeIn.Source = newSource;

            TransitionType = TransitionEffects[EffectIndex].ToString();

            Storyboard StboardFadeOut = (Resources[string.Format("{0}Out", TransitionType.ToString())] as Storyboard).Clone();
            StboardFadeOut.Begin(imgFadeOut);
            Storyboard StboardFadeIn = Resources[string.Format("{0}In", TransitionType.ToString())] as Storyboard;
            StboardFadeIn.Begin(imgFadeIn);
        }
        private void timerImageChange_Tick(object sender, EventArgs e)
        {
            PlaySlideShow();
        }
        #endregion

    }
}
