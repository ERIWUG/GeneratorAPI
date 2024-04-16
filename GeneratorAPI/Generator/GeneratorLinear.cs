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
        public static RezultatEntity GenerateX2(QuesToAns[] mas, int maxInt = 5, int minInt = 3, bool flag = false)
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
            mas[0].Question.Answers = null;
            mas[0].Question.QuestionToImage = null;
            mas[0].Question.QuestionToAnswer = null;
            t.Question = mas[0].Question; var c= db.Questions.Where(c => c.Id == mas[0].QuestionID).Include(c => c.Theme).First() ;
            t.Seed = $"{c.Id}-{c.Theme.Id}-{c.IdSet}-GL-{ForSeed}-";
            foreach (int i in Answers)
            {
                var qq = db.Answers.Where(c => c.Id == i).First();
                t.Seed += $"{qq.Id}-";
                t.Answers.Add(qq);

            }
            t.Seed += "0";
            t.CorrectAnswer = 0;
            return t;
        }


        
    }





}
