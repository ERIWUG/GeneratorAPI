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
        public string questionText;
        public string[] answers;
        public int correctAnswer;
        public string QuestionHash;

        public Question(string questionText, string[] answers, int correctAnswer, string questionHash)
        {
            this.questionText = questionText;
            this.answers = answers;
            this.correctAnswer = correctAnswer;
            QuestionHash = questionHash;
        }

        public void print()
        {
            Console.WriteLine(questionText);
            for (int i = 0; i < answers.Length; i++)
                Console.WriteLine(answers[i]);
            Console.WriteLine("Номер правильного ответа: " + correctAnswer + "\n" + QuestionHash);
        }
    };
}
