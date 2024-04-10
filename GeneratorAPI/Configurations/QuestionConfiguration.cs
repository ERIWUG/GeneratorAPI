using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneratorAPI.Configurations
{
    public class QuestionConfiguration:IEntityTypeConfiguration<QuestionEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder
                .HasMany(c=>c.Images)
                .WithMany(c=>c.Questions)
                .UsingEntity<QuesToIm>(
                 j => j
                .HasOne(c => c.Image)
                .WithMany(c => c.QuestionToImage)
                .HasForeignKey(c => c.ImageID),
                j => j
                .HasOne(c => c.Question)
                .WithMany(c => c.QuestionToImage)
                .HasForeignKey(c => c.QuestionID),
                j =>
                {
                    j.HasKey(t => new { t.ImageID, t.QuestionID });
                }
                
                );
        }
    }
}
