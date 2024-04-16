using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol;
using System.Text.RegularExpressions;

namespace GeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly AppDbContext _appDbContext = new AppDbContext();
        

        [HttpGet("/api/Ticket/GetLinear")]

        public async Task<IActionResult> GetLinear(int id,AppDbContext db,int amount)
        {
            var c = db.QuestionsToAnswers.Where(c => c.QuestionID == id).ToArray();


            return Ok(Generator.GenerateX2(c, 3, 5).ToJson());
            
        }

        [HttpGet("/api/Ticket/GetEnum")]


        public async Task<IActionResult> GetEnum(int id, AppDbContext db)
        {
            var c = db.QuestionsToAnswers.Where(c => c.QuestionID == id).ToArray();


            return Ok(Generator.GenerateEnum(c, 5,5).ToJson(0));

        }

        [HttpGet("/api/Ticket/GetHDMI")]
        public async Task<IActionResult> GetRezultat(string str)
        {
            string[] words = str.Split(';');
            int count = words.Length;
            string generatorType;
            int questionId;
            int idSet;
            int idSetGroup;
            int min=3;
            int max=5;
            int[] answersIds;
            bool O=false, YN = false, X2 = false, ALL = false;
            bool Qid=false;
            switch (count)
            {
                case 0:
                case 1:
                    throw new ArgumentOutOfRangeException();
                    break;
                case 2:
                    generatorType = words[0];
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
                    for (int i=1; i < count; i++)
                    {
                        if (words[i].Trim().StartsWith('['))//ограничение на число вариантов ответа
                        {
                            string ogr = words[i].Trim();
                            if (ogr.Length==0||ogr==" "||ogr=="0")//если ограничения не заданы, то берем по умолчанию
                            {
                            }
                            else if (ogr.Contains(","))//если задан минимальный и максимальный предел
                            {
                                string[]minMax=ogr.Split(",");
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
                            string[] mas = words[i].Split(',');
                            answersIds = new int[mas.Length];
                            for (int  j=0; j<mas.Length; j++)
                                answersIds[j] = int.Parse(mas[j].Trim());
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
                            string[] ids=words[i].Split(',');
                            idSet = int.Parse(ids[0].Trim()); 
                            idSetGroup = int.Parse(ids[1].Trim());
                        }
                        else
                        if (words[i].EndsWith('Y')|| words[i].EndsWith('N')|| words[i].EndsWith('R'))
                        {

                        }
                    }
                    break;

            }
            

            return null;
        }

    }
}
