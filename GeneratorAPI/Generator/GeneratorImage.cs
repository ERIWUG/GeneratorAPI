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
//using Newtonsoft.Json.Linq;

namespace GeneratorAPI
{
    /// <summary>
    /// Class that generate and print questions
    /// </summary>
    public static partial class Generator
    {



        public static RezultatEntity GeneratorImage(QuestionEntity q, int[] IdSets,bool flag, int maxInd = 5, int minInd = 3)
        {
            if (flag) { GeneratorImage2(q, IdSets, maxInd, minInd); }
            RezultatEntity entity = GenerateLinear(q.QuestionToAnswer.ToArray(), IdSets, maxInd, minInd);
            entity.Question = q;
            Random k = new Random();
            entity.Images.Add(q.Images[k.Next(q.Images.Count)]);
            return entity;
        }


        public static RezultatEntity GeneratorImage(ImageEntity im, int[] IdSets, int maxInd = 5, int minInd = 3)
        {
            
            Random k= new Random();
            QuestionEntity q = im.Questions[k.Next(im.Questions.Count)];
            RezultatEntity rez = GenerateLinear(q.QuestionToAnswer.ToArray(), IdSets, maxInd, minInd);

            rez.Images.Add(im);

            return rez;
        }

        public static RezultatEntity GeneratorImage2(QuestionEntity q, int[] IdSets, int maxInd = 5, int minInd = 3)
        {
            RezultatEntity entity = GenerateLinear(q.QuestionToAnswer.ToArray(), IdSets, maxInd, minInd);
            Random random = new Random();
            List<int> ImageIDS = new List<int>();
            foreach(var l in entity.Answers)
            {
                var im = l.Images[random.Next(l.Images.Count)];
                while (ImageIDS.Contains(im.Id))
                {
                    im = l.Images[random.Next(l.Images.Count)];
                }
                ImageIDS.Add(im.Id);
                
                entity.Images.Add(im);
            }
            return entity;
        }



    }
}