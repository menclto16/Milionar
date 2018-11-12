using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milionar
{
    class Question
    {
        public string QuestionText { get; set; }
        public List<Answer> Answers = new List<Answer>();
        public int Difficulty { get; set; }

        public void AddAnswer(string text)
        {
            Answer answer = new Answer();
            answer.Content = text;
            Answers.Add(answer);
        }

        public string GetRightAnswer()
        {
            return Answers[0].Content;
        }
    }
}
