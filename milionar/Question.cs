﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milionar
{
    class Question
    {
        public List<Answer> Answers = new List<Answer>();

        public string GetRightAnswer()
        {
            return Answers[0].Content;
        }
    }
}
