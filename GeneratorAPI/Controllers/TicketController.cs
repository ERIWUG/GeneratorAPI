using GeneratorAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private AppDbContext db = new AppDbContext();

        [HttpGet("/api/Ticket/GetLinear/{amount}")]

        public async Task<IActionResult> GetLinear(int id)
        {
            
            
            return Ok(Generator.GenerateLinear(db.TempQuestAns.Where(c=>c.QuestionID==id).ToArray(),5));
            
        }


    }
}
