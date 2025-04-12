using Demo2.Data;
using Demo2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
namespace Demo2.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Account account)
        {
            if(ModelState.IsValid)
            {
                account.Password = HashPassword(account.Password);
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(account);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username,string password)
        {
            string hash = HashPassword(password);
            var user = _context.Accounts.FirstOrDefault(x => x.UserName == username && x.Password == hash);
                if(user != null)
            {
                // Login successful
                HttpContext.Session.SetString("UserName", user.UserName);
                TempData["LoginSuccess"] = "Đăng nhập thành công!";

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Sai tài khoản hoặc mật khẩu.";

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }
}
