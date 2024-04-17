using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// Function that generate and print IsItquestion
        /// </summary>
        /// <param name="mas">Array of Data</param>
        /// <param name="amount">Amount of Question</param>
        /// <Author>Belyi Egor</Author>
        public static RezultatEntity GenerateIsIt(QuesToAns[] mas, AnswerEntity[] ans)
        {
            AppDbContext db = new AppDbContext();
            ParseData(mas);
            Random k = new Random();
            int i = k.Next();
            AnswerEntity n;
            if (i % 2 == 0)
            {
                n = db.Answers.Where(c => c.Id == CorrectAnswerIndexes[k.Next(CorrectAnswerIndexes.Count)]).First();
            }
            else
            {
                n = db.Answers.Where(c => c.Id == IncorrectAnswerIndexes[k.Next(IncorrectAnswerIndexes.Count)]).First();
            }

            
            var t = new RezultatEntity();
            t.Question = mas[0].Question;
            t.Answers.Add(n); t.Answers.Add(ans[0]); t.Answers.Add(ans[1]);
            //t.Seed = $"{t.Question.Id}-{t.Question.Theme.Id}-{t.Question.IdSet}-{n}-ISIT-2-{ans[0].Id}-{ans[1].Id}";



            return t;
           
        }
    }
}
