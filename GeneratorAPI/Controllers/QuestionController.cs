using GeneratorAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace GeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionDataRepository _questionDataRepository;

        public QuestionController(QuestionDataRepository questionDataRepository)
        {
            _questionDataRepository = questionDataRepository;
        }



        [HttpGet("/api/Question/get-Questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var mass = await _questionDataRepository.GetAll();

            return Ok(mass);
        }


    }
}
