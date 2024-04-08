namespace GeneratorAPI.Models.Entities
{
    public class ImageDataEntity
    {
        public string Href { get; set; } = string.Empty;

        public List<QuestionDataEntity> Answers { get; set; } = [];

        public Guid Id { get; set; }



    }
}
