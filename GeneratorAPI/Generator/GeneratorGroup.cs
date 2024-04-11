using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;

namespace GeneratorAPI
{
    public static partial class Generator
    {
        /// <summary>
        /// Method for generating Ticket with one correct and some incorrect answers
        /// </summary>
        /// <param name="mas">Data for generating</param>
        /// <param name="ogr">Max amount of answers in one ticket</param>
        /// <returns>One Ticket with one correct and some Incorrect Question</returns>
        public static Ticket GenerateGroup(QuesToAns[] mas, int ogr)
        {
            List<int> BlockOfAnswers= new List<int>();
            List<int> CorrectAnswer = new List<int>();
            List<List<int>> GroupOfAnswers = new List<List<int>>();
            List<int> randomElements = new List<int>();
            Random random = new Random();


            List<int> GenerateRandomStrings(List<int> list)
            {
                string TempHash = "";

                // Создание копии списка для извлечения элементов без повторений
                List<int> tempList = new List<int>(list);
                randomElements.Clear();

                // Определение случайного количества элементов для выборки
                int numberOfElements = random.Next(0, tempList.Count + 1);
                if (numberOfElements == 0)
                {
                    
                    //return -1;
                }
                // Выбор случайных элементов без повторений и в исходном порядке
                while (randomElements.Count < numberOfElements)
                {
                    int index = random.Next(tempList.Count);
                    randomElements.Add(tempList[index]);
                    tempList.RemoveAt(index); // Удаление выбранного элемента, чтобы избежать повторений
                }
                randomElements.Sort((a, b) => list.IndexOf(a).CompareTo(list.IndexOf(b)));
                return randomElements;
            }



            ParseData(mas);
            

            int AmountOfAnswersWithQuestion = random.Next(2, ogr);

            while (AmountOfAnswersWithQuestion-- > 0)
            {

                int IndexOfAnswer = random.Next(AllAnswersToQuestion.Count);
                

                BlockOfAnswers.Add(AllAnswersToQuestion[IndexOfAnswer]);
                if (CorrectAnswerIndexes.Contains(AllAnswersToQuestion[IndexOfAnswer]))
                {
                    CorrectAnswer.Add(AllAnswersToQuestion[IndexOfAnswer]);
                }
                AllAnswersToQuestion.RemoveAt(IndexOfAnswer);
            }

            if (CorrectAnswer.Count == 0)
            {
                
                //CorrectAnswer.Add();
            }

            int minvalue = 3;
            int maxvalue = 5;
            if (BlockOfAnswers.Count <= 3)
            {
                maxvalue = 4;
            }
            int NumberOfAnswers = random.Next(minvalue, maxvalue);
            GroupOfAnswers.Add(CorrectAnswer);
            while (GroupOfAnswers.Count < NumberOfAnswers)
            {
                List<int> randomAnswer= new List<int>();
                randomAnswer.AddRange(GenerateRandomStrings(BlockOfAnswers));
                if (!GroupOfAnswers.Contains(randomAnswer))
                {
                    GroupOfAnswers.Add(randomAnswer);
                }
            }
            int IndexOfCorrectAnswer = 0;

            return new Ticket(mas[0].QuestionID, GroupOfAnswers, IndexOfCorrectAnswer, " ");
        }
    }
}
