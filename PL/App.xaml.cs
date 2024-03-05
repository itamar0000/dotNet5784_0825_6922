using System.Configuration;
using System.Data;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            s_bl.ReadDateAtTheStart();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            s_bl.Clock.SetCurrentDate(s_bl.CurrentClock);
        }
    }


    public class EngineerSelectedEventArgs : EventArgs
    {
        public int SelectedEngineerId { get; set; }
    }
}
