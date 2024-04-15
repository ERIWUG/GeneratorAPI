using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace GeneratorAPI.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<AnswerEntity>

    {
        public void Configure(EntityTypeBuilder<AnswerEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder
                .HasMany(c => c.Questions)
                .WithMany(c => c.Answers)
                .UsingEntity<QuesToAns>(
                j => j
                .HasOne(c => c.Question)
                .WithMany(c => c.QuestionToAnswer)
                .HasForeignKey(c => c.QuestionID),
                j => j
                .HasOne(c => c.Answer)
                .WithMany(c => c.QuestionToAnswer)
                .HasForeignKey(c => c.AnswerID),
                j =>
                {
                    j.HasKey(t => new { t.AnswerID, t.QuestionID });
                }
                );

            builder
                   .HasMany(c => c.Rezultat)
                   .WithMany(c => c.Answers);

        }
    }
}
