using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace GeneratorAPI.Repositories
{
    public class QuestionRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly AnswerRepository _answerRepository;
       // private readonly ThemeRepository _themeRepository;

        public QuestionRepository(AppDbContext context)
        {
            _dbContext = context;
            _answerRepository = new AnswerRepository(context);
       //     _themeRepository = new ThemeRepository(context);
        }

        public async Task<List<QuestionEntity>> Get()
        {
            return await _dbContext.Questions.AsNoTracking().ToListAsync();
        }

        public async Task<List<QuestionEntity>> GetWithTheme()
        {
            return null;
     //       return await _dbContext.Questions.Include(u => u.ThemeEntity).AsNoTracking().ToListAsync();
        }

        public async Task<List<QuestionEntity>> GetWithImages()
        {
            return await _dbContext.Questions.AsNoTracking().Include(q => q.Images).ToListAsync();
        }

        public async Task<QuestionEntity?> GetById(int id)
        {
            return await _dbContext.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<List<QuestionEntity>> GetByTheme(int theme)
        {
            return await _dbContext.Questions.AsNoTracking().Where(q => q.Theme.Id == theme).ToListAsync();

        }
        public async Task<List<QuestionEntity>> GetByPage(int page, int pageSize)
        {
            return await _dbContext.Questions.Include(u => u.Theme).AsNoTracking().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
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

        public async Task Add(string text, int themeId)
        {
            var questiondata = new QuestionEntity
            {
                Text = text,

            };
    //        ThemeEntity themeEntity = await _themeRepository.GetById(themeId);
    //        themeEntity.Questions.Add(questiondata);
            await _dbContext.AddAsync(questiondata);
    //        _dbContext.Update(themeEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAnswersForQuestion(int questionId, List<int> answersIds)
        {
            QuestionEntity question = await GetById(questionId);
            foreach (int answerId in answersIds)
            {
                AnswerEntity answer = await _answerRepository.GetById(answerId);
                question.Answers.Add(answer);
            }
            _dbContext.Update(question);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Edit(int questionId, string text, List<String>flags)
        {
            bool O = flags.Contains("O");
            bool X2 = flags.Contains("X2");
            bool ALL = flags.Contains("ALL");
            bool YN = flags.Contains("YN");
            QuestionEntity question = await GetById(questionId);
            question.Text = text;
           

            _dbContext.Update(question);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int questionId)
        {
            if (_dbContext.Questions.Where(a => a.Id == questionId).ToList().Count != 0)
                _dbContext.Remove(_dbContext.Questions.Where(a => a.Id == questionId).ToList()[0]);

            _dbContext.SaveChanges();
        }
    }
}
