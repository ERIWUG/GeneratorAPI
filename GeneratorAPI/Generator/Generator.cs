using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GeneratorAPI;
using GeneratorAPI.Models;
using System.Net.WebSockets;
using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GeneratorAPI.Models.TempTable;
using Microsoft.EntityFrameworkCore;
using GeneratorAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Newtonsoft.Json.Linq;

namespace GeneratorAPI
{
    /// <summary>
    /// Class that generate and print questions
    /// </summary>
    public static partial class Generator
    {

       
        private static List<int>? CorrectAnswerIndexes = null;
        private static List<int>? IncorrectAnswerIndexes = null;
        private static List<int>? AllAnswersToQuestion=null;

        public enum Pics
        {
            Yes,
            No,
            Random
        }



        public static RezultatEntity GeneratorCombi(int fixQwId, int idSet, int idSetGroup, int min, int max, int[] fixAnswid, bool O, bool YN, bool X2, bool ALL, Pics qwPic, Pics answPic)
        {
            AppDbContext db = new AppDbContext();
            int[] idSets;
            List<QuesToAns> questionsArray;
            questionsArray = db.QuestionsToAnswers.Include(c => c.Question).Where(c=>c.Question.IsNegative==O)
                          .AsNoTracking()
                          .ToList();

            if (fixQwId != 0)//если задано конкретное id вопроса
            {
                questionsArray = db.QuestionsToAnswers
                           .Where(c => c.QuestionID == fixQwId)
                           .Include(c => c.Question)
                           .AsNoTracking()
                           .ToList();
            }
            if (idSet != 0)
            {
                idSets = new int[] { idSet };
            }
            else if (idSetGroup != 0)
            {
                idSets = db.IdSet.Where(c => c.IdGroup.Id == idSetGroup).AsNoTracking().Select(c => c.Id).ToArray();
            }
            else idSets = db.IdSet.AsNoTracking().Select(c => c.Id).ToArray();
            /*  else if (idSet != 0)//если задано конкретный idset
              {
                  var q = db.Questions.Where(c => c.IdSet.Id == idSet).AsNoTracking().Select(c => c.Id).ToArray();
                  questionsArray = db.QuestionsToAnswers
                              .Where(c => q.Contains(c.QuestionID))
                              .Include(c => c.Question)
                              .AsNoTracking()
                               .ToList();
              }
              else if (idSetGroup != 0)//если задано только конкретный idset
              {
                  List<int> set = db.IdSet.Where(c => c.IdGroup.Id == idSetGroup).AsNoTracking().Select(c => c.Id).ToList();
                  var q = db.Questions.Where(c => set.Contains(c.IdSet.Id)).AsNoTracking().Select(c => c.Id).ToArray();
                  questionsArray = db.QuestionsToAnswers
                              .Where(c => q.Contains(c.QuestionID))
                              .Include(c => c.Question)
                              .AsNoTracking()
                              .ToList();
              }
              else//если ничего не зодано -- все вопросы
              {
                  questionsArray = db.QuestionsToAnswers.Include(c => c.Question)
                              .AsNoTracking()
                              .ToList(); ;
              }*/
            if (fixAnswid[0] != 0)//если заданы конкретные id вопросов, то фильтруем из массива, чтоб были только они
                                  //иначе -- оставляем весь массив для рандомной выборки внутри генератора
            {
                List<QuesToAns> copyQuestionArray = new List<QuesToAns>(questionsArray);
                foreach (QuesToAns q in questionsArray)
                {
                    foreach (int ansId in fixAnswid)
                    {
                        if (ansId != 0 && q.AnswerID != ansId) questionsArray.Remove(q);
                    }
                }
                Random r = new Random();
                while (questionsArray.Count < max)//если при этом не хватило, добираем рандомными вопросами 
                {
                    int k = r.Next(questionsArray.Count);
                    if (!questionsArray.Contains(copyQuestionArray[k]))
                        questionsArray.Add(copyQuestionArray[k]);
                }
            }
                RezultatEntity t = new RezultatEntity();

               
                        if (YN)
                        {
                            AnswerEntity[] answers = null;//явл -- не явл
                            t = Generator.GenerateIsIt(questionsArray.ToArray(), idSets, answers);
                        }
                        else if (ALL) t = Generator.GenerateEnum(questionsArray.ToArray(), idSets, min, max);
                        else if (X2) t = Generator.GenerateGroup(questionsArray.ToArray(), idSets, min, max);
                        else t = Generator.GenerateLinear(questionsArray.ToArray(), idSets, max, min);

     
            return t;
            }


