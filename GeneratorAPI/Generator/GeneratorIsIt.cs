using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorAPI.Models;

namespace GeneratorAPI
{
    public static partial class Generator
    {
        /// <summary>
        /// Function that generate and print IsItquestion
        /// </summary>
        /// <param name="mas">Array of Data</param>
        /// <param name="amount">Amount of Question</param>
        /// <Author>Belyi Egor</Author>
        public static Question[] GenerateIsIt(QuestionData[] mas, int amount)
        {
            const String APPE = "Являются ли ";
            String ANSW1 = "Являются.";
            String ANSW2 = "Не являются";
            String[] ANSW = { ANSW1, ANSW2 };
            Question[] Quest = new Question[amount];
            String ENDP = "";
            List<int> IntQuest = new List<int>();
            for (int i = 0; i < mas.Length - 1; i++)
            {
                if (mas[i].type == 2)
                {
                    IntQuest.Add(i);
                }
                else
                {
                    if (mas[i].type == 0)
                    {
                        ENDP = mas[i].text;
                    }
                }
            }
            int l = 0;
            String MyHash;
            while (amount-- > 0)
            {

                Random m = new Random();
                int IA = m.Next(IntQuest.Count);
                int AA = IntQuest[IA];
                IntQuest.RemoveAt(IA);
                MyHash = $"DBDATA-G2-{AA}-{(mas[AA].flag ? 0 : 1)}";
                Quest[l] = new Question($"{APPE} {mas[AA].text.ToLower()} {ENDP}?", ANSW, (mas[AA].flag ? 0 : 1), MyHash);


            }

            return Quest;
        }
    }
}
