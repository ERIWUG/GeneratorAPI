using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace GeneratorAPI.Repositories
{
    public class QuestionRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly AnswerRepository _answerRepository;
        private readonly IdSetRepository idSetRepository;

        public QuestionRepository(AppDbContext context)
        {
            _dbContext = context;
            _answerRepository = new AnswerRepository(context);
            idSetRepository = new IdSetRepository(context);
        }

        public async Task<List<QuestionEntity>> Get()
        {
            return await _dbContext.Questions.AsNoTracking().ToListAsync();
        }

        public async Task<List<QuestionEntity>> GetWithTheme()
        {
                   return await _dbContext.Questions.Include(u => u.IdSet).AsNoTracking().ToListAsync();
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
            return await _dbContext.Questions.AsNoTracking().Where(q => q.IdSet.Id == theme).ToListAsync();

        }
        public async Task<List<QuestionEntity>> GetByPage(int page, int pageSize)
        {
            return await _dbContext.Questions.Include(u => u.IdSet).AsNoTracking().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
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

        public async Task Add(string text, int themeId, bool O, bool YN)
        {
            var questiondata = new QuestionEntity
            {
                Text = text,
                IsNegative = O,
                IsItQuestion=YN
            
            };
            IdSetEntity idSetEntity=await idSetRepository.GetById(themeId);
            idSetEntity.Questions.Add(questiondata);
            await _dbContext.AddAsync(questiondata);
                    _dbContext.Update(idSetEntity);
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

        public async Task Edit(int questionId, string text, List<String> flags)
        {
            bool O = false, YN = false;
            if (flags.Count == 2)
            {
                O = flags[0].Contains("O") || flags[1].Contains("O");
                YN = flags[0].Contains("YN") || flags[1].Contains("YN");
            }
            else if (flags.Count == 1)
            {
                O = flags[0].Contains("O");
                YN = flags[0].Contains("YN");
            }
            QuestionEntity question = await GetById(questionId);
            question.Text = text;
            question.IsNegative = O;
            question.IsItQuestion = YN;
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