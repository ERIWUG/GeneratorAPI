using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GeneratorAPI.Controllers
{
    enum Pics
    {
        Yes,
        No,
        Random
    }


    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly AppDbContext _appDbContext = new AppDbContext();


        [HttpGet("/api/Ticket/GetLinear")]

        public async Task<IActionResult> GetLinear(int id, AppDbContext db, int amount)
        {
            var c = db.QuestionsToAnswers
                            .Where(c => c.QuestionID == id)
                            .Include(c => c.Question)
                            .AsNoTracking()
                            .ToArray();


            return Ok(Generator.GenerateX2(c, 5, 3));

        }

        [HttpGet("/api/Ticket/GetEnum")]


        public async Task<IActionResult> GetEnum(int id, AppDbContext db)
        {
            var c = db.QuestionsToAnswers.Where(c => c.QuestionID == id).ToArray();


            return Ok(Generator.GenerateEnum(c, 5, 5).ToJson());

        }

        [HttpGet("/api/Ticket/GetGroup")]

        public async Task<IActionResult> GetGroup(int id, AppDbContext db)
        {
            var c = db.QuestionsToAnswers
                            .Where(c => c.QuestionID == id)
                            .Include(c => c.Question)
                            .AsNoTracking()
                            .ToArray();


            return Ok(Generator.GenerateGroup(c, 3, 5));

        }

        [HttpGet("/api/Ticket/GetNewParsing")]
        public async Task<IActionResult> GetNewParsing(string str, AppDbContext db)
        {
            string generatorType;
            int fixQwId = 0;
            int idSet = 0;
            int idSetGroup = 0;
            int min = 3;
            int max = 5;
            int[] fixAnswid = new int[5];
            bool O = false, YN = false, X2 = false, ALL = false;
            Pics qwPic, answPic;

            string[] words = str.Trim().Split(',');
            int count = words.Length;
            if (count < 5) //5 параметров должен быть обязательно
                new ArgumentOutOfRangeException();//заменить на нормальный error
            generatorType = words[0];//имя генератора
            switch (words[1][5])//QwPic
            {
                case 'Y':
                    qwPic = Pics.Yes;
                    break;
                case 'N':
                    qwPic = Pics.No;
                    break;
                case 'R':
                    qwPic = Pics.Random;
                    break;
            }

            switch (words[2][6])//AnswPic
            {
                case 'Y':
                    answPic = Pics.Yes;
                    break;
                case 'N':
                    answPic = Pics.No;
                    break;
                case 'R':
                    answPic = Pics.Random;
                    break;
            }
            idSet = int.Parse(words[3]);
            idSetGroup = int.Parse(words[4]);
            try
            {
                min = int.Parse(words[5]);
                max = int.Parse(words[6]);
                if (max < min) max = min;
                int number = int.Parse(words[7]);
                if (number == 1) O = true;
                number = int.Parse(words[8]);
                if (number == 1) X2 = true;
                number = int.Parse(words[9]);
                if (number == 1) ALL = true;
                number = int.Parse(words[9]);
                if (number == 1) YN = true;
                fixQwId = int.Parse(words[11]);//номер вопроса
                                               //номера ответов
                fixAnswid[0] = int.Parse(words[12]);
                fixAnswid[1] = int.Parse(words[13]);
                fixAnswid[2] = int.Parse(words[14]);
                fixAnswid[3] = int.Parse(words[15]);
                fixAnswid[4] = int.Parse(words[16]);

            }
            catch (IndexOutOfRangeException)//если не указаны все параметры, то продолжаем дальше
            {

            }


            List<QuesToAns> questionsArray;

            if (fixQwId != 0)//если задано конкретное id вопроса
            {
                questionsArray = db.QuestionsToAnswers
                           .Where(c => c.QuestionID == fixQwId)
                           .Include(c => c.Question)
                           .AsNoTracking()
                           .ToList();
            }
            else if (idSet != 0)//если задано конкретный idset
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
            }
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
            RezultatEntity t=new RezultatEntity();
            
            switch (generatorType.ToLower())//может быть задан именем или цифрами
            {
                case "combi":
                case "1":
                    if (YN)
                    {
                        AnswerEntity[] answers = null;//явл -- не явл
                        t = Generator.GenerateIsIt(questionsArray.ToArray(), answers);
                    }
                    else if (ALL) t = Generator.GenerateEnum(questionsArray.ToArray(), min, max);
                    else t = Generator.GenerateX2(questionsArray.ToArray(), max, min, O);
                    //а генератор ГХ когда вызывать???
                    break;
                case "onpic":
                case "2":
                    break;
            }

           

            return Ok(t);
        }

        //универсальный, но, увы, не нужный парсинг
        [HttpGet("/api/Ticket/GetHDMI")]
        public async Task<IActionResult> GetRezultat(string str)
        {
            string[] words = str.Split(';');
            int count = words.Length;
            string generatorType;
            int questionId = 0;
            int idSet = 0;
            int idSetGroup = 0;
            int min = 3;
            int max = 5;
            int[] answersIds = new int[0];
            bool O = false, YN = false, X2 = false, ALL = false;
            bool Qid = false;
            int qwPic, ansPic;

            switch (count)
            {
                case 0:
                case 1:
                    throw new ArgumentOutOfRangeException();
                    break;
                case 2:
                    generatorType = words[0].Trim();
                    try
                    {
                        questionId = int.Parse(words[1].Trim());
                    }
                    catch
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    break;
                case 3:
                    generatorType = words[0];
                    idSet = int.Parse(words[1].Trim());
                    idSetGroup = int.Parse(words[2].Trim());
                    break;
                default:
                    generatorType = words[0];
                    for (int i = 1; i < count; i++)
                    {
                        if (words[i].Trim().StartsWith('['))//ограничение на число вариантов ответа
                        {
                            string ogr = words[i].Trim(new Char[] { ' ', '[', ']' });
                            if (ogr.Length == 0 || ogr == " " || ogr == "0")//если ограничения не заданы, то берем по умолчанию
                            {
                            }
                            else if (ogr.Contains('-'))//если задан минимальный и максимальный предел
                            {
                                string[] minMax = ogr.Split("-");
                                min = int.Parse(minMax[0].Trim());
                                max = int.Parse(minMax[1].Trim());
                            }
                            else//если задан только максимальный предел
                            {
                                max = int.Parse(ogr);
                            }
                        }
                        else
                            if (words[i].Trim().StartsWith('{'))//список вариантов ответа
                        {
                            string[] masAns = words[i].Trim(new Char[] { ' ', '{', '}' }).Split(',');

                            answersIds = new int[masAns.Length];
                            for (int j = 0; j < masAns.Length; j++)
                                answersIds[j] = int.Parse(masAns[j].Trim());
                        }
                        else
                            if (words[i].Trim().StartsWith('('))//флаги: O, X2, ALL, YN
                        {
                            string allFlags = words[i];
                            if (allFlags.Contains("O")) O = true;
                            if (allFlags.Contains("X2")) X2 = true;
                            if (allFlags.Contains("ALL")) ALL = true;
                            if (allFlags.Contains("YN")) YN = true;
                        }
                        else
                            if (words[i].Trim().EndsWith("d"))//Qid -- id вопроса
                        {
                            Qid = true;
                            questionId = int.Parse(Regex.Replace(words[i], @"[^\d]", "", RegexOptions.Compiled));
                        }
                        else
                            if (!Qid)//если не задан id вопроса, получаем idSet и idSetGroup 
                        {
                            string[] ids = words[i].Split(',');


                            idSet = int.Parse(ids[0].Trim());
                            idSetGroup = int.Parse(ids[1].Trim());
                        }
                        else
                        if (words[i].EndsWith('Y') || words[i].EndsWith('N') || words[i].EndsWith('R'))
                        {
                            string[] pics = words[i].Split(',');
                            switch (pics[0].Trim()[5])//последний символ строки qwPic
                            {
                                case 'Y':
                                    qwPic = 2;
                                    break;
                                case 'N':
                                    qwPic = 1;
                                    break;
                                case 'R':
                                    qwPic = 0;
                                    break;
                            }
                            switch (pics[1].Trim()[6])//последний символ строки ansPic
                            {
                                case 'Y':
                                    ansPic = 2;
                                    break;
                                case 'N':
                                    ansPic = 1;
                                    break;
                                case 'R':
                                    ansPic = 0;
                                    break;
                            }
                        }
                        else//неверная строка
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                    }
                    break;

            }
            Console.WriteLine(generatorType + " " + questionId + " " + idSet + " " + idSetGroup + " " + min + " " + max + " " + O + " " + YN + " " + X2 + " " + ALL + " " + Qid);
            return null;
        }

    }
}
