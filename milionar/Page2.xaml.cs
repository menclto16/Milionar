using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page2
    {
        private Game gameObj = new Game();
        private List<Button> buttons = new List<Button>();
        private List<TextBlock> roundLabels = new List<TextBlock>();
        private List<Control> audienceIcons = new List<Control>();
        private List<Control> friendIcons = new List<Control>();
        
        Random rnd = new Random();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public int Time = 60;
        public int AnswerAnimTime = 20;

        public Page2()
        {
            InitializeComponent();

            buttons.Add(button1);
            buttons.Add(button2);
            buttons.Add(button3);
            buttons.Add(button4);

            roundLabels.Add(round1);
            roundLabels.Add(round2);
            roundLabels.Add(round3);
            roundLabels.Add(round4);
            roundLabels.Add(round5);
            roundLabels.Add(round6);
            roundLabels.Add(round7);
            roundLabels.Add(round8);
            roundLabels.Add(round9);
            roundLabels.Add(round10);
            roundLabels.Add(round11);
            roundLabels.Add(round12);

            audienceIcons.Add(audience1);
            audienceIcons.Add(audience2);
            audienceIcons.Add(audience3);
            audienceIcons.Add(audience4);

            friendIcons.Add(friend1);
            friendIcons.Add(friend2);
            friendIcons.Add(friend3);
            friendIcons.Add(friend4);

            updateLayout();
        }

        private void updateLayout()
        {
            foreach (var audienceIcon in audienceIcons)
            {
                audienceIcon.Visibility = Visibility.Hidden;
            }

            foreach (var friendIcon in friendIcons)
            {
                friendIcon.Visibility = Visibility.Hidden;
            }

            foreach (var button in buttons)
            {
                button.Visibility = Visibility.Visible;
            }
            
            foreach (var roundLabel in roundLabels)
            {
                roundLabel.Background = Brushes.Transparent;
                roundLabel.Foreground = Brushes.PapayaWhip;
            }

            roundLabels[gameObj.Round-1].Background = Brushes.GreenYellow;
            roundLabels[gameObj.Round - 1].Foreground = Brushes.Black;

            Time = 60;
            AnswerAnimTime = 20;

            startTimer();

            question.Content = gameObj.CurrentQuestion;

            List<int> indexes = new List<int> { 0, 1, 2, 3 };

            for (int i = 0; i < indexes.Count; i++)
            {
                int temp = indexes[i];
                int randomIndex = rnd.Next(i, indexes.Count);
                indexes[i] = indexes[randomIndex];
                indexes[randomIndex] = temp;
            }

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[indexes[i]].Content = gameObj.Questions[gameObj.Round - 1].Answers[i].Content;
            }
        }

        private void startTimer()
        {
            timer.Text = Time.ToString();
            
            dispatcherTimer.Tick -= pickedAnswerTimer;
            dispatcherTimer.Tick += updateTimerLabel;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void updateTimerLabel(object sender, EventArgs e)
        {
            if (Time > 0)
            {
                if (Time <= 10)
                {
                    timer.Foreground = Brushes.Red;
                } else
                {
                    timer.Foreground = Brushes.PapayaWhip;
                }
                Time--;
                timer.Text = Time.ToString();
            }
            else if (Time == 0)
            {
                gameObj.gameFinished = true;
                gameFinishedDialogue(false);
            }
        }

        private void pickedAnswerTimer(object sender, EventArgs e)
        {
            if (AnswerAnimTime > 5)
            {
                buttons[gameObj.PickedAnswer - 1].Background = (AnswerAnimTime % 2 == 0) ? Brushes.GreenYellow : Brushes.MidnightBlue;
                buttons[gameObj.PickedAnswer - 1].Foreground= (AnswerAnimTime % 2 == 0) ? Brushes.Black : Brushes.PapayaWhip;
            }
            else
            {
                if (gameObj.CheckAnswer(buttons[gameObj.PickedAnswer - 1].Content.ToString()))
                {
                    buttons[gameObj.PickedAnswer - 1].Background = Brushes.GreenYellow;
                    if (AnswerAnimTime == 0)
                    {
                        if (gameObj.Round != 12)
                        {
                            buttons[gameObj.PickedAnswer - 1].ClearValue(Control.BackgroundProperty);
                            buttons[gameObj.PickedAnswer - 1].ClearValue(Control.ForegroundProperty);
                            gameObj.RoundAdvance();
                            updateLayout();
                        }
                        else if (gameObj.Round == 12)
                        {
                            gameObj.gameFinished = true;
                            gameFinishedDialogue(true);
                        }
                    }
                }
                else
                {
                    buttons[gameObj.PickedAnswer - 1].Background = Brushes.Red;
                    if (AnswerAnimTime == 0)
                    {
                        gameObj.gameFinished = true;
                        gameFinishedDialogue(false);
                    }
                }
            }
            AnswerAnimTime--;
        }

        private void gameFinishedDialogue(bool win)
        {
            newGameButton.Visibility = Visibility.Visible;
            quitToMenuButton.Visibility = Visibility.Visible;

            if (win) gameStatusLabel.Content = "Gratuluji! Milion je tvůj.";

            for (int i = 0; i < buttons.Count; i++)
            {
                if (i + 1 == gameObj.PickedAnswer) continue;
                if (buttons[i].Content.ToString() == gameObj.Questions[gameObj.Round - 1].GetRightAnswer())
                {
                    buttons[i].Background = Brushes.GreenYellow;
                    buttons[i].Foreground = Brushes.Black;
                }
                else
                {
                    buttons[i].Background = Brushes.MidnightBlue;
                    buttons[i].Foreground = Brushes.PapayaWhip;
                }
            }
        }

        private void audienceHint(object sender, RoutedEventArgs e)
        {
            if (!gameObj.gameFinished)
            {
                audienceHintButton.Visibility = Visibility.Hidden;

                for (int i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].Content.ToString() == gameObj.Questions[gameObj.Round - 1].GetRightAnswer())
                    {
                        audienceIcons[i].Visibility = Visibility.Visible;
                        break;
                    }
                }
            }
        }

        private void friendHint(object sender, RoutedEventArgs e)
        {
            if (!gameObj.gameFinished)
            {
                friendHintButton.Visibility = Visibility.Hidden;

                for (int i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].Content.ToString() == gameObj.Questions[gameObj.Round - 1].GetRightAnswer())
                    {
                        friendIcons[i].Visibility = Visibility.Visible;
                        break;
                    }
                }
            }
        }

        private void fiftyHint(object sender, RoutedEventArgs e)
        {
            if (!gameObj.gameFinished)
            {
                foreach (var button in buttons)
                {
                    button.Visibility = Visibility.Hidden;
                }

                fiftyHintButton.Visibility = Visibility.Hidden;

                for (int i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].Content.ToString() == gameObj.Questions[gameObj.Round - 1].GetRightAnswer())
                    {
                        buttons[i].Visibility = Visibility.Visible;
                        int randomIndex = rnd.Next(0, buttons.Count);

                        while (randomIndex == i)
                        {
                            randomIndex = rnd.Next(0, buttons.Count);
                        }

                        buttons[randomIndex].Visibility = Visibility.Visible;

                        break;
                    }
                }
            }
        }

        private void answerButton1(object sender, RoutedEventArgs e)
        {
            if (gameObj.PickedAnswer == 0 & !gameObj.gameFinished)
            {
                gameObj.PickedAnswer = 1;

                dispatcherTimer.Tick -= updateTimerLabel;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
                dispatcherTimer.Tick += pickedAnswerTimer;
            }
        }

        private void answerButton2(object sender, RoutedEventArgs e)
        {
            if (gameObj.PickedAnswer == 0 & !gameObj.gameFinished)
            {
                gameObj.PickedAnswer = 2;

                dispatcherTimer.Tick -= updateTimerLabel;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
                dispatcherTimer.Tick += pickedAnswerTimer;
            }
        }

        private void answerButton3(object sender, RoutedEventArgs e)
        {
            if (gameObj.PickedAnswer == 0 & !gameObj.gameFinished)
            {
                gameObj.PickedAnswer = 3;

                dispatcherTimer.Tick -= updateTimerLabel;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
                dispatcherTimer.Tick += pickedAnswerTimer;
            }
        }

        private void answerButton4(object sender, RoutedEventArgs e)
        {
            if (gameObj.PickedAnswer == 0 & !gameObj.gameFinished)
            {
                gameObj.PickedAnswer = 4;

                dispatcherTimer.Tick -= updateTimerLabel;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
                dispatcherTimer.Tick += pickedAnswerTimer;
            }
        }

        private void newGame(object sender, RoutedEventArgs e)
        {
            var mainWindow = GetParentWindow(this);
            if (mainWindow != null) mainWindow.mainFrame.Navigate(new Page2());
        }

        private void quitToMenu(object sender, RoutedEventArgs e)
        {
            var mainWindow = GetParentWindow(this);
            if (mainWindow != null) mainWindow.mainFrame.Navigate(new Page1());
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
