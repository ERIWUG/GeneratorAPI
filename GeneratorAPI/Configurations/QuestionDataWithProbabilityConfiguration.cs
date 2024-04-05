using GeneratorAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneratorAPI.Configurations
{
    public class QuestionDataWithProbabilityConfiguration : IEntityTypeConfiguration<QuestionDataWithProbability>
    {
        public void Configure(EntityTypeBuilder<QuestionDataWithProbability> builder)
        {
            builder.Property(x => x.id).ValueGeneratedOnAdd();
        }
    }
}
