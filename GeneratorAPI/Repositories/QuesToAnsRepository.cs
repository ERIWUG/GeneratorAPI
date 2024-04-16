using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;
using Microsoft.EntityFrameworkCore;

namespace GeneratorAPI.Repositories
{
    public class QuesToAnsRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly AnswerRepository _answerRepository;

        public QuesToAnsRepository(AppDbContext context)
        {
            _dbContext = context;
            _answerRepository = new AnswerRepository(context);
        }

        public async Task AddAnswersForQuestion(int questionId, List<int> answersIds)
        {
            foreach (var answerId in answersIds) {
                AnswerEntity answer = await _answerRepository.GetById(answerId);
                QuesToAns quesToAns = new QuesToAns
                {
                    QuestionID = questionId,
                    AnswerID = answerId,
                    ThemeAnswer = answer.Theme.Id
                };
                  if (_dbContext.QuestionsToAnswers.Where(c => c.QuestionID == questionId).Where(c=>c.AnswerID==answerId).Select(c => c.ThemeAnswer).ToList().Count==0)
                await _dbContext.AddAsync(quesToAns);
            }
           
            await _dbContext.SaveChangesAsync();
        }

        public async Task DelAnswersForQuestion(int questionId, List<int> answersIds)
        {
            foreach (var answerId in answersIds)
            {
                if (_dbContext.QuestionsToAnswers.Where(c => c.QuestionID == questionId).Where(c => c.AnswerID == answerId).Select(c => c.ThemeAnswer).ToList().Count!=0)
                    _dbContext.Remove(_dbContext.QuestionsToAnswers.Where(a => a.QuestionID == questionId).Where(a => a.AnswerID == answerId).ToList()[0]);
                if ( _dbContext.QuestionsToAnswers.Where(a => a.QuestionID == questionId).Where(a => a.AnswerID == answerId).ToList().Count!=0)
                _dbContext.Remove(_dbContext.QuestionsToAnswers.Where(a => a.QuestionID == questionId).Where(a => a.AnswerID == answerId).ToList()[0]);
            }

            _dbContext.SaveChanges();
        }

        public async Task<int[]> GetAnswerFromQuestionId(int id)
        {
            var c= _dbContext.QuestionsToAnswers.Where(c => c.QuestionID == id).Select(c => c.AnswerID).ToArray();
            return c;
        }

        public async Task<QuesToAns[]> GetById(int id)
        {
            var c =  _dbContext.QuestionsToAnswers.Where(c => c.QuestionID == id).ToArray();
            return c;
        }
    }
}
