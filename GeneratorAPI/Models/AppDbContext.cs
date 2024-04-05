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




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-TQLBOGP;Database=applicationdb;Trusted_Connection=True;TrustServerCertificate=True; ");
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionData>().Property(x => x.id).ValueGeneratedOnAdd();
            modelBuilder.Entity<QuestionData>().Property(x => x.type).HasColumnName("Type");
            modelBuilder.Entity<QuestionData>().Property(x => x.text).HasColumnName("Text");
            modelBuilder.Entity<QuestionData>().Property(x => x.flag).HasColumnName("Flag");


            
            
        }



    }
}