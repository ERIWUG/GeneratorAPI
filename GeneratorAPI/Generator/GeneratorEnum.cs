using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;

namespace GeneratorAPI
{
    public static partial class Generator
    {
        /// <summary>
        /// Method for generating Ticket where one correct and some incorrect answers.
        /// The correct answer can be "all of the above" or "none of all the above"
        /// </summary>
        /// <param name="mas">Data for generating</param>
        /// <param name="ogr">Max amount of answers in one ticket</param>
        /// <returns>One Ticket with one correct and some Incorrect Question</returns>
        public static RezultatEntity GenerateEnum(QuesToAns[] mas, int ogr)
        {
            ParseData(mas);
            Random k = new Random();
            RezultatEntity ticket;
            List<int> Answers = new List<int>();
            int allOrNo = k.Next(0, 3);
            int DeletingIndex = 0;
            if (allOrNo == 0)
            {
                ticket = GenerateLinear(mas, ogr - 2);
            }
            else if (allOrNo == 1)//if "all of the above"
            {
                int NowAmountAnswers = k.Next(2, ogr - 2);
                while (NowAmountAnswers-- > 0)
                {
                    DeletingIndex = k.Next(CorrectAnswerIndexes.Count);
                    Answers.Add(CorrectAnswerIndexes[DeletingIndex]);
                    CorrectAnswerIndexes.RemoveAt(DeletingIndex);
                }
                List<List<int>> listOfLists = new List<List<int>>();
                foreach (int OneAnswer in Answers)
                {
                    List<int> newList = new List<int>();
                    newList.Add(OneAnswer);
                    listOfLists.Add(newList);
                }
                ticket = new RezultatEntity();
            }
            else //if "none of the above"
            {
                int NowAmountAnswers = k.Next(2, ogr - 2);
                while (NowAmountAnswers-- > 0)
                {
                    DeletingIndex = k.Next(IncorrectAnswerIndexes.Count);
                    Answers.Add(IncorrectAnswerIndexes[DeletingIndex]);
                    IncorrectAnswerIndexes.RemoveAt(DeletingIndex);
                }
                List<List<int>> listOfLists = new List<List<int>>();
                foreach (int OneAnswer in Answers)
                {
                    List<int> newList = new List<int>();
                    newList.Add(OneAnswer);
                    listOfLists.Add(newList);
                }
                ticket = new RezultatEntity();
            }

            //ticket.AnswersID.Add(-1);//все перечисленое
            //ticket.AnswersID.Add(-2);//ничего из перечисленного
            return ticket;
        }
    }
}
