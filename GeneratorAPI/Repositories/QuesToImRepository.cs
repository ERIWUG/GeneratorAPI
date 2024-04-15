using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Models.TempTable;

namespace GeneratorAPI.Repositories
{
    public class QuesToImRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ImageRepository _imageRepository;

        public QuesToImRepository(AppDbContext context)
        {
            _dbContext = context;
            _imageRepository = new ImageRepository(context);
        }

        public async Task AddAnswersForQuestion(int questionId, List<int> imagesIds)
        {
            foreach (var imageId in imagesIds)
            {
                ImageEntity image = await _imageRepository.GetById(imageId);
                QuesToIm quesToIm = new QuesToIm
                {
                    QuestionID = questionId,
                    ImageID= imageId,    
                };
                await _dbContext.AddAsync(quesToIm);
            }

            await _dbContext.SaveChangesAsync();
        }



        public async Task<int[]> GetImageFromQuestionId(int id)
        {
            var c = _dbContext.QuestionsToImages.Where(c => c.QuestionID == id).Select(c => c.ImageID).ToArray();
            return c;
        }
    }
}
