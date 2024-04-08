using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneratorAPI
{
    public class QuestionDataRepository
    {
        private readonly AppDbContext _appDbContext;

        public QuestionDataRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<List<QuestionDataEntity>> GetAll()
        {
            return await _appDbContext.QuestionDatas
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
