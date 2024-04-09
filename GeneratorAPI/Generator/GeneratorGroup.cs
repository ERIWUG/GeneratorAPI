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
        /// Function that generate random combination of questions with new parameter
        /// <param name="mas"></param>
        /// <param name="ogr"></param>
        /// <param name="amount"></param>
        /// <Author>Alex Veremeychik</Author>>
        public static Question[] GenerateGroup(QuestionDataEntity[] mas, int ogr, int amount)
        {
            Question[] questions = new Question[amount];
            Random rand = new Random();
            int amQuest = 0;
            List<int> intTrueAns = new List<int>();
            List<int> intFalseAns = new List<int>();
            List<int> intQuest = new List<int>();
            List<int> intAnswer = new List<int>();

            List<string> AllAnsw = new List<string>();
            List<string> CorrectAnswers = new List<string>();
            List<string> GroupOfAnswers = new List<string>();
            List<string> randomElements = new List<string>();
            List<string> AnswersHashList = new List<string>();

            int IndexOfCorrectAnswer = 0;

            int IndAnswer = 0;
            int l = 0;
            string QuestionText = null;
            string MyHash = "DBNAME-GX-";

            void ParseData(QuestionDataEntity[] mas)
            {
                int i = -1;
                while (i++ < mas.Length - 1)
                {

                    if (mas[i].Type == 1)
                    {
                        intQuest.Add(i);
                    }
                    if (mas[i].Type == 2)
                    {
                        intAnswer.Add(i);
                        if (mas[i].Flag)
                        {
                            intTrueAns.Add(i);
                        }
                        else
                        {
                            intFalseAns.Add(i);
                        }
                    }
                }
            }


            string GenerateRandomStrings(List<string> list)
            {
                string TempHash = "";

                // Создание копии списка для извлечения элементов без повторений
                List<string> tempList = new List<string>(list);
                randomElements.Clear();

                // Определение случайного количества элементов для выборки
                int numberOfElements = rand.Next(0, tempList.Count + 1);
                if (numberOfElements == 0)
                {
                    return "Ничего из перечисленного";
                }
                // Выбор случайных элементов без повторений и в исходном порядке
                while (randomElements.Count < numberOfElements)
                {
                    int index = rand.Next(tempList.Count);
                    randomElements.Add(tempList[index]);
                    tempList.RemoveAt(index); // Удаление выбранного элемента, чтобы избежать повторений
                }
                randomElements.Sort((a, b) => list.IndexOf(a).CompareTo(list.IndexOf(b)));
                return String.Join("; ", randomElements);


            }

            string GetHashOfAnswer(List<string> list)
            {
                string TempHash = "";
                if (list.Count != 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (i + 1 < list.Count)
                        {
                            TempHash += $"{mas.Select((obj, i) => new { obj, i }).First(p => p.obj.Text == list[i]).i},";

                        }
                        else
                        {
                            TempHash += $"{mas.Select((obj, i) => new { obj, i }).First(p => p.obj.Text == list[i]).i}";

                        }
                    }
                }
                else
                {
                    TempHash += "A2";
                }
                return TempHash;
            }


            void GenerateAnswers(List<int> full, bool sign, int num)
            {
                int k = num;
                while (k-- > 0)
                {

                    int IA = rand.Next(full.Count);
                    var AA = mas[full[IA]];
                    if (AA.Probability != 1)
                    {
                        int c = (int)Math.Round(1 / AA.Probability);
                        int rnd = rand.Next(c);
                        if (rnd == 1)
                        {
                            full.RemoveAt(IA);
                            //Console.WriteLine($"----{AA.text},  {AA.flag}\n");
                            AllAnsw.Add(AA.Text);
                            if (sign)
                            {
                                if (AA.Flag)
                                {
                                    CorrectAnswers.Add(AA.Text);
                                }
                            }
                            else
                            {
                                if (!AA.Flag)
                                {
                                    CorrectAnswers.Add(AA.Text);
                                }
                            }
                        }
                        else k++;
                    }
                    else
                    {
                        full.RemoveAt(IA);
                        Console.WriteLine($"----{AA.Text},  {AA.Flag}\n");
                        AllAnsw.Add(AA.Text);
                        if (sign)
                        {
                            if (AA.Flag)
                            {
                                CorrectAnswers.Add(AA.Text);
                            }
                        }
                        else
                        {
                            if (!AA.Flag)
                            {
                                CorrectAnswers.Add(AA.Text);
                            }
                        }
                    }


                }



                string CorrectString = String.Join("; ", CorrectAnswers);

                if (CorrectString == null || CorrectAnswers.Count == 0)
                {
                    CorrectString = "Ничего из перечисленного";
                }

                int minvalue = 3;
                int maxvalue = 5;
                if (AllAnsw.Count <= 3)
                {
                    maxvalue = 4;
                }
                int NumberOfAnswers = rand.Next(minvalue, maxvalue);
                MyHash += GetHashOfAnswer(AllAnsw);

                MyHash += "-" + $"{NumberOfAnswers}";

                AnswersHashList.Add(GetHashOfAnswer(CorrectAnswers));

                //MyHash += "-" + String.Join("",AnswersHashList);

                GroupOfAnswers.Add(CorrectString);
                while (GroupOfAnswers.Count < NumberOfAnswers)
                {
                    string randomString = GenerateRandomStrings(AllAnsw);
                    if (!GroupOfAnswers.Contains(randomString))
                    {
                        GroupOfAnswers.Add(randomString);
                        AnswersHashList.Add(GetHashOfAnswer(randomElements));

                    }
                }
                IndexOfCorrectAnswer = 0;
                int n = 0;
                IndexOfCorrectAnswer = Shuffling(GroupOfAnswers, IndexOfCorrectAnswer, AnswersHashList);




                //добавление блока ответов в строку вопроса
                QuestionText += "-" + String.Join("\n-", AllAnsw);

                MyHash += "-" + String.Join("-", AnswersHashList);
                MyHash += $"-{IndexOfCorrectAnswer}";
                questions[l] = new Question(QuestionText, GroupOfAnswers.ToArray(), IndexOfCorrectAnswer, MyHash);
                l++;
                Console.WriteLine();
                Console.WriteLine(QuestionText);
                Console.WriteLine();
                foreach (string str in GroupOfAnswers)
                {
                    n++;
                    Console.WriteLine(Convert.ToString(n) + " " + str);
                }
                Console.WriteLine(MyHash);

                GroupOfAnswers.Clear();
                AnswersHashList.Clear();


            }


            ParseData(mas);

            while (amount-- > 0)
            {
                MyHash = "DBNAME-GX-";
                List<int> Answers = new List<int>(intAnswer);
                int AmountOfAnswersWithQuestion = rand.Next(2, ogr);

                int IQ = rand.Next(intQuest.Count);
                var AQ = mas[intQuest[IQ]];
                AllAnsw.Clear();
                CorrectAnswers.Clear();
                MyHash += $"{IQ}-";
                QuestionText = AQ.Text + "\n";

                //Console.WriteLine($"{AQ.text}\n");
                if (!AQ.Flag)
                {
                    GenerateAnswers(Answers, false, AmountOfAnswersWithQuestion);
                }
                else
                {
                    GenerateAnswers(Answers, true, AmountOfAnswersWithQuestion);
                }

            }

            return questions;

        }
    }
}
