using System.Windows;
using FactorioRatio.Gui.ViewModels;

namespace FactorioRatio.Gui
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowVm();
        }
    }
}
