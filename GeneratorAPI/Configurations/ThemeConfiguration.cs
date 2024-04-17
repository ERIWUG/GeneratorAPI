using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneratorAPI.Configurations
{
    public class ThemeConfiguration : IEntityTypeConfiguration<IdSetEntity>
    {
        public void Configure(EntityTypeBuilder<IdSetEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();


        }
    }
}
