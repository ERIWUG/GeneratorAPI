﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using GeneratorAPI.Models;
//using GeneratorAPI.Models.Entities;

//namespace GeneratorAPI
//{
//    public static partial class Generator
//    {
//        /// <summary>
//        /// Function that generate and print IsItquestion
//        /// </summary>
//        /// <param name="mas">Array of Data</param>
//        /// <param name="amount">Amount of Question</param>
//        /// <Author>Belyi Egor</Author>
//        public static Question[] GenerateIsIt(QuestionEntity[] mas, int amount)
//        {
//            const String APPE = "Являются ли ";
//            String ANSW1 = "1)Являются.";
//            String ANSW2 = "2)Не являются";
//            String[] ANSW = { ANSW1, ANSW2 };
//            Question[] Quest = new Question[amount];
//            String ENDP = "";
//            List<int> IntQuest = new List<int>();
//            for (int i = 0; i < mas.Length - 1; i++)
//            {
//                if (mas[i].Type == 2)
//                {
//                    IntQuest.Add(i);
//                }
//                else
//                {
//                    if (mas[i].Type == 0)
//                    {
//                        ENDP = mas[i].Text;
//                    }
//                }
//            }
//            int l = 0;
//            String MyHash;
//            while (amount-- > 0)
//            {

//                Random m = new Random();
//                int IA = m.Next(IntQuest.Count);
//                int AA = IntQuest[IA];
//                IntQuest.RemoveAt(IA);
//                MyHash = $"DBDATA-G2-{AA}-{(mas[AA].Flag ? 0 : 1)}";
//                Quest[l] = new Question($"{APPE} {mas[AA].Text.ToLower()} {ENDP}?", ANSW, (mas[AA].Flag ? 0 : 1), MyHash);


//            }

//            return Quest;
//        }
//    }
//}
