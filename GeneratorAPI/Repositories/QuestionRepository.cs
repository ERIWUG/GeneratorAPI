using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace GeneratorAPI.Repositories
{
    public class QuestionRepository
    {
        private readonly AppDbContext _dbContext;

            public QuestionRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<QuestionEntity>> Get()
        {
            return await _dbContext.Questions.AsNoTracking().ToListAsync();    
        }

        public async Task<List<QuestionEntity>> GetWithImages()
        {
            return await _dbContext.Questions.AsNoTracking().Include(q=>q.Images).ToListAsync();
        }

        public async Task<QuestionEntity?> GetById(int id)
        {
            return await _dbContext.Questions.AsNoTracking().FirstOrDefaultAsync(q=>q.Id==id);
        }

        

       



       

        public async Task<List<QuestionEntity>> GetByPage(int page, int pageSize)
        {
            return await _dbContext.Questions.AsNoTracking().Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        }


    }
}
