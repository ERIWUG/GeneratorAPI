using GeneratorAPI.Models.Entities;

namespace GeneratorAPI.Models.TempTable
{
    public class QuesToIm
    {

        public int QuestionID { get; set; }
        public QuestionEntity Question { get; set; }



        public int ImageID { get; set; }
        public ImageEntity Image { get; set; }

    }
}
