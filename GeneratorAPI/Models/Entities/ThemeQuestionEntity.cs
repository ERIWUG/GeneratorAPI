namespace GeneratorAPI.Models.Entities
{
    [Serializable]
    public class ThemeQuestionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;


        public List<QuestionEntity> Questions { get; set; } = [];
    }
}
