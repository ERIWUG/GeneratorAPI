namespace GeneratorAPI.Models
{
    public class ImageData
    {
        public string Href { get; set; }
        public int[] Answers { get; set; }

        public ImageData(string Href, int[] Answers)
        {
            this.Href = Href;
            this.Answers = Answers;
        }
    }
}
