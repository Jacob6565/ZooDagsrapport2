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

        public static void Cleanup()
        {
        }
    }
}