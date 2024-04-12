using GeneratorAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly AppDbContext _appDbContext = new AppDbContext();
        

        [HttpGet("/api/Ticket/GetLinear")]

        public async Task<IActionResult> GetLinear(int id,AppDbContext db,int amount)
        {
            var c = db.QuestionsToAnswers.Where(c => c.QuestionID == id).ToArray();


            return Ok(Generator.GenerateLinear(c, 5));
            
        }



    }
}
