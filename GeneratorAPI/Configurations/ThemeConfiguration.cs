using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneratorAPI.Configurations
{
    public class ThemeConfiguration : IEntityTypeConfiguration<ThemeEntity>
    {
        public void Configure(EntityTypeBuilder<ThemeEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
        }
    }
}
