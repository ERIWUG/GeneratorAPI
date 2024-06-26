﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace GeneratorAPI
{
    public static partial class Generator
    {
        /// <summary>
        /// Генератор, использующий для генерации вопросов типа Является/ Не является.
        /// При этом вопрос для этого генератора должен иметь __ Для замены на ответ
        /// </summary>
        /// <param name="mas">Массив из данных, необходимых для генерирования</param>
        /// <param name="IdSets">ID set ответов</param>
        /// <param name="ans">Ответы является/Не является из бд</param>
        /// <returns></returns>
        public static RezultatEntity GenerateIsIt(QuesToAns[] mas, int[] IdSets,AnswerEntity[] ans)
        {
            AppDbContext db = new AppDbContext();
            ParseData(mas,IdSets);
            Random k = new Random();
            int i = k.Next();
            int c = 0;
            AnswerEntity n;
            if (i % 2 == 0)
            {
                c = 0;
                n = db.Answers.Where(c => c.Id == CorrectAnswerIndexes[k.Next(CorrectAnswerIndexes.Count)]).First();
            }
            else
            {
                c = 1;
                n = db.Answers.Where(c => c.Id == IncorrectAnswerIndexes[k.Next(IncorrectAnswerIndexes.Count)]).First();
            }

            
            var t = new RezultatEntity();
            mas[0].Question.Answers = null;
            mas[0].Question.QuestionToImage = null;
            mas[0].Question.QuestionToAnswer = null;
            var l = new QuestionEntity();
            l.Text = mas[0].Question.Text.Replace("__", n.Text);
            t.Question = l;
            t.Answers.Add(n); t.Answers.Add(ans[0]); t.Answers.Add(ans[1]);
            t.Seed = $"{mas[0].Question.Id}-{mas[0].Question.IdSet.IdGroup.Id}-{t.Question.IdSet.Id}-{l}-ISIT-2-{ans[0].Id}-{ans[1].Id}-{c}";
            return t;
           
        }
    }
}
