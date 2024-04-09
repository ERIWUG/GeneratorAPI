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
        /// Generate question with some params
        /// </summary>
        /// <param name="mas">DataFields</param>
        /// <param name="ogr">Maximum amount of answer in one question</param>
        /// <param name="amount">Amount of generated question</param>
        /// <returns></returns>
        public static Question[] GenerateParam(QuestionDataEntity[] mas,int ogr,int amount)
        {
            List<int> QuestionIndex = new List<int>();
            List<int> AnswerIndex = new List<int>();



            void ParseData()
            {
                int i = -1;
                while (i++ < mas.Length - 1)
                {
                    if (mas[i].Type == 1)
                    {
                        QuestionIndex.Add(i);
                    }
                    else
                    {
                        AnswerIndex.Add(i);
                    }   
                }
            }

            Question GenerateAnswers(string HashString,string QuestionText)
            {
                List<int> NowAnswerIndex = AnswerIndex.Slice(0, AnswerIndex.Count);
                List<string> AnswersInQuestion = new List<string>();
                double paramValue = 0;
                int IndexAnswer = 0;
                Random m = new Random();
                
                IndexAnswer = m.Next(0, NowAnswerIndex.Count);
                HashString += $"{IndexAnswer};{mas[NowAnswerIndex[IndexAnswer]].Probability}-";
                AnswersInQuestion.Add($"{mas[NowAnswerIndex[IndexAnswer]].Text} - {mas[NowAnswerIndex[IndexAnswer]].Probability}");
                NowAnswerIndex.RemoveAt(IndexAnswer);
                int AmountQuestionWithOgr = m.Next(2, ogr);


                while (AmountQuestionWithOgr-- > 0)
                {
                    IndexAnswer = m.Next(0, NowAnswerIndex.Count);
                    paramValue = Convert.ToDouble(mas[NowAnswerIndex[IndexAnswer]].Probability);
                    paramValue += Math.Round((m.NextDouble() + 0.1) * paramValue * 0.5,1,MidpointRounding.ToEven);
                    paramValue = Math.Round(paramValue, 1);
                    HashString += $"{IndexAnswer};{paramValue}-";

                    AnswersInQuestion.Add($"{mas[NowAnswerIndex[IndexAnswer]].Text} - {paramValue}");

                    NowAnswerIndex.RemoveAt(IndexAnswer);
                }
                HashString += "0";
                return new Question(QuestionText, AnswersInQuestion.ToArray(), 0, HashString);
            }

            ParseData();
            Question[] q = new Question[amount];
            string QuestionText = "";
            string HashString = "";
            Random RandomPar = new Random();
            int GeneretedQuestionIndex = 0;
            while (amount-- > 0)
            {
                HashString = "DBNAME-GP-";
                QuestionText = "";
                int DeletedIndex = RandomPar.Next(QuestionIndex.Count);
                QuestionText = mas[QuestionIndex[DeletedIndex]].Text;
                QuestionIndex.RemoveAt(DeletedIndex);
                HashString += $"{DeletedIndex}-";
                q[GeneretedQuestionIndex] = GenerateAnswers(HashString,QuestionText);
                GeneretedQuestionIndex++;
            }

            return q;
        }







    }
}
