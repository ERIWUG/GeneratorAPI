
using GeneratorAPI.Configurations;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace GeneratorAPI.Models
{
    public class AppDbContext : DbContext
    {
        
        


        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {

        }

        public AppDbContext()
        {
            Database.EnsureCreated();

           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            // string connection = "Server=DESKTOP-TQLBOGP;Database=applicationdb;user id=Egor;password=123123;Trusted_Connection=True;TrustServerCertificate=True;";
            string connection = "Server=DESKTOP-TQLBOGP;Database=applicationdb;user id=Vitya;password=1234;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connection);
        }


        public DbSet<QuestionEntity> Questions { get; set; }

        public DbSet<ImageEntity> Images { get; set; }

        public DbSet<AnswerEntity> Answers{ get; set;}

        public DbSet<ImToAns> ImagesToAnswers { get; set; }

        public DbSet<QuesToAns> QuestionsToAnswers { get; set; }

        public DbSet<QuesToIm> QuestionsToImages { get; set; }

        public DbSet<ThemeEntity> Themes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
      //      modelBuilder.ApplyConfiguration(new ThemeConfiguration());
        }



    }
}