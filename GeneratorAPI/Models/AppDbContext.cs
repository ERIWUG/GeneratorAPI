using GeneratorAPI.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GeneratorAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            Database.EnsureCreated();
        }




        public DbSet<QuestionData> QuestionDatas { get; set; }
        public DbSet<QuestionDataWithProbability> QuestionDataWithProbabilities{ get;set;}

        public DbSet<ImageData> imageDatas { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-TQLBOGP;Database=applicationdb;Trusted_Connection=True;TrustServerCertificate=True; ");
            //optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new QuestionDataConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionDataWithProbabilityConfiguration());
            modelBuilder.ApplyConfiguration(new ImageDataConfiguration());
        }



    }
}