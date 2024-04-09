using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace GeneratorAPI.Repositories
{
    public class QuestionDataRepository
    {
        private readonly AppDbContext _dbContext;

            public QuestionDataRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<QuestionDataEntity>> Get()
        {
            return await _dbContext.QuestionDatas.AsNoTracking().ToListAsync();    
        }

        public async Task<List<QuestionDataEntity>> GetWithImages()
        {
            return await _dbContext.QuestionDatas.AsNoTracking().Include(q=>q.Images).ToListAsync();
        }

        public async Task<QuestionDataEntity?> GetById(Guid id)
        {
            return await _dbContext.QuestionDatas.AsNoTracking().FirstOrDefaultAsync(q=>q.Id==id);
        }

        public async Task<List<QuestionDataEntity>> GetByFilter(int type)
        {
            var query = _dbContext.QuestionDatas.AsNoTracking();
            query = query.Where(q => q.Type == type);
            return await query.ToListAsync();
        }

        public async Task Add(string text, int type, bool flag, int theme, decimal probability=decimal.Zero, bool hasImage=false)
        {
            var questionData = new QuestionDataEntity
            {
                Id = Guid.NewGuid(),
                Text = text,
                Type = type,
                Flag = flag,
                Probability = probability,
                HasImage = hasImage,
                Theme = theme,
            };
            await _dbContext.AddAsync(questionData);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<QuestionDataEntity>> GetByPage(int page, int pageSize)
        {
            return await _dbContext.QuestionDatas.AsNoTracking().Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        }


    }
}
