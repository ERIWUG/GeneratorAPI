using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace GeneratorAPI.Configurations
{
    public class QuestionDataConfiguration : IEntityTypeConfiguration<QuestionDataEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionDataEntity> builder)
        {

            builder.HasKey(x => x.Id);
            builder
                .HasMany(x => x.Images)
                .WithMany(c => c.Answers);
        }
    }
}
