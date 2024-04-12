using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;

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
                    ThemeAnswer =answer.ThemeEntity.Id
                };
                await _dbContext.AddAsync(quesToAns);
            }
           
            await _dbContext.SaveChangesAsync();
        }



        public async Task<int[] > GetAnswerFromQuestionId(int id)
        {
            return _dbContext.TempQuestAns.Where(c=>c.QuestionID==id).Select(c=>c.AnswerID).ToArray();
        } 
    }
}
