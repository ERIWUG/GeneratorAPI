using GeneratorAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly ThemeRepository _themeRepository;

        public ThemeController(ThemeRepository themeRepository)
        {
            _themeRepository = themeRepository;
        }


        [HttpGet("/api/Theme/get-Theme")]
        public async Task<IActionResult> Get()
        {
            var mass = await _themeRepository.Get();

            return Ok(mass);
        }



    }
}