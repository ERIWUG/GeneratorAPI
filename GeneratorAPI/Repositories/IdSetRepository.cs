using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneratorAPI.Repositories
{
    public class IdSetRepository
    {
        private readonly AppDbContext _dbContext;
        public IdSetRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IdSetEntity?> GetById(int id)
        {
            return await _dbContext.IdSet.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
        }

    }
}
