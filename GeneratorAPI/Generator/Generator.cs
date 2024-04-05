
using GeneratorAPI.Models;
using System.Net.WebSockets;

namespace GeneratorAPI
{
    /// <summary>
    /// Class that generate and print questions
    /// </summary>
    /// <Author>Belyi Egor</Author>
    public static partial class Generator
    {
        /// <summary>
        /// Метод для перемешки списка ответов
        /// </summary>
        /// <param name="originalList"></param>
        /// <Author>Veremeychik Alex</Author>
        public static int Shuffling(List<string> originalList, int index)
        {

            Random random = new Random();
            for (int i = originalList.Count - 1; i >= 1; i--)
            {

                int j = random.Next(i + 1);
                // Обменять значения originalList[j] и originalList[i]
                string temp = originalList[j];
                originalList[j] = originalList[i];
                originalList[i] = temp;
                if (i == index)
                {

                    index = j;
                }
                else if (j == index)
                {

                    index = i;
                }

            }
            return index;
        }
        /// <summary>
        /// Генерация билета с указанным числом вопросов
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="questAmount">число вопросов в билете</param>
        /// <Author>Nichiporuk Viktor</Author>
        public static Question[] GenerateTicket(QuestionData[] mas, int questAmount)
        {
            Question[] questions1 = new Question[questAmount];
            Random rand = new Random();
            int countOfTypes = 5;//число типов вопросов (их  5 потом будет)
            int[] questions = new int[countOfTypes];
            for (int i = 0; i < questAmount; i++)
            {
                int type;
                do
                {
                    type = rand.Next(countOfTypes);
                }
                while (questions[type] > questAmount / countOfTypes);
                // if (type == prevType) type = rand.Next(countOfTypes);
                switch (type)
                {
                    case 0:
                        questions1[i] = GenerateLinear(mas, 5, 1, true)[0];
                        questions[0]++;
                        break;
                    case 1:
                        questions1[i] = GenerateLinear(mas, 5, 1, false)[0];
                        questions[1]++;
                        break;
                    case 2:
                        questions1[i] = GenerateEnum(mas, 5, 1)[0];
                        questions[2]++;
                        break;
                    case 3:
                        questions1[i] = GenerateIsIt(mas, 1)[0];
                        questions[3]++;
                        break;

                    case 4:
                        questions1[i] = GenerateGroup(mas, 5, 1)[0];
                        questions[4]++;
                        break;
                }

            }
            return questions1;
        }

        /// <summary>
        /// Generate a question from hash function
        /// </summary>
        /// <param name="hash">hash</param>
        /// <Author>Nichiporuk Viktor</Author>
        public static Question degeneration(String hash)
        {
            var mas = new QuestionData[3];
            //локальный массив для отладки метода
    //        var mas = new QuestionData[]{

    //new("элементами дороги",0,true),
    //new("Какие из перечисленных элементов являются элементами дороги?",1,true),
    //new("Какие из перечисленных элементов(при их наличии) являются элементами дороги?",1,true),
    //new("Что является элементом дороги?",1,true),
    //new("Что входит в элементы дороги?",1,true),
    //new("Какие из перечисленных элементов не являются элементами дороги?",1,false),
    //new("Какие из перечисленных элементов(при их наличии) не являются элементами дороги?",1,false),
    //new("Что не является элементом дороги?",1,false),
    //new("Что не входит в элементы дороги?",1,false),
    //new("Разделительные полосы",2,true),
    //new("Разделительные зоны",2,true),
    //new("Трамвайные пути",2,true),
    //new("Островки безопасности",2,true),
    //new("Островки, выделенные только разметкой",2,false),
    //new("Проезжие части",2,true),
    //new("Тротуары",2,true),
    //new("Обочины",2,true),
    //new("Пешеходные дорожки",2,true),
    //new("Велосипедные дорожки",2,true),
    //new("Обособленные велосипедные дорожки",2,false),
    //new("Пешеходные переходы",2,false),
    //new("Велосипедные переезды",2,false),
    //new("Перекрестки",2,false),
    //new("Кюветы", 2, false),
    //new("Обрезы", 2, false),
    //new("Придорожные насаждения", 2, false),
    //new("Кустарник при дороге", 2, false),
    //new("Дорожки для всадников", 2, false),

    //};
            string[] words = hash.Split('-');
            string genType = words[1];
            int questionIndex;
            String question;
            String[] answers;
            int rightAnswer;
            if (genType == "G2")
            {
                int wordNum = int.Parse(words[2]);
                string word = mas[wordNum].text; //надо будет заменить на чтение из БД
                question = "Являются ли " + word.ToLower() + " " + mas[0].text;
                answers = new string[2];
                answers[0] = "Являются.";
                answers[1] = "Не являются";
                rightAnswer = int.Parse(words[3]);
            }
            else
            {
                questionIndex = int.Parse(words[2]);
                question = mas[questionIndex].text; //надо будет заменить на чтение из БД
                int amountOfAnswers = words.Length - 5;
                answers = new String[amountOfAnswers];

                for (int i = 0; i < amountOfAnswers; i++)
                {
                    int answersIndex = 0;
                    string value = words[i + 4];
                    if (words[i + 4] == $"A1") answers[i] = (i + 1) + ") " + "Все перечисленное";
                    else if (words[i + 4] == $"A2") answers[i] = (i + 1) + ") " + "Ничего из перечисленного";
                    else if (genType == "GX")
                    {
                        string[] ansNumbers = value.Split(',');
                        int[] ans = new int[ansNumbers.Length];
                        for (int j = 0; j < ans.Length; j++)
                            ans[j] = int.Parse(ansNumbers[j]);
                        string variant = "";
                        for (int j = 0; j < ans.Length; j++)
                            variant += (mas[ans[j]].text + ", ");
                        answers[i] = (i + 1) + ") " + variant;
                    }
                    else
                    {
                        answersIndex = int.Parse(words[i + 4]);
                        answers[i] = (i + 1) + ") " + mas[answersIndex].text;//надо будет заменить на чтение из БД
                    }
                }
                rightAnswer = int.Parse(words[4 + amountOfAnswers]);
            }
            Question q = new Question(question, answers, rightAnswer, hash);
            return q;
        }


    }
}
