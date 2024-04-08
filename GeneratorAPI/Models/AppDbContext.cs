using GeneratorAPI.Configurations;
using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneratorAPI.Models
{
    public class AppDbContext : DbContext
    {
       


        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
           
        }

        


        public DbSet<QuestionDataEntity> QuestionDatas { get; set; }

        public DbSet<ImageDataEntity> imageDatas { get; set; }




        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("Server=DESKTOP-TQLBOGP;Database=applicationdb;Trusted_Connection=True;TrustServerCertificate=True; ");
        //    //optionsBuilder.LogTo(Console.WriteLine);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new QuestionDataConfiguration());
            modelBuilder.ApplyConfiguration(new ImageDataConfiguration());
        }



    }
}