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
        /// Генератор, использующийся для генерации простого вопроса с одним правильным и max-1 неправильных ответов
        /// </summary>
        /// <param name="mas"> Массив из данных, необходимый для генерации</param>
        /// <param name="IdSets">IdSet из которых будут браться неправильные ответы</param>
        /// <param name="maxInt">Максимальное число вариантов ответов</param>
        /// <param name="minInt">Минимальное число вариантов ответов</param>
        /// <returns></returns>
        public static RezultatEntity GenerateLinear(QuesToAns[] mas,int[] IdSets, int maxInt = 5, int minInt = 3)
        {
            AppDbContext db = new AppDbContext();
            ParseData(mas, IdSets);
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
            t.Question = mas[0].Question; var c= db.Questions.Where(c => c.Id == mas[0].QuestionID).Include(c => c.IdSet).ThenInclude(c=>c.IdGroup).First() ;
            t.Seed = $"{c.Id}-{c.IdSet.IdGroup.Id}-{c.IdSet.Id}-GL-{ForSeed}-";
            foreach (int i in Answers)
            {
                var qq = db.Answers.Where(c => c.Id == i).First();
                t.Seed += $"{qq.Id}-";
                qq.IdSet =null;

                t.Answers.Add(qq);

            }
            t.Seed += "0";
            t.CorrectAnswer = 0;
            return t;
        }


        
    }





}
