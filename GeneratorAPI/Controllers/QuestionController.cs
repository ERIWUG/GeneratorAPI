using GeneratorAPI.Models;
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
        private readonly QuestionDataRepository _questionDataRepository;

        public QuestionController(QuestionDataRepository questionDataRepository)
        {
            _questionDataRepository = questionDataRepository;
        }



        [HttpGet("/api/Question/get-Questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var mass = await _questionDataRepository.Get();

            return Ok(mass);
        }

        [HttpPost("/api/Question/Add-Question")]
        public async Task AddQuestion(string text, int type, bool flag,int theme, decimal probability=decimal.Zero,bool hasImage = false)
        {
            await _questionDataRepository.Add(text,type,flag,theme,probability,hasImage);
        }


    }
}