            private static void UpdateDictionary()
        {
            AppDbContext db = new AppDbContext();
           
           
            


        }
        

        private static void ParseData(QuesToAns[] mas, int[] IdSets)
        {
            AppDbContext db = new AppDbContext();
            if (CorrectAnswerIndexes is null)
            {
                CorrectAnswerIndexes = new List<int>();
                IncorrectAnswerIndexes = new List<int>();   
                AllAnswersToQuestion= new List<int>();
            }
            CorrectAnswerIndexes.Clear();
            IncorrectAnswerIndexes.Clear();
            AllAnswersToQuestion.Clear();


            foreach(var c in IdSets)
            {
                var q = db.IdSet.AsNoTracking().Where(l => l.Id == c).Include(l=>l.Answers);
                foreach(var kl in q)
                {
                    foreach (var kl2 in kl.Answers)
                    {
                        IncorrectAnswerIndexes.Add(kl2.Id);
                    }
                }
                




            }


            AllAnswersToQuestion.AddRange(IncorrectAnswerIndexes);




            foreach(var ind in mas)
            {
                CorrectAnswerIndexes.Add(ind.AnswerID);
                IncorrectAnswerIndexes.Remove(ind.AnswerID);
            }

        }



        ///// <summary>
        ///// Метод для перемешки списка ответов
        ///// </summary>
        ///// <param name="originalList"></param>
        ///// <Author>Veremeychik Alex</Author>
        //public static int Shuffling(List<string> originalList, int index, List<string> HashShufflingList)
        //{

        //    Random random = new Random();
        //    for (int i = originalList.Count - 1; i >= 1; i--)
        //    {

        //        int j = random.Next(i + 1);
        //        // Обменять значения originalList[j] и originalList[i]
        //        string temp = originalList[j];
        //        originalList[j] = originalList[i];
        //        originalList[i] = temp;
        //        temp = HashShufflingList[j];
        //        HashShufflingList[j] = HashShufflingList[i];
        //        HashShufflingList[i] = temp;

        //        if (i == index)
        //        {

        //            index = j;
        //        }
        //        else if (j == index)
        //        {

        //            index = i;
        //        }

        //    }
        //    return index;
        //}
        ///// <summary>
        ///// Генерация билета с указанным числом вопросов
        ///// </summary>
        ///// <param name="mas"></param>
        ///// <param name="questAmount">число вопросов в билете</param>
        ///// <Author>Nichiporuk Viktor</Author>
        //public static
        //    Question[] GenerateTicket(QuestionEntity[] mas, int questAmount)
        //{
        //    Question[] questions1 = new Question[questAmount];
        //    Random rand = new Random();
        //    int countOfTypes = 5;//число типов вопросов (их  5 потом будет)
        //    int[] questions = new int[countOfTypes];
        //    for (int i = 0; i < questAmount; i++)
        //    {
        //        int type;
        //        do
        //        {
        //            type = rand.Next(countOfTypes);
        //        }
        //        while (questions[type] > questAmount / countOfTypes);
        //        // if (type == prevType) type = rand.Next(countOfTypes);
        //        switch (type)
        //        {
        //            case 0:
        //                //questions1[i] = GenerateLinear(mas, 5, 1, true)[0];
        //                questions[0]++;
        //                break;
        //            case 1:
        //                // questions1[i] = GenerateLinear(mas, 5, 1, false)[0];
        //                questions[1]++;
        //                break;
        //            case 2:
        //                questions1[i] = GenerateEnum(mas, 5, 1)[0];
        //                questions[2]++;
        //                break;
        //            case 3:
        //                questions1[i] = GenerateIsIt(mas, 1)[0];
        //                questions[3]++;
        //                break;

