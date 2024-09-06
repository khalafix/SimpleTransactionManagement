using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.DataModel;
using MyProject.Models;
using System.Diagnostics;
using System.Security.Policy;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _db;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, ApplicationContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            //// بررسی نقش کاربری
            //if (!_db.UserRoles.Any(x => x.User!.UserName == "admin" && x.Role!.Name == "admin")) { 
            //    var user = _db.Users.FirstOrDefault(x=>x.UserName == "admin");
            //    var role = _db.Roles.FirstOrDefault(x=>x.Name == "admin");

            //    _db.UserRoles.Add(new UserRole { UserId = user!.Id, RoleId = role!.Id });
            //    _db.SaveChanges();
            //}
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
