using AalborgZooProjekt.Model.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AalborgZooProjekt.ViewModel.Converters
{
    class EmployeeIDToEmployeeNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            return employeeRepository.GetEmployee((int)value).Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
