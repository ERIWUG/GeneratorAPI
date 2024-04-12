using GeneratorAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace GeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly AppDbContext _appDbContext = new AppDbContext();
        

        [HttpGet("/api/Ticket/GetLinear/{amount}")]

        public async Task<IActionResult> GetLinear(int id,AppDbContext db)
        {
            var c = db.QuestionsToAnswers.Where(c => c.QuestionID == id).ToArray();


            return Ok(Generator.GenerateLinear(c, 5));
            
        }

        [HttpGet("/api/Ticket/GetEnum/{amount}")]

        public async Task<IActionResult> GetEnum(int id, AppDbContext db)
        {
            var c = db.QuestionsToAnswers.Where(c => c.QuestionID == id).ToArray();


            return Ok(Generator.GenerateEnum(c, 5));

        }

    }
}
