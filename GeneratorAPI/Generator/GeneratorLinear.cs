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
        /// Method for generating TicketEntity with one correct and some incorrect answers
        /// </summary>
        /// <param name="mas">Data for generating</param>
        /// <param name="ogr">Max amount of answers in one TicketEntity</param>
        /// <returns>One TicketEntity with one correct and some Incorrect Question</returns>
        public static RezultatEntity GenerateLinear(QuesToAns[] mas, int ogr)
        {
            ParseData(mas);
            int DeletingIndex = 0;
            Random k = new Random();
            List<int> Answers = new List<int>();
            int NowAmountAnswers = k.Next(2, ogr);
            Answers.Add(CorrectAnswerIndexes[k.Next(CorrectAnswerIndexes.Count)]);
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
            return new RezultatEntity();
        }
    }
}
