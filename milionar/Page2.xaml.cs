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
    public partial class Page2
    {
        private Game gameObj = new Game();

        public Page2()
        {
            InitializeComponent();

            updateLayout();
        }

        private void updateLayout()
        {
            question.Content = gameObj.CurrentQuestion;
            List<Button> buttons = new List<Button> { button1, button2, button3, button4 };

            Random rnd = new Random();
            for (int i = 0; i < buttons.Count; i++)
            {
                Button temp = buttons[i];
                int randomIndex = rnd.Next(i, buttons.Count);
                buttons[i] = buttons[randomIndex];
                buttons[randomIndex] = temp;
            }

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Content = gameObj.Questions[gameObj.Round - 1].Answers[i].Content;
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (gameObj.Questions[gameObj.Round-1].GetRightAnswer() == button1.Content.ToString())
            {
                gameObj.Round++;
                updateLayout();
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            if (gameObj.Questions[gameObj.Round - 1].GetRightAnswer() == button2.Content.ToString())
            {
                gameObj.Round++;
                updateLayout();
            }
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            if (gameObj.Questions[gameObj.Round - 1].GetRightAnswer() == button3.Content.ToString())
            {
                gameObj.Round++;
                updateLayout();
            }
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            if (gameObj.Questions[gameObj.Round - 1].GetRightAnswer() == button4.Content.ToString())
            {
                gameObj.Round++;
                updateLayout();
            }
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
