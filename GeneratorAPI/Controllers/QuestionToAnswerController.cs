using GeneratorAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionToAnswerController : ControllerBase
    {
        private readonly QuesToAnsRepository _quesToAnsRepository;

        public QuestionToAnswerController(QuesToAnsRepository quesToAnsRepository)
        {
            _quesToAnsRepository = quesToAnsRepository;
        }

        [HttpGet("/api/QuesToAnswer/getAnswerIdFromQuestionId")]
        public async Task<IActionResult> GetAnswerIdFromQuestionId(int Id)
        {
            var c = _quesToAnsRepository.GetAnswerFromQuestionId(Id).Result;
            if (c is null)
            {
                return NoContent();
            }
            else
            {
                return Ok(c);
            }
        }
        [HttpGet("/api/QuesToAnswer/ge-tQuestionAnswer-Id")]
        public async Task<IActionResult> GetById(int Id)
        {
            var c = await _quesToAnsRepository.GetById(Id);
            if (c is null)
            {
                return NoContent();
            }
            else
            {
                return Ok(c);
            }
        }

    }
}
