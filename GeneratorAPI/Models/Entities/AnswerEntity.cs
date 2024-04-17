using GeneratorAPI.Models.TempTable;

namespace GeneratorAPI.Models.Entities
{
    [Serializable]
    public class AnswerEntity
    {
        public int Id { get; set; }
        public string Text { get; set; } = String.Empty;

        public decimal Param {  get; set; } = decimal.Zero;

        public decimal Probability { get; set; } = decimal.Zero;

        public List<QuestionEntity> Questions { get; set; } = [];
        public List<ImageEntity> Images { get; set; } = [];

        public List<QuesToAns> QuestionToAnswer { get; set; } = [];
        public List<ImToAns> ImagesToAnswer { get; set; } = [];


        public bool IsImageRequired { get; set; } = false;

        public IdSetEntity? IdSet { get; set; }

        public List<RezultatEntity> Rezultat { get; set; } = [];

    }
}
