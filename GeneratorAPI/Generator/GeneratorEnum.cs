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
        /// Генератор, использующийся для генерации вопросов с вариантами ответа "Все перечисленное"/"Ничего из перечисленного"
        /// Method for generating Ticket where one correct and some incorrect answers.
        /// The correct answer can be "all of the above" or "none of all the above"
        /// </summary>
        /// <param name="mas">Data for generating</param>
        /// <param name="ogr">Max amount of answers in one ticket</param>
        /// <returns>One Ticket with one correct and some Incorrect Question</returns>
        public static RezultatEntity GenerateEnum(QuesToAns[] mas, int[] IdSets, int minInt, int maxInt)
        {
            AppDbContext db = new AppDbContext();
            ParseData(mas, IdSets);
            Random k = new Random();
            List<int> Answers = new List<int>();
            int allOrNo = k.Next(0, 3);
            int DeletingIndex = 0;
            if (minInt < 4) minInt = 4;
            int NowAmountAnswers = k.Next(minInt - 2, maxInt - 2);
            int ForSeed = NowAmountAnswers;
            var t = new RezultatEntity();
            mas[0].Question.Answers = null;
            mas[0].Question.QuestionToImage = null;
            mas[0].Question.QuestionToAnswer = null;
            t.Question = mas[0].Question; var c = db.Questions.Where(c => c.Id == mas[0].QuestionID).Include(c => c.IdSet).ThenInclude(c => c.IdGroup).First();
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
                    var qq = db.Answers.Where(c => c.Id == i).First();
                    t.Seed += $"{qq.Id}-";
                    qq.IdSet.Answers = null;
                    qq.IdSet.IdGroup.IdSets = null;
                    qq.IdSet.Questions = null;
                    qq.IdSet.Images = null;
                    t.Answers.Add(qq);

                }
                var qq1 = db.Answers.Where(c => c.Id == 73).Include(c => c.IdSet).First();//все перечисленное
                t.Seed += $"{qq1.Id}-";
                qq1.IdSet.Answers = null;
            //    qq1.IdSet.IdGroup.IdSets = null;
                qq1.IdSet.Questions = null;
                qq1.IdSet.Images = null;
                t.Answers.Add(qq1);
                 qq1 = db.Answers.Where(c => c.Id == 74).Include(c => c.IdSet).First();//ничего  перечисленное
                t.Seed += $"{qq1.Id}-";
                qq1.IdSet.Answers = null;
             //   qq1.IdSet.IdGroup.IdSets = null;
                qq1.IdSet.Questions = null;
                qq1.IdSet.Images = null;
                t.Answers.Add(qq1);
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
                    var qq = db.Answers.Where(c => c.Id == i).First();
                    t.Seed += $"{qq.Id}-";
                    qq.IdSet.Answers = null;
                    qq.IdSet.IdGroup.IdSets = null;
                    qq.IdSet.Questions = null;
                    qq.IdSet.Images = null;
                    t.Answers.Add(qq);

                }
                var qq1 = db.Answers.Where(c => c.Id == 73).Include(c => c.IdSet).First();//все перечисленное
                t.Seed += $"{qq1.Id}-";
              //  qq1.IdSet.IdGroup.IdSets = null;
                qq1.IdSet.Questions = null;
                qq1.IdSet.Images = null;
                t.Answers.Add(qq1);
                qq1 = db.Answers.Where(c => c.Id == 74).Include(c => c.IdSet).First();//ничего  перечисленное
                t.Seed += $"{qq1.Id}-";
             //   qq1.IdSet.IdGroup.IdSets = null;
                qq1.IdSet.Questions = null;
                qq1.IdSet.Images = null;
                t.Answers.Add(qq1);
                t.Seed += ForSeed - 2;
                t.CorrectAnswer = ForSeed - 2;
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
                    var qq = db.Answers.Where(c => c.Id == i).First();
                    t.Seed += $"{qq.Id}-";
                    qq.IdSet.IdGroup.IdSets = null;
                    qq.IdSet.Questions = null;
                    qq.IdSet.Images = null;
                    t.Answers.Add(qq);

                }

                var qq1 = db.Answers.Where(c => c.Id == 73).Include(c=>c.IdSet).First();//все перечисленное
                t.Seed += $"{qq1.Id}-";
             //   qq1.IdSet.IdGroup.IdSets = null;
                qq1.IdSet.Questions = null;
                qq1.IdSet.Images = null;
                t.Answers.Add(qq1);
                qq1 = db.Answers.Where(c => c.Id == 74).Include(c => c.IdSet).First();//ничего  перечисленное
                t.Seed += $"{qq1.Id}-";
             //   qq1.IdSet.IdGroup.IdSets = null;
                qq1.IdSet.Questions = null;
                qq1.IdSet.Images = null;
                t.Answers.Add(qq1);
                t.Seed += ForSeed - 1;
                t.CorrectAnswer = ForSeed - 1;
            }
            return t;
        }
    }
}