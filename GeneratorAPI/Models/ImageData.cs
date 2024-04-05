namespace GeneratorAPI.Models
{
    public class ImageData
    {
        public string Href { get; set; }
        public string DbName { get; set; }
        public string Answers { get; set; }

        public int Id { get; set; }

        public ImageData(string Href, string DbName,string Answers)
        {
            this.Href = Href;
            this.Answers = Answers;
            this.DbName = DbName;
        }

    }
}
