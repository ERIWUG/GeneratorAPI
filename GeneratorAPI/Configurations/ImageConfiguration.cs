using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneratorAPI.Configurations
{
    public class ImageConfiguration:IEntityTypeConfiguration<ImageEntity>
    {
        public void Configure(EntityTypeBuilder<ImageEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder
                .HasMany(c => c.Answers)
                .WithMany(c => c.Images)
                .UsingEntity<ImToAns>(
                j => j
                .HasOne(c => c.Answer)
                .WithMany(c => c.ImagesToAnswer)
                .HasForeignKey(c => c.AnswerID),
                j => j
                .HasOne(c => c.Image)
                .WithMany(c => c.ImagesToAnswers)
                .HasForeignKey(c => c.ImageID),
                j =>
                {
                    j.HasKey(t => new { t.AnswerID, t.ImageID });
                }
                );
            builder
                    .HasMany(c => c.Rezultat)
                    .WithMany(c => c.Images);

        }
    }
}
