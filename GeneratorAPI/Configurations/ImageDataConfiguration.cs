using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneratorAPI.Configurations
{
    public class ImageDataConfiguration : IEntityTypeConfiguration<ImageDataEntity>
    {
        public void Configure(EntityTypeBuilder<ImageDataEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .HasMany(x => x.Answers)//?????
                .WithMany(c => c.Images);
        }
    }
}
