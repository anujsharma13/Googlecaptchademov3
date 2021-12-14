using captchademov3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace captchademov3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Student student = new Student();
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Result(Student s)
        {
            //var _googlerecaptcha=GoogleCaptchaService.verifyrecaptcha()
            GoogleCaptchaService googleCaptchaService = new GoogleCaptchaService();
            var _googlecaptcha = await googleCaptchaService.verifycaptcha(s.token);
            if(_googlecaptcha)
            {
                Console.WriteLine("yes");
            }
            else
            {
                Console.WriteLine("no");
            }
            return View("Result", s);
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
