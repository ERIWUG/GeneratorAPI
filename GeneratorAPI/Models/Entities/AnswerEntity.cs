using GeneratorAPI.Models.TempTable;

namespace GeneratorAPI.Models.Entities
{
    public class AnswerEntity
    {
        public int Id { get; set; }
        public string Text { get; set; } = String.Empty;

        public decimal Par {  get; set; } = decimal.Zero;

        public int Theme { get; set; } = 0;

        public List<QuestionEntity> Questions { get; set; } = [];
        public List<ImageEntity> Images { get; set; } = [];

        public List<QuesToAns> QuestionToAnswer { get; set; } = [];
        public List<ImToAns> ImagesToAnswer { get; set; } = [];

        public ThemeEntity ThemeEntity { get; set; }

    }
}
