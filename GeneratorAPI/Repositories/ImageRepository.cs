using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GeneratorAPI.Repositories
{
    public class ImageRepository
    {
        private readonly AppDbContext _dbContext;

        public ImageRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<ImageEntity>> Get()
        {
            return await _dbContext.Images.AsNoTracking().ToListAsync();
        }


       
        public async Task Add(string href)
        {
            var imageData = new ImageEntity
            {
              Href = href,

            };
            await _dbContext.AddAsync(imageData);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<ImageEntity?> GetById(int id)
        {
            return await _dbContext.Images.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}

