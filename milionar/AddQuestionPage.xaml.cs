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
using System.IO;
using Newtonsoft.Json;

namespace milionar
{
    /// <summary>
    /// Interaction logic for AddQuestionPage.xaml
    /// </summary>
    public partial class AddQuestionPage : Page
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        private List<Question> questions = new List<Question>();

        public AddQuestionPage()
        {
            if (File.Exists("questions.json"))
            {
                string jsonFromFile = File.ReadAllText("questions.json");

                questions = JsonConvert.DeserializeObject<List<Question>>(jsonFromFile, settings);
            }

            InitializeComponent();
        }

        private void addQuestion(object sender, RoutedEventArgs e)
        {
            if (!checkInputs())
            {
                errorMess.Content = "Špatný formát vstupů!";
            }
            else
            {
                Question question = new Question();

                question.QuestionText = questionInput.Text;

                question.AddAnswer(answer1.Text);
                question.AddAnswer(answer2.Text);
                question.AddAnswer(answer3.Text);
                question.AddAnswer(answer4.Text);

                question.Difficulty = Int32.Parse(difficulty.Text);

                questions.Add(question);

                saveFile();
            }
        }

        private bool checkInputs()
        {
            if (questionInput.Text == "" | answer1.Text == "" | answer2.Text == "" | answer3.Text == "" | answer4.Text == "" | difficulty.Text == "") return false;

            try
            {
                int input = Int32.Parse(difficulty.Text);
                if (input < 1 | input > 12) return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void saveFile()
        {
            string json = JsonConvert.SerializeObject(questions, settings);

            File.WriteAllText("questions.json", json);
        }

        private void navToPage1(object sender, RoutedEventArgs e)
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
