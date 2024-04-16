using GeneratorAPI.Models.TempTable;

namespace GeneratorAPI.Models.Entities
{
    [Serializable]
    public class RezultatEntity
    {
        public int Id { get; set; }
        public int CorrectAnswer { get; set; } = 0;

        public string Seed { get; set; } = string.Empty;
        public QuestionEntity? Question { get; set; }
        public List<ImageEntity> Images { get; set; } = [];

        public List<AnswerEntity> Answers { get; set; } = [];

        public List<BlockOfAnswers> BlockAnswers { get; set; } = [];

        

    }
}
