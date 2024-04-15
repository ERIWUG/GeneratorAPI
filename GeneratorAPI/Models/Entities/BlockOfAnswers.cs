using Microsoft.Identity.Client;

namespace GeneratorAPI.Models.Entities
{
    public class BlockOfAnswers
    {
        public int Id {  get; set; }

        public int[] Ints { get; set; } = [];

        public RezultatEntity Ticket { get; set; }

    }
}
