using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;
using Microsoft.EntityFrameworkCore;

namespace GeneratorAPI
{
    public static partial class Generator
    {
        public static RezultatEntity? GeneratePseudoHard(QuesToAns[] mas, int[] Answers)
        {
            AppDbContext db = new AppDbContext();
            var b = new RezultatEntity();
            var t = mas[0].Question;
            List<int> Ans = new List<int>();
            foreach(var c in mas)
            {
                Ans.Add(c.AnswerID);
            }

            b.Seed=$"{t.Id}-{t.Theme.Id}-{t.IdSet}-GL-{Answers.Length}-";
            int l = -1;
            b.Question = mas[0].Question;
            foreach(int i in Answers)
            {
                if (Ans.Contains(i)) l = i; 
                b.Seed += $"{i}-";
                b.Answers.Add(db.Answers.Where(c => c.Id == i).First());
            }
            if(l ==-1) { return null; }
            b.Seed += $"{l}";




            return b;
        }


    }
}
