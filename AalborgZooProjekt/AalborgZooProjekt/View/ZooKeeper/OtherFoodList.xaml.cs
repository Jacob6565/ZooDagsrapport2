using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AalborgZooProjekt.View
{
    /// <summary>
    /// Interaction logic for DepartmentRawFeed.xaml
    /// </summary>
    public partial class OtherFoodList : UserControl
    {
        public OtherFoodList()
        {
            InitializeComponent();
            ConnectionViewModel vm = new ConnectionViewModel();
            DataContext = vm;
            ((ConnectionViewModel)DataContext).PhonebookEntry = "test";
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^\\d]+"); //regex that matches allowed text

            return !regex.IsMatch(text);
        }

        private void Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                    text = Regex.Match(text, "[\\d]+").ToString();
                    Clipboard.SetData(DataFormats.Text, text);
                    ApplicationCommands.Paste.Execute(text, (System.Windows.IInputElement)sender);
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

    }
}

public class PhoneBookEntry
{
    public string Name { get; set; }
    public PhoneBookEntry(string name)
    {
        Name = name;
    }
    public override string ToString()
    {
        return Name;
    }
}
public class ConnectionViewModel : INotifyPropertyChanged
{
    public ConnectionViewModel()
    {
        IList<PhoneBookEntry> list = new List<PhoneBookEntry>();
        list.Add(new PhoneBookEntry("test"));
        list.Add(new PhoneBookEntry("test2"));
        _phonebookEntries = new CollectionView(list);
    }
    private readonly CollectionView _phonebookEntries;
    private string _phonebookEntry;

    public CollectionView PhonebookEntries
    {
        get { return _phonebookEntries; }
    }

    public string PhonebookEntry
    {
        get { return _phonebookEntry; }
        set
        {
            if (_phonebookEntry == value) return;
            _phonebookEntry = value;
            OnPropertyChanged("PhonebookEntry");
        }
    }
    private void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
    public event PropertyChangedEventHandler PropertyChanged;
}
