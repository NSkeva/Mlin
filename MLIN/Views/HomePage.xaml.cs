using System.Windows;
using System.Windows.Controls;

namespace MLIN.Views
{
    /// <summary>
    ///     Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            var newPlayerPage = new NewPlayerPage();
            Content = newPlayerPage;
        }

        private void Pravila(object sender, RoutedEventArgs e)
        {
            var PravilaPage = new Pravila();
            Content = PravilaPage;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
