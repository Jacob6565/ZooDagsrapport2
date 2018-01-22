using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using AalborgZooProjekt.Model;

namespace AalborgZooProjekt.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<ZookeeperViewModel>();
            SimpleIoc.Default.Register<OfficeViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
        }

        public ZookeeperViewModel Zoo
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ZookeeperViewModel>();
            }
        }

        public OfficeViewModel Office
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OfficeViewModel>();
            }
        }

        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public static void Cleanup()
        {
        }
    }
}