using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Frame frame;

        public MainWindow()
        {
            InitializeComponent();
            NavigateToPage1();
        }

        public void NavigateToPage1()
        {
            mainFrame.Navigate(new Page1(mainFrame));
        }

        public void NavigateToPage2()
        {
            mainFrame.Navigate(new Page2(mainFrame));
        }
    }
}
