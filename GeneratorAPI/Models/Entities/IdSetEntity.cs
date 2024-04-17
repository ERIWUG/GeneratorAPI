namespace GeneratorAPI.Models.Entities
{
    [Serializable]
    public class IdSetEntity
    {
        public int Id { get; set; }


        public List<QuestionEntity> Questions { get; set; } = [];
        public List<AnswerEntity> Answers { get; set; } = [];
        public List<ImageEntity> Images { get; set; } = [];

        public IdGroupEntity IdGroup { get; set; }
    }
}
