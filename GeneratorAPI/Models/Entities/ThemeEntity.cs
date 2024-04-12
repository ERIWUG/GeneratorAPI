namespace GeneratorAPI.Models.Entities
{
    [Serializable]
    public class ThemeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AnswerEntity> Answers { get; set; } = [];

        public List<QuestionEntity> Questions { get; set; }
    }
}
