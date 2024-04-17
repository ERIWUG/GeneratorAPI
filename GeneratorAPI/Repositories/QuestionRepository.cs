using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace GeneratorAPI.Repositories
{
    public class QuestionRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly AnswerRepository _answerRepository;

        public QuestionRepository(AppDbContext context)
        {
            _dbContext = context;
            _answerRepository = new AnswerRepository(context);
        }

        public async Task<List<QuestionEntity>> Get()
        {
            return await _dbContext.Questions.AsNoTracking().ToListAsync();
        }

        public async Task<List<QuestionEntity>> GetWithImages()
        {
            return await _dbContext.Questions.AsNoTracking().Include(q => q.Images).ToListAsync();
        }

        public async Task<QuestionEntity?> GetById(int id)
        {
            return await _dbContext.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
        }

        //public async Task<List<QuestionEntity>> GetByTheme(int theme)
        //{
        //    return await _dbContext.Questions.AsNoTracking().Where(q => q.IdSet == theme).ToListAsync();
        //}
        public async Task<List<QuestionEntity>> GetByPage(int page, int pageSize)
        {
            return await _dbContext.Questions.AsNoTracking().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task Add(string text)
        {
            var questiondata = new QuestionEntity
            {
                Text = text,

            };
            await _dbContext.AddAsync(questiondata);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAnswersForQuestion(int questionId, List<int> answersIds)
        {
            QuestionEntity question = await GetById(questionId);
            foreach (int answerId in answersIds)
            {
                //AnswerEntity answer = await _answerRepository.GetById(answerId);
                //question.Answers.Add(answer);
            }
            _dbContext.Update(question);
             await _dbContext.SaveChangesAsync();
        }


    }
}


