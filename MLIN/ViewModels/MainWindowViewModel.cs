using ReactiveUI;
using System.Windows.Controls;
using MLIN.Views;

namespace MLIN.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private UserControl currentControl = new HomePage();
        public UserControl CurrentControl
        {
            get { return currentControl; }
            set {this.RaiseAndSetIfChanged(ref currentControl, value); }
        }
    }
}