        //            case 4:
        //                questions1[i] = GenerateGroup(mas, 5, 1)[0];
        //                questions[4]++;
        //                break;
        //        }

        //    }
        //    return questions1;
        //}

        ///// <summary>
        ///// Generate a question from hash function
        ///// </summary>
        ///// <param name="hash">hash</param>
        ///// <Author>Nichiporuk Viktor</Author>
        //public static Question degeneration(QuestionEntity[] mas, String hash)
        //{
        //    //локальный массив для отладки метода
        //    string[] words = hash.Split('-');
        //    string genType = words[1];
        //    int questionIndex;
        //    String question;
        //    String[] answers;
        //    int rightAnswer;
        //    if (genType == "G2")
        //    {
        //        int wordNum = int.Parse(words[2]);
        //        string word = mas[wordNum].Text; //надо будет заменить на чтение из БД
        //        question = "Являются ли " + word.ToLower() + " " + mas[0].Text;
        //        answers = new string[2];
        //        answers[0] = "Являются.";
        //        answers[1] = "Не являются";
        //        rightAnswer = int.Parse(words[3]);
        //    }
        //    else
        //    {
        //        questionIndex = int.Parse(words[2]);
        //        question = mas[questionIndex].Text; //надо будет заменить на чтение из БД
        //        int amountOfAnswers = words.Length - 5;
        //        int step = 4;

        //        if (genType == "GX")
        //        {
        //            amountOfAnswers = words.Length - 6;
        //            step = 5;
        //            string[] BlockOfAnswNumbers = words[3].Split(',');
        //            int[] ans = new int[BlockOfAnswNumbers.Length];
        //            for (int i = 0; i < ans.Length; i++)
        //                ans[i] = int.Parse(BlockOfAnswNumbers[i]);
        //            string OneOfAnsw = "";
        //            for (int i = 0; i < ans.Length; i++)
        //                OneOfAnsw += "\n-" + (mas[ans[i]].Text);
        //            question += $"{OneOfAnsw}";

        //        }

        //        answers = new String[amountOfAnswers];

        //        for (int i = 0; i < amountOfAnswers; i++)
        //        {
        //            int answersIndex = 0;
        //            string value = words[i + step];
        //            if (words[i + step] == $"A1") answers[i] = (i + 1) + ") " + "Все перечисленное";
        //            else if (words[i + step] == $"A2") answers[i] = (i + 1) + ") " + "Ничего из перечисленного";
        //            else if (genType == "GX")
        //            {

        //                string[] ansNumbers = value.Split(',');
        //                int[] ans = new int[ansNumbers.Length];
        //                for (int j = 0; j < ans.Length; j++)
        //                    ans[j] = int.Parse(ansNumbers[j]);
        //                string variant = "";
        //                for (int j = 0; j < ans.Length; j++)
        //                    variant += (mas[ans[j]].Text + ", ");
        //                answers[i] = (i + 1) + ") " + variant;
        //            }
        //            else
        //            {
        //                answersIndex = int.Parse(words[i + step]);
        //                answers[i] = (i + 1) + ") " + mas[answersIndex].Text;//надо будет заменить на чтение из БД
        //            }
        //        }
        //        rightAnswer = int.Parse(words[step + amountOfAnswers]);
        //    }
        //    Question q = new Question(question, answers, rightAnswer, hash);
        //    return q;
        //    return null;
        //}


    }
}
