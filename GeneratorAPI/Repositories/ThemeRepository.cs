using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneratorAPI.Repositories
{
    public class ThemeRepository
    {
        private readonly AppDbContext _dbContext;

        public ThemeRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<ThemeEntity>> Get()
        {
            return await _dbContext.Themes.AsNoTracking().ToListAsync();
        }

    }
}
