using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace PG5200_Innlevering1.SettingsEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
