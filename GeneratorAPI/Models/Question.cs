using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorAPI.Models
{
    [Serializable]
    public class Question
    {
        public string QuestionText;
        public string[] Answers;
        public int CorrectAnswer;
        public string QuestionHash;

        public Question(string questionText, string[] answers, int correctAnswer, string questionHash)
        {
            this.QuestionText = questionText;
            this.Answers = answers;
            this.CorrectAnswer = correctAnswer;
            QuestionHash = questionHash;
        }

        public void print()
        {
            Console.WriteLine(QuestionText);
            for (int i = 0; i < Answers.Length; i++)
                Console.WriteLine(Answers[i]);
            Console.WriteLine("Номер правильного ответа: " + CorrectAnswer + "\n" + QuestionHash);
        }
    };
}
