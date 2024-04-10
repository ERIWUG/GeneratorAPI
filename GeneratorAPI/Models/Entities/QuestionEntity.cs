using GeneratorAPI.Models.TempTable;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneratorAPI.Models.Entities
{
    [Serializable]
    public class QuestionEntity
    {
        public int Id { get; set; }
        public string Text { get; set; } = String.Empty;    

        public List<ImageEntity> Images { get; set; } = [];
        public List<AnswerEntity> Answers { get; set; } = [];


        public List<QuesToIm> QuestionToImage { get; set; } = [];
        public List<QuesToAns> QuestionToAnswer { get; set; } = [];

    }
}
