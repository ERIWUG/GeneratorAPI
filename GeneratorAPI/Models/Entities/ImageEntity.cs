using GeneratorAPI.Models.TempTable;

namespace GeneratorAPI.Models.Entities
{
    public class ImageEntity
    {
        public string Href { get; set; } = string.Empty;

        public List<QuestionEntity> Questions { get; set; } = [];
        public List<AnswerEntity> Answers { get; set; } = [];

        public int Id { get; set; }

        public List<QuesToIm> QuestionToImage { get; set; } = [];
        public List<ImToAns> ImagesToAnswers { get; set; } = [];


        public List<RezultatEntity> Rezultat { get; set; } = [];
    }
}
