using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace GeneratorAPI.Models.Entities
{
    public class IdGroupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public List<IdSetEntity>? IdSets { get; set; } = [];


    }
}
