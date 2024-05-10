using Microsoft.AspNetCore.Mvc;
using System;
using PhiKapInternal.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.Xml;
using PhiKapInternal.Models.PKT_Internal;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace PhiKapInternal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly PktInternalContext _context;

        public HomeController(ILogger<HomeController> logger, PktInternalContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //if (HttpContext.Session.GetString("IsLoggedIn") == "Yes")
            //{
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login");
            //}
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ViewProfile()
        {
            string name = HttpContext.Session.GetString("User");
            var user = _context.Members.Where(x => x.Name == name).FirstOrDefault();

            return RedirectToAction("Details", "Members", new { id = user.Id });
        }

        [HttpPost]
        public async Task<ActionResult> Login(string name, string password)
        {
            var db_member = await _context.Members.Where(x => x.Name == name).FirstOrDefaultAsync();
            if (db_member != null && db_member.Password == password)
            {
                HttpContext.Session.SetString("IsLoggedIn", "Yes");
                HttpContext.Session.SetString("User", name);
                return RedirectToAction("Index");
            } else
            {
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChapterHistory()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NationalHistory()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("IsLoggedIn", "No");
            HttpContext.Session.SetString("User", "");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}