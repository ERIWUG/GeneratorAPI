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
                    return entity;

                    break;

                 case 2:
                    GeneratorImage(entity, 0);
                    GeneratorImage(entity, 1);
                    return entity;
                    break;


                    
            }

            return null;
        }


        public static async Task<RezultatEntity> GeneratorImage(ImageEntity im, int[] IdSets, int maxInd = 5, int minInd = 3)
        {
            QuestionEntity q;
            Random k= new Random();
            try
            {
                 q = im.Questions[k.Next(im.Questions.Count)];
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return null;
            }
            RezultatEntity rez = GenerateLinear(q.QuestionToAnswer.ToArray(), IdSets, maxInd, minInd);
            im.Questions = null;
            rez.Images.Add(im);
            rez.Question.Images = null;
            return rez;
        }

       



    }
}