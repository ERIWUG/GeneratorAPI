using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;

namespace GeneratorAPI
{
    public static partial class Generator
    {
        /// <summary>
        /// Function that generate and print linear question with probability, can generate true and false questions
        /// </summary>
        /// <param name="mas">array of data</param>
        /// <param name="ogr">integer that used in random (less number)</param>
        /// <param name="amount">integer represent amount of generated questions</param>>
        /// <param name="flag">bool is Negative?</param>>
        public static Question[] GenerateLinear(QuestionEntity[] mas, int ogr, int amount, bool flag)
        {
            Question[] questions = new Question[amount];
            Random rand = new Random();
            List<int> intTrueAns = new List<int>();
            List<int> intFalseAns = new List<int>();
            List<int> intQuest = new List<int>();
            List<string> ans = new List<string>();
            int IndAnswer = 0;
            int l = 0;
            string AQQQQ = null;
            string MyHash = "DBNAME-G1-";

            void ParseData(QuestionEntity[] mas)
            {
                int i = -1;
                while (i++ < mas.Length - 1)
                {

                    if (mas[i].Type == 1 && mas[i].Flag == flag)
                    {
                        intQuest.Add(i);
                    }
                    else
                    {
                        if (mas[i].Flag && mas[i].Type == 2)
                        {
                            intTrueAns.Add(i);
                        }
                        if (!mas[i].Flag && mas[i].Type == 2)
                        {
                            intFalseAns.Add(i);
                        }
                    }
                }
            }




            void GenerateQuest(List<int> a, List<int> b, int k)
            {
                ans.Clear();
                int i = a[rand.Next(a.Count)];
                ans.Add($"1){mas[i].Text}");
                MyHash += $"{i}-";
                while (k-- > 0)
                {
                    if (b.Count == 0) break;
                    int IA = rand.Next(b.Count);
                    var AA = mas[b[IA]];
                    if (AA.Probability != 1)
                    {
                        int c = (int)Math.Round(1 / AA.Probability);
                        int rnd = rand.Next(c);
                        if (rnd == 1)
                        {
                            ans.Add(AA.Text);
                            MyHash += $"{b[IA]}-";
                            b.RemoveAt(IA);
                        }
                        else k++;
                    }
                    else
                    {
                        ans.Add(AA.Text);
                    MyHash += $"{b[IA]}-";
                        b.RemoveAt(IA);
                    }
                    
                }
                questions[l] = new Question(AQQQQ, ans.ToArray(), IndAnswer, MyHash + "0");
                l++;
            }

            ParseData(mas);

            while (amount-- > 0)
            {
                MyHash = "DBNAME-G1-";
                List<int> mT = intTrueAns.Slice(0, intTrueAns.Count);
                List<int> mF = intFalseAns.Slice(0, intFalseAns.Count);
                int k = rand.Next(2, ogr);
                int IQ = rand.Next(intQuest.Count);
                var AQ = mas[intQuest[IQ]];
                MyHash += $"{IQ}-{k+1}-";
                AQQQQ = AQ.Text;
                //Console.WriteLine($"{AQ.text}");
                if (!AQ.Flag)
                {
                    GenerateQuest(mF, mT, k);
                }
                else
                {
                    GenerateQuest(mT, mF, k);
                }

            }
            return questions;

        }
    }
}
