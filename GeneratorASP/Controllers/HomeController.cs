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
            ViewBag.Db = new AppDbContext();
            var q = db.Questions.AsNoTracking().Where(c => c.Id == 1).Include(c=>c.Answers).Include(c => c.IdSet).ThenInclude(c => c.IdGroup).FirstAsync();
            ViewBag.IdGroup = q.Result.IdSet.IdGroup.Id;
            return View(q.Result);
        }

        public IActionResult QTIindex()
        {
            ViewBag.Db = new AppDbContext();
            var q = db.Questions.AsNoTracking().Where(c => c.Id == 1).Include(c=>c.IdSet).ThenInclude(c=>c.IdGroup).FirstAsync();
            ViewBag.IdGroup = q.Result.IdSet.IdGroup.Id;
            return View(q.Result);
        }


        public IActionResult QuestionIndex()
        {
            ViewBag.Db = new AppDbContext();
            /* int pageSize = 5; 
             IEnumerable<QuestionEntity> questions = await _questionRepository.GetWithTheme();//await _questionRepository.GetByPage(page, pageSize);// await _questionRepository.GetByPage(page, pageSize);
             PageViewModel pageInfo = new PageViewModel (questions.Count(), page, pageSize);
             IEnumerable<QuestionEntity> questions1 = await _questionRepository.GetByPage(page, pageSize);
             QuestionIndexViewModel ivm = new QuestionIndexViewModel { PageInfo = pageInfo, Questions = questions1 };
             return View(ivm);*/
            return View(db);
            //return Redirect("/Home/QuestionIndex");
        }

        [HttpPost]
        public async Task<IActionResult> EditQuestion()
        {
            int questionId = Int32.Parse(Request.Form["questionID"]);
            String text = Request.Form["questionText"];
            List<string> flags = new List<string>();
            flags = Request.Form["div-checkbox-values"].ToList();
            Console.WriteLine(flags[0]);
            await _questionRepository.Edit(questionId, text, flags);
            return Redirect("/Home/QuestionIndex");
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion()
        {
            int themeId = Int32.Parse(Request.Form["themeId"]);
            Console.WriteLine(themeId);
            String text = Request.Form["questionText"];
            await _questionRepository.Add(text, themeId);
            return Redirect("/Home/QuestionIndex");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteQuestion(int questionID)
        {
            Console.WriteLine("============" + questionID + "===============");
            await _questionRepository.Delete(questionID);
            return Redirect("/Home/QuestionIndex");
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
