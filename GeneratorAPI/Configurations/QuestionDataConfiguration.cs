using GeneratorAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace GeneratorAPI.Configurations
{
    public class QuestionDataConfiguration : IEntityTypeConfiguration<QuestionData>
    {
        public void Configure(EntityTypeBuilder<QuestionData> builder)
        {
            
            builder.Property(x => x.id).ValueGeneratedOnAdd();
            builder.Property(x => x.type).HasColumnName("Type");
            builder.Property(x => x.text).HasColumnName("Text");
            builder.Property(x => x.flag).HasColumnName("Flag");
        }
    }
}
