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
        /// Method for generating Ticket where one correct and some incorrect answers.
        /// The correct answer can be "all of the above" or "none of all the above"
        /// </summary>
        /// <param name="mas">Data for generating</param>
        /// <param name="ogr">Max amount of answers in one ticket</param>
        /// <returns>One Ticket with one correct and some Incorrect Question</returns>
        public static RezultatEntity GenerateEnum(QuesToAns[] mas, int minInt, int maxInt)
        {
            AppDbContext db = new AppDbContext();
            ParseData(mas);
            Random k = new Random();
            List<int> Answers = new List<int>();
            int allOrNo = k.Next(0, 3);
            int DeletingIndex = 0;
            if (minInt < 4) minInt = 4;
            int NowAmountAnswers = k.Next(minInt-2, maxInt - 2);
            int ForSeed = NowAmountAnswers;
            var t = new RezultatEntity();
            t.Question = mas[0].Question;
        //    t.Seed = $"{t.Question.Id }-{t.Question.IdSet}-GL-QwPicNo-AnswPicNo-{ForSeed}-";
            if (allOrNo == 0)
            {
                Answers.Add(CorrectAnswerIndexes[k.Next(CorrectAnswerIndexes.Count)]);
                while (NowAmountAnswers-- > 2)
                {
                    DeletingIndex = k.Next(IncorrectAnswerIndexes.Count);
                    Answers.Add(IncorrectAnswerIndexes[DeletingIndex]);
                    IncorrectAnswerIndexes.RemoveAt(DeletingIndex);
                }
                foreach (int i in Answers)
                {
                    var c = db.Answers.Where(c => c.Id == i).First();
                    t.Seed += $"{c.Id}-";
                    t.Answers.Add(c);

                }
                t.Answers.Add(new AnswerEntity());
                t.Answers.Add(new AnswerEntity());
          /**      t.Seed += $"{c.Id}-";
                t.Seed += $"{c.Id}-";*/
                t.Seed += "0";
                t.CorrectAnswer = 0;
            }
            else if (allOrNo == 1)//if "all of the above"
            {

                while (NowAmountAnswers-- > 0)
                {
                    DeletingIndex = k.Next(IncorrectAnswerIndexes.Count);
                    Answers.Add(IncorrectAnswerIndexes[DeletingIndex]);
                    IncorrectAnswerIndexes.RemoveAt(DeletingIndex);
                }
                foreach (int i in Answers)
                {
                    var c = db.Answers.Where(c => c.Id == i).First();
                    t.Seed += $"{c.Id}-";
                    t.Answers.Add(c);

                }
                t.Answers.Add(new AnswerEntity());
                t.Answers.Add(new AnswerEntity());
            /*    t.Seed += $"{c.Id}-";
                t.Seed += $"{c.Id}-";*/
                t.Seed += ForSeed-2;
                t.CorrectAnswer = ForSeed-2;
            }
            else //if "none of the above"
            {

                while (NowAmountAnswers-- > 0)
                {
                    DeletingIndex = k.Next(IncorrectAnswerIndexes.Count);
                    Answers.Add(IncorrectAnswerIndexes[DeletingIndex]);
                    IncorrectAnswerIndexes.RemoveAt(DeletingIndex);
                }
                foreach (int i in Answers)
                {
                    var c = db.Answers.Where(c => c.Id == i).First();
                    t.Seed += $"{c.Id}-";
                    t.Answers.Add(c);

                }
                
                t.Answers.Add(new AnswerEntity());
                t.Answers.Add(new AnswerEntity());
          /*      t.Seed += $"{c.Id}-";
                t.Seed += $"{c.Id}-";*/
                t.Seed += ForSeed-1;
                t.CorrectAnswer = ForSeed - 1;
            }
            return t;
        }
    }
}
