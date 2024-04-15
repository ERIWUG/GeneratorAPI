using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;
using Microsoft.EntityFrameworkCore;

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
        public static RezultatEntity GenerateLinear(QuesToAns[] mas, int minInt,int maxInt)
        {
            AppDbContext db = new AppDbContext();
            ParseData(mas);
            int DeletingIndex = 0;
            Random k = new Random();
            List<int> Answers = new List<int>();
            int NowAmountAnswers = k.Next(minInt,maxInt);
            int ForSeed = NowAmountAnswers;
            Answers.Add(CorrectAnswerIndexes[k.Next(CorrectAnswerIndexes.Count)]);
            while (NowAmountAnswers-- > 0)
            {
                DeletingIndex = k.Next(IncorrectAnswerIndexes.Count);
                Answers.Add(IncorrectAnswerIndexes[DeletingIndex]);
                IncorrectAnswerIndexes.RemoveAt(DeletingIndex);
            }
            var t = new RezultatEntity();
            t.Question = mas[0].Question;
            t.Seed = $"{t.Question.IdGroup}-{t.Question.IdSet}-GL-QwPicNo-AnswPicNo-{ForSeed}-";
            foreach (int i in Answers)
            {
                var c = db.Answers.Where(c => c.Id == i).First();
                t.Seed += $"{c.Id}-";
                t.Answers.Add(c);

            }
            t.Seed += "0";
            t.CorrectAnswer = 0;
            return t;
        }
    }
}
