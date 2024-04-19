using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Build.Framework;

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


       
        public async Task Add(string href, int themeId)
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

        public async Task Delete(int imageId)
        {
            if (_dbContext.Images.Where(a => a.Id == imageId).ToList().Count != 0)
                _dbContext.Remove(_dbContext.Images.Where(a => a.Id == imageId).ToList()[0]);

            _dbContext.SaveChanges();
        }
    }
}

