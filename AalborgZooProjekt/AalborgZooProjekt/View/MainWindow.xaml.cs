using System.Windows;
using AalborgZooProjekt.ViewModel;
using MahApps.Metro.Controls;
using System.Windows.Threading;
using System;

namespace AalborgZooProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        DispatcherTimer Timer = new DispatcherTimer();

        public DateTime Time
        {
            get { return (DateTime)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        private string department = "Afrika";

        public string Department
        {
            get { return department; }
        }

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

        }

        private void TimerClick(object sender, EventArgs e)
        {
            Time = DateTime.Now;
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}