using System.Windows;
using AalborgZooProjekt.ViewModel;
using MahApps.Metro.Controls;
using System.Windows.Threading;
using System;
using System.Windows.Controls;

namespace AalborgZooProjekt.View.ZooKeeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        DispatcherTimer Timer = new DispatcherTimer();
        GridLength[] expanderGridHeight;

        public DateTime Time
        {
            get { return (DateTime)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        //private string department = "Afrika";

        //public string Department
        //{
        //    get { return department; }
        //    set
        //    {
        //        if (value.Length > 0)
        //        {
        //            department = value;
        //        }
        //    }
        //}

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(DateTime.Now));

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Closing += (s, e) => ViewModelLocator.Cleanup();

            this.DataContext = this;
            Timer.Tick += new EventHandler(TimerClick);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();

            StatusbarText.Text = "STATUS";
        }



        private void TimerClick(object sender, EventArgs e)
        {
            Time = DateTime.Now;
        }

    }
}