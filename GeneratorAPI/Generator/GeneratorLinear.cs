using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using GeneratorAPI.Models;

namespace GeneratorAPI
{
    public static partial class Generator
    {
        /// <summary>
        /// Function that generate and print linear question, can generate true and false questions
        /// </summary>
        /// <param name="mas">array of data</param>
        /// <param name="ogr">integer that used in random (less number)</param>
        /// <param name="amount">integer represent amount of generated questions</param>>
        /// <param name="mark">bool is Negative?</param>>
        /// <Author>Belyi Egor</Author>
        public static Question[] GenerateLinear(QuestionData[] mas, int ogr, int amount, bool mark)
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
            void ParseData(QuestionData[] mas)
            {
                int i = -1;
                while (i++ < mas.Length - 1)
                {

                    if (mas[i].type == 1 && mas[i].flag == mark)
                    {
                        intQuest.Add(i);
                    }
                    else
                    {
                        if (mas[i].flag && mas[i].type == 2)
                        {
                            intTrueAns.Add(i);
                        }
                        if (!mas[i].flag && mas[i].type == 2)
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
                ans.Add(mas[i].text);
                //Console.WriteLine($"1){mas[a[rand.Next(a.Count)]].text}");
                MyHash += $"{i}-";
                while (k-- > 0)
                {
                    int IA = rand.Next(b.Count);
                    var AA = mas[b[IA]];
                    
                    ans.Add(AA.text);
                    MyHash += $"{b[IA]}-";
                    b.RemoveAt(IA);
                    //Console.WriteLine($"T){AA.text}");
                }
                questions[l] = new Question(AQQQQ, ans.ToArray(), IndAnswer, MyHash + "0");
                l++;
            }

            ParseData(mas);

            while (amount-- > 0)
            {
                List<int> mT = intTrueAns.Slice(0, intTrueAns.Count);
                List<int> mF = intFalseAns.Slice(0, intFalseAns.Count);
                int k = rand.Next(2, ogr);
                MyHash = "DBNAME-G1-";

                int IQ = rand.Next(intQuest.Count);
                var AQ = mas[intQuest[IQ]];
                MyHash += $"{IQ}-{k+1}-";
                //intQuest.RemoveAt(IQ);
                AQQQQ = AQ.text;
                //Console.WriteLine($"{AQ.text}");
                if (!AQ.flag)
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




        /// <summary>
        /// Function that generate and print linear question with probability, can generate true and false questions
        /// </summary>
        /// <param name="mas">array of data</param>
        /// <param name="ogr">integer that used in random (less number)</param>
        /// <param name="amount">integer represent amount of generated questions</param>>
        /// <param name="flag">bool is Negative?</param>>
        public static Question[] GenerateLinear(QuestionDataWithProbability[] mas, int ogr, int amount, bool flag)
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

            void ParseData(QuestionDataWithProbability[] mas)
            {
                int i = -1;
                while (i++ < mas.Length - 1)
                {

                    if (mas[i].type == 1 && mas[i].flag == flag)
                    {
                        intQuest.Add(i);
                    }
                    else
                    {
                        if (mas[i].flag && mas[i].type == 2)
                        {
                            intTrueAns.Add(i);
                        }
                        if (!mas[i].flag && mas[i].type == 2)
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
                ans.Add($"1){mas[i].text}");
                MyHash += $"{i}-";
                while (k-- > 0)
                {
                    if (b.Count == 0) break;
                    int IA = rand.Next(b.Count);
                    var AA = mas[b[IA]];
                    if (AA.probability != 1)
                    {
                        int c = (int)Math.Round(1 / AA.probability);
                        int rnd = rand.Next(c);
                        if (rnd == 1)
                        {
                            ans.Add(AA.text);
                            MyHash += $"{b[IA]}-";
                            b.RemoveAt(IA);
                        }
                        else k++;
                    }
                    else
                    {
                        ans.Add(AA.text);
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
                AQQQQ = AQ.text;
                //Console.WriteLine($"{AQ.text}");
                if (!AQ.flag)
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
