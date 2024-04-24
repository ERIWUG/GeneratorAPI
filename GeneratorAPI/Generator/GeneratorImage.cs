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
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Host.Mef;
using System.Linq.Expressions;
using System.Xml;
//using Newtonsoft.Json.Linq;

namespace GeneratorAPI
{
    /// <summary>
    /// Class that generate and print questions
    /// </summary>
    public static partial class Generator
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public async static Task<RezultatEntity> GeneratorImage(RezultatEntity entity,int flag)
        {
            AppDbContext db = new AppDbContext();
            Random k = new Random();
            int IND = 0;
            int er = 0;
            switch (flag)
            {
                case 0:
                    var c = await db.Questions.Where(c => c.Id == entity.Question.Id).AsNoTracking().Include(c=>c.Images).FirstAsync();
                    if (c.Images.Count != 0)
                    {
                        entity.Question.Images.Add(c.Images[k.Next(c.Images.Count)]);
                        foreach(var h in entity.Question.Images)
                        {
                            h.Questions = null;
                        }
                    }
                    return entity;
                    break;
                case 1:
                    foreach(var l in entity.Answers)
                    {
                        var q = await db.Answers.Where(c => c.Id == l.Id).Include(c => c.Images).AsNoTracking().FirstAsync();

                        if (q.Images.Count != 0)
                        {
                            entity.Answers[IND].Images.Add(q.Images[k.Next(q.Images.Count)]);
                        }
                        else
                        {
                            er = 1;
                            break;
                        }
                    }

                    if (er == 1)
                    {
                        foreach(var l in entity.Answers)
                        {
                            l.Images = null;
                        }
                    }

                    foreach(var l in entity.Answers)
                    {
                        foreach(var q in l.Images)
                        {
                            q.Answers = null;
                        }
                    }
                    return entity;

                    break;

                 case 2:
                    GeneratorImage(entity, 0);
                    GeneratorImage(entity, 1);
                    return entity;
                    break;
                case -1:
                    return entity;
                    break;

                    
            }

            return null;
        }


        public static async Task<RezultatEntity> GeneratorImage(QuestionEntity q,int[] IdSets, int maxInd = 5, int minInd = 3)
        {
            AppDbContext db = new AppDbContext();
            Random k= new Random();
            RezultatEntity rez = new RezultatEntity();

            int amount = q.Answers.Count;

            if (amount > maxInd)
            {
                maxInd = amount;
            }

            if (amount <=minInd)
            {
                maxInd = amount;
                minInd = amount;
            }

            
            amount = k.Next(minInd, maxInd);
            int e=0;
            List<int> MyList = new List<int>();
            rez.Question = await db.Questions.Where(c=>c.Id == q.Id).AsNoTracking().FirstAsync();

            foreach(var c in q.Answers)
            {
                MyList.Add(e);e++;
            }

            while (amount-- > 0)
            {
                e = k.Next(MyList.Count);
                rez.Answers.Add(q.Answers[MyList[e]]);
                MyList.RemoveAt(e);
            }

            

            var im = await db.Answers.Where(c => c.Id == q.Answers[0].Id).Include(c => c.Images).AsNoTracking().FirstAsync();

            
            
            foreach (var l in rez.Answers)
            {
                l.Questions.Clear();
                l.Images.Clear();
                l.QuestionToAnswer = null;
                l.IdSet = null;
            }
            rez.Question.Answers = null;
            rez.Question.QuestionToAnswer = null;
            rez.CorrectAnswer = 0;
            rez.Answers[0].Images.Add(im.Images[k.Next(im.Images.Count)]);
            rez.Answers[0].Images[0].Answers = null;
            rez.BlockAnswers = [];
            return rez;
        }

       



    }
}