namespace GeneratorAPI.Models
{
    public class QuestionDataWithProbability
    {
        public double probability { get; set; }
        public string text { get; set; }
        public int type { get; set; }
        public bool flag { get; set; }
        public int id { get; set; }
        public bool hasImage { get; set; }


        public QuestionDataWithProbability(string text, int type, bool flag,  double probability,bool hasImage = false)
        {
            this.probability = probability;
            this.text = text;
            this.type = type;
            this.flag = flag;
            this.hasImage = hasImage;
        }
    }
}
