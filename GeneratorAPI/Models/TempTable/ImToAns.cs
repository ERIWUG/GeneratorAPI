using GeneratorAPI.Models.Entities;

namespace GeneratorAPI.Models.TempTable
{
    public class ImToAns
    {
        public int ImageID { get; set; }
        public ImageEntity Image { get; set; }

        public int AnswerID { get; set; }
        public AnswerEntity? Answer { get; set; }

        public int ThemeAnswer { get; set; }
    }
}
