using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneratorAPI.Models.Entities
{
    [Serializable]
    public class QuestionDataEntity
    {
        public string text { get; set; } = string.Empty;
        public int type { get; set; } = 0;
        public bool flag { get; set; } = false;
        public Guid id { get; set; }
        public decimal probability { get; set; } = decimal.Zero;
        public bool hasImage { get; set; } = false;

        public List<ImageDataEntity> images { get; set; } = [];

    }
}
