
using GeneratorAPI.Configurations;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GeneratorAPI.Models
{
    public class AppDbContext : DbContext
    {



        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {

        }




        public DbSet<QuestionEntity> Questions { get; set; }

        public DbSet<ImageEntity> Images { get; set; }

        public DbSet<AnswerEntity> Answers{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());

        }



    }
}