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

namespace milionar
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1
    {
        public Page1()
        {
            InitializeComponent();

        }

        private void newGame(object sender, RoutedEventArgs e)
        {
            if (!new Game().CheckQuestions())
            {
                errorMess.Content = "Nedostatek otázek!";
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                var mainWindow = GetParentWindow(this);
                if (mainWindow != null) mainWindow.mainFrame.Navigate(new Page2());
            }

        }

        private void addQuestions(object sender, RoutedEventArgs e)
        {
            var mainWindow = GetParentWindow(this);
            if (mainWindow != null) mainWindow.mainFrame.Navigate(new AddQuestionPage());
        }

        private void quitGame(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private static MainWindow GetParentWindow(DependencyObject obj)
        {
            while (obj != null)
            {
                var mainWindow = obj as MainWindow;
                if (mainWindow != null) return mainWindow;
                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }
    }
}
