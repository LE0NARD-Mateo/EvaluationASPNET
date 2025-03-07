using System.Diagnostics;
using EvaluationASPNET.Data;
using EvaluationASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluationASPNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly EvaluationASPNETContext _context;

        public HomeController(EvaluationASPNETContext context)
        {
            _context = context;
        }
        private string SessionUserName = "SessionUserName";
        private string SessionUserId = "SessionUserId";
        private string SessionRole = "SessionRole";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Inscription()
        {
            return View(new Home());
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inscription([Bind("Id,Email,Password,Role")] Home home)
        {
            if (ModelState.IsValid)
            {
                home.Password = BCrypt.Net.BCrypt.HashPassword(home.Password);
                home.Role = "Client";

                _context.Add(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(home);
        }

        public IActionResult Login()
        {
            return View(new Home());
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,Email,Password")] Home home)
        {
            if (ModelState.IsValid)
            {

                if (home.Email == null || home.Password == null)
                {
                    return NotFound();
                }

                var homeBdd = await _context.Home
                                   .FirstOrDefaultAsync(u => u.Email == home.Email);

                if (homeBdd == null)
                {
                    return NotFound();
                }

                if (!BCrypt.Net.BCrypt.Verify(home.Password, homeBdd.Password))
                {
                    return View(home);
                }

                HttpContext.Session.SetString(SessionUserName, homeBdd.Email);
                HttpContext.Session.SetString(SessionRole, homeBdd.Role);
                return RedirectToAction(nameof(Index));
            }
            return View(home);
        }

        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Redirect to the Login page
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
