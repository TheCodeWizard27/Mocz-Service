using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Mocz_Parser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MoczHandler _handler;

        public MainWindow()
        {
            DataContext = _handler = new MoczHandler();

            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _handler.Init();
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Load Dictionary", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InputBox.Text)) return;

            try
            {
                _handler.LoadResults(InputBox.Text, GetInputMethodInUse());
            }catch(Exception ex)
            {
                MessageBox.Show("Couldn't Load Results", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private InputMethod GetInputMethodInUse()
        {
            var method = InputMethod.LocalApi;
            method = GoogleApiRadioBtn.IsChecked.GetValueOrDefault() ? InputMethod.GoogleApi : method;

            return method;
        }
    }
}
