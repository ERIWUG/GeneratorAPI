using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
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
            var c = db.QuestionsToAnswers
                            .Where(c => c.QuestionID == id)
                            .Include(c => c.Question)
                            .AsNoTracking()
                            .ToArray();


            return Ok(Generator.GenerateX2(c, 5,3));
            
        }

        [HttpGet("/api/Ticket/GetEnum")]


        public async Task<IActionResult> GetEnum(int id, AppDbContext db)
        {
            var c = db.QuestionsToAnswers.Where(c => c.QuestionID == id).ToArray();


            return Ok(Generator.GenerateEnum(c, 5, 5).ToJson());

        }

        [HttpGet("/api/Ticket/GetHDMI")]
        public async Task<IActionResult> GetRezultat(string str)
        {
            string[] words = str.Split(';');
            int count = words.Length;
            string generatorType;
            int questionId=0;
            int idSet=0;
            int idSetGroup=0;
            int min=3;
            int max=5;
            int[] answersIds=new int[0];
            bool O=false, YN = false, X2 = false, ALL = false;
            bool Qid=false;
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
                    for (int i=1; i < count; i++)
                    {
                        if (words[i].Trim().StartsWith('['))//ограничение на число вариантов ответа
                        {
                            string ogr = words[i].Trim(new Char[] { ' ', '[', ']' });
                            if (ogr.Length==0||ogr==" "||ogr=="0")//если ограничения не заданы, то берем по умолчанию
                            {
                            }
                            else if (ogr.Contains('-'))//если задан минимальный и максимальный предел
                            {
                                string[]minMax=ogr.Split("-");
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
                            for (int  j=0; j<masAns.Length; j++)
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
                        if (words[i].EndsWith('Y')|| words[i].EndsWith('N')|| words[i].EndsWith('R'))
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
            Console.WriteLine(generatorType + " " + questionId + " " + idSet + " " + idSetGroup + " " + min + " " + max  + " " + O + " " + YN + " " + X2 + " "+ALL+" "+Qid);           
         //   QuesToAns[] mas;
        //    if (Qid) mas = null; //questionId???????;//если задан вопрос, то только его взять
         //   else if (answersIds.Length!=0) mas = null;//если задан список  вариантов ответа
        //    else mas = null;//get по idSet

         /*   switch (generatorType)
            {
                
                case "Combi":
                    if (X2) return Ok(Generator.GenerateGroup(null, max));
                    if (ALL) return Ok(Generator.GenerateEnum(null, min, max));
                    return Ok(Generator.GenerateEnum(null, min, max));
                    break;

            }*/
            return null;
        }

    }
}
