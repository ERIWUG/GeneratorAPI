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
        private readonly QuestionRepository _questionRepository;

        public QuestionController(QuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }



        //[HttpGet("/api/Question/get-Questions")]
        //public async Task<IActionResult> GetQuestions()
        //{
        //    var mass = await _questionRepository.Get();

        //    return Ok(mass);
        //}



        //[HttpPost("/api/Question/AddQuestion")]
        //public async Task AddQuestion(string text)
        //{
        //    await _questionRepository.Add(text);
        //} 
    }
}
