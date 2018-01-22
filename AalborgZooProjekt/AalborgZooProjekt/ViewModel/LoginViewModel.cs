using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Model;
using AalborgZooProjekt.Model.Repository;
using GalaSoft.MvvmLight.CommandWpf;
using AalborgZooProjekt.View.ZooKeeper;
using AalborgZooProjekt.View;

namespace AalborgZooProjekt.ViewModel
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            Departments = new BindingList<Department>(dbDepartmentRep.GetDepartments());
        }
        private IDepartmentRepository dbDepartmentRep = new DeparmentRepository();

        public BindingList<Department> _departments = new BindingList<Department>();
        public BindingList<Department> Departments
        {
            get
            {
                return _departments;
            }
            private set
            {
                _departments = value;
            }
        }

        private RelayCommand<Department> _openDepartmentCommand;
        public RelayCommand<Department> OpenDepartmentCommand 
        {
            get
            {
                return _openDepartmentCommand
                    ?? (_openDepartmentCommand = new RelayCommand<Department>(OpenZookeeperView));
            }
        }

        public void OpenZookeeperView(Department department)
        {
            MainWindow departmentWindow = new MainWindow();
            ZookeeperViewModel zookeeperViewModel = new ZookeeperViewModel();            
            zookeeperViewModel.Department = department;
            departmentWindow.DataContext = zookeeperViewModel;            
            
            //loginView.Hide();
            departmentWindow.ShowDialog();            
            //loginView.Show();
        }

        private RelayCommand _openOfficeCommand;
        public RelayCommand OpenOfficeCommand
        {
            get
            {
                return _openOfficeCommand
                    ?? (_openOfficeCommand = new RelayCommand(OpenOfficeView));
            }
        }

        public void OpenOfficeView()
        {
            OfficeView officeView = new OfficeView();                       
            officeView.ShowDialog();
        }
    }


}
