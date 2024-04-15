using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;

namespace GeneratorAPI.Repositories
{
    public class ImToAnsRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly AnswerRepository _answerRepository;

        public ImToAnsRepository(AppDbContext context)
        {
            _dbContext = context;
            _answerRepository = new AnswerRepository(context);
        }

        public async Task AddAnswersForImage(int imageId, List<int> answersIds)
        {
            foreach (var answerId in answersIds)
            {
                //AnswerEntity answer = await _answerRepository.GetById(answerId);
                //ImToAns imToAns = new ImToAns
                //{
                //    ImageID = imageId,
                //    AnswerID = answerId,
                //    ThemeAnswer = answer.ThemeEntity.Id
                //};
                //await _dbContext.AddAsync(imToAns);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<int[]> GetAnswerFromImageId(int id)
        {
            var c = _dbContext.ImagesToAnswers.Where(c => c.ImageID == id).Select(c => c.AnswerID).ToArray();
            return c;
        }
    }
}
