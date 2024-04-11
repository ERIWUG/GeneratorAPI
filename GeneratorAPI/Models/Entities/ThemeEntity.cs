namespace GeneratorAPI.Models.Entities
{
    public class ThemeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AnswerEntity> Answers { get; set; } = [];
    }
}
