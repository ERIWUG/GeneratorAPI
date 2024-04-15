using GeneratorAPI.Models.Entities;

namespace GeneratorAPI.Models.TempTable
{
    public class QuesToAns
    {
        public int QuestionID { get; set; }
        public QuestionEntity Question { get; set; }



        public int AnswerID { get; set; }
        public AnswerEntity Answer { get; set; }

        public int ThemeAnswer { get; set; }

    }
}
