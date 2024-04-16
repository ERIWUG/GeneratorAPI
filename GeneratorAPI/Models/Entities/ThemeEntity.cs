namespace GeneratorAPI.Models.Entities
{
    public class ThemeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } =String.Empty;
        public List<AnswerEntity> Answers { get; set; } = [];

    }
}
