using GeneratorAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly AnswerRepository _answerRepository;

        public AnswerController(AnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }


        [HttpGet("/api/Answer/Get-All")]
        public async Task<IActionResult> Get()
        {
            var answer =  _answerRepository.Get();
            return Ok(answer);
        }

        [HttpGet("/api/Answer/Get-Id-By-Theme")]
        public async Task<IActionResult> GetIdByIndex(int id)
        {
            var c = await _answerRepository.GetIdByTheme(id);
            if(c is null)
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
