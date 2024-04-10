namespace GeneratorAPI.Models.Entities
{
    public class ThemeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AnswerEntity[] Answers { get; set; }
    }
}
