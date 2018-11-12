using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace milionar
{
    class Game
    {
        public int Round = 1;
        public string CurrentQuestion
        {
            get
            {
                return Questions[Round-1].QuestionText;
            }
        }

        public List<Question> Questions = new List<Question>();

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public Game()
        {
            if (File.Exists("questions.json"))
            {
                string jsonFromFile = File.ReadAllText("questions.json");

                List<Question> questions = JsonConvert.DeserializeObject<List<Question>>(jsonFromFile, settings);

                prepareQuestions(questions);
            }
        }

        private void prepareQuestions(List<Question> questions)
        {
            List<List<Question>> sortedQuestions = new List<List<Question>>();
            for (int i = 0; i < 12; i++)
            {
                sortedQuestions.Add(new List<Question>());
                foreach (var question in questions)
                {
                    if (question.Difficulty == i+1) sortedQuestions[i].Add(question);
                }

                Random rnd = new Random();
                int rndNum = rnd.Next(0, sortedQuestions[i].Count);

                Questions.Add(sortedQuestions[i][rndNum]);
            }
        }

        public bool CheckQuestions()
        {
            if (File.Exists("questions.json"))
            {
                if (new FileInfo("questions.json").Length == 0) return false;

                string jsonFromFile = File.ReadAllText("questions.json");

                List<Question> questions = JsonConvert.DeserializeObject<List<Question>>(jsonFromFile, settings);
                
                for (int i = 1; i <= 12; i++)
                {
                    bool check = false;

                    foreach (var question in questions)
                    {
                        if (question.Difficulty == i)
                        {
                            check = true;
                            break;
                        }
                    }

                    if (!check) return false;
                }
            }
            else return false;

            return true;
        }
    }
}
