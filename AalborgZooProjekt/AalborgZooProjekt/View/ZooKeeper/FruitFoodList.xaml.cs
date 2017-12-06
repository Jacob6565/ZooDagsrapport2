using System;
using System.Collections.Generic;
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
    /// Interaction logic for DepartmentFoodList.xaml
    /// </summary>
    public partial class FruitFoodList : UserControl
    {
        public FruitFoodList()
        {
            InitializeComponent();
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^\\d ]+"); //regex that matches disallowed text

            if (text.Contains(' '))
                return true;
            else
                return !regex.IsMatch(text);
        }
    }
}
