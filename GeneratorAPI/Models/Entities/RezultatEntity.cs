using GeneratorAPI.Models.TempTable;

namespace GeneratorAPI.Models.Entities
{
    public class RezultatEntity
    {
        public int Id { get; set; }
        public QuestionEntity? Question { get; set; }
        public List<ImageEntity> Images { get; set; } = [];

        public List<AnswerEntity> Answers { get; set; } = [];

        public List<BlockOfAnswers> BlockAnswers { get; set; } = [];

        public int CorrectAnswer = 0;

        public String Seed = string.Empty;  

    }
}
