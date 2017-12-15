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

            //expanderGridHeight = new GridLength[ExpanderGrid.RowDefinitions.Count];
            //expanderGridHeight[0] = ExpanderGrid.RowDefinitions[0].Height;
            //expanderGridHeight[1] = ExpanderGrid.RowDefinitions[1].Height;
            //ExpandedOrCollapsed(BemærkningerExpander);
            //ExpandedOrCollapsed(MedarbejderExpander);

            //BemærkningerExpander.Expanded += ExpandedOrCollapsed;
            //BemærkningerExpander.Collapsed += ExpandedOrCollapsed;
            //MedarbejderExpander.Expanded += ExpandedOrCollapsed;
            //MedarbejderExpander.Collapsed += ExpandedOrCollapsed;

            //Todo impl dis :3
            StatusbarText.Text = "STATUS?";
        }

        //void ExpandedOrCollapsed(object sender, RoutedEventArgs e)
        //{
        //    ExpandedOrCollapsed(sender as Expander);
        //}

        //void ExpandedOrCollapsed(Expander expander)
        //{
        //    var rowIndex = Grid.GetRow(expander);
        //    var row = ExpanderGrid.RowDefinitions[rowIndex];

        //    if (expander.IsExpanded)
        //    {
        //        row.Height = expanderGridHeight[rowIndex];
        //        row.MinHeight = 50;
        //    }
        //    else
        //    {
        //        expanderGridHeight[rowIndex] = row.Height;
        //        row.Height = GridLength.Auto;
        //        row.MinHeight = 0;
        //    }
        //}

        private void TimerClick(object sender, EventArgs e)
        {
            Time = DateTime.Now;
        }

    }
}