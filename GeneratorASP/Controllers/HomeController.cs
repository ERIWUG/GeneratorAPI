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
        private readonly AnswerRepository _answerRepository;
        private readonly QuesToAnsRepository _quesToAnsRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, QuestionRepository questionRepository, QuesToAnsRepository quesToAnsRepository, AnswerRepository answerRepository)
        {
            _questionRepository = questionRepository;
            _quesToAnsRepository= quesToAnsRepository;
            _answerRepository = answerRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task <RedirectResult> MyIndex()
        {
            int questionId = Int32.Parse(Request.Form["questionID"]);
           List<AnswerEntity>allAnswers= _answerRepository.Get();
            List<int>allAnswersId= new List<int>();
            foreach (AnswerEntity answer in allAnswers) { 
                allAnswersId.Add(answer.Id);
            }
             List<string> answersIdsStr=new List<string>();
              answersIdsStr = Request.Form["div-checkbox-values"].ToList();
            List<int> answersIds = new List<int>();
        foreach(string IdStr in answersIdsStr) 
                answersIds.Add(Int32.Parse(IdStr));
            List<int> answersToDel = allAnswersId.Except(answersIds).ToList();
              await _quesToAnsRepository.DelAnswersForQuestion(questionId, answersToDel);
             await _quesToAnsRepository.AddAnswersForQuestion(questionId, answersIds);
            return Redirect("/Home/QTAindex");
    
        }

        public IActionResult QTAindex()
        {
            return View(db);
        }

        public IActionResult QTIindex()
        {
            return View(db);
        }




        public IActionResult About()
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
