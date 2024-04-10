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
    public class Ticket
    {
        public int QuestionID;
        public List<int> AnswersID;
        public int CorrectAnswer;
        public string QuestionHash;

        public Ticket(int questionID, List<int> answers, int correctAnswer, string questionHash)
        {
            this.QuestionID = questionID;
            this.AnswersID = answers;
            this.CorrectAnswer = correctAnswer;
            QuestionHash = questionHash;
        }

        
    };
}
