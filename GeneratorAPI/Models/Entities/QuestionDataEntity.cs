using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneratorAPI.Models.Entities
{
    [Serializable]
    public class QuestionDataEntity
    {
        public string Text { get; set; } = string.Empty;
        public int Type { get; set; } = 0;
        public bool Flag { get; set; } = false;
        public Guid Id { get; set; }
        public decimal Probability { get; set; } = decimal.Zero;
        public bool HasImage { get; set; } = false;
        public int Theme { get; set; } = 0;

        public List<ImageDataEntity> Images { get; set; } = [];

    }
}
