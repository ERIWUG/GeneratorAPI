using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Repositories;
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
        private readonly QuestionRepository _questionDataRepository;

        public QuestionController(QuestionRepository questionDataRepository)
        {
            _questionDataRepository = questionDataRepository;
        }



        [HttpGet("/api/Question/get-Questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var mass = await _questionDataRepository.Get();

            return Ok(mass);
        }

       

        

    }
}
