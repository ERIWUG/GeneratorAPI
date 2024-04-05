namespace GeneratorAPI.Models
{
    public class QuestionDataWithProbability:QuestionData
    {
        public double probability { get; set; }

        public QuestionDataWithProbability(string text, int type, bool flag, double probability) : base(text, type, flag)
        {
            this.probability = probability;
        }
    }
}
