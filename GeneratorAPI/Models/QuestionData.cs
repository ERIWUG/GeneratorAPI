using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneratorAPI.Models
{
    [Serializable]
    public class QuestionData
    {
        public string text { get; set; }
        public int type { get; set; }
        public bool flag { get; set; }
        public int id { get; set; }
        public QuestionData(string text, int type, bool flag)
        {
            this.text = text;
            this.type = type;
            this.flag = flag;
        }
        public QuestionData() { }
    }
}
