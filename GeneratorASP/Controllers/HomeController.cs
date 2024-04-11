using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Repositories;
using GeneratorASP.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GeneratorASP.Controllers
{
    public class HomeController : Controller

    {
        public readonly AppDbContext db = new AppDbContext();
        private readonly QuestionRepository _questionRepository;
        private readonly QuesToAnsRepository _quesToAnsRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, QuestionRepository questionRepository, QuesToAnsRepository quesToAnsRepository)
        {
            _questionRepository = questionRepository;
            _quesToAnsRepository= quesToAnsRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(db);
        }

        [HttpPost]
        public async Task<IActionResult> MyIndex()
        {
            int questionId = Int32.Parse(Request.Form["questionID"]);
             List<string> answersIdsStr=new List<string>();
              answersIdsStr = Request.Form["div-checkbox-values"].ToList();
            List<int> answersIds = new List<int>();
        foreach(string IdStr in answersIdsStr)
                answersIds.Add(Int32.Parse(IdStr));
            AnswerEntity ans = db.Answers.Include(u => u.ThemeEntity)  // подгружаем данные по компаниям
                    .ToList()[0];
           // AnswerEntity ans= db.Answers.Find(1);

           Console.WriteLine(ans.ThemeEntity.Id);
           // await _questionRepository.AddAnswersForQuestion(questionId, answersIds); //в этом не хвносится тема

           await _quesToAnsRepository.AddAnswersForQuestion(questionId, answersIds);//
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
