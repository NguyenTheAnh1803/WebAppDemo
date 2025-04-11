using Demo2.Data;
using Demo2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo2.Controllers
{
    public class HelloController : Controller
    {
        private readonly AppDbContext _context;

        public HelloController(AppDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách người dùng
        public IActionResult Index()
        {
            var users = _context.UserInfos.ToList();
            return View(users);
        }

        // =================== CREATE =====================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserInfo user)
        {
            if (ModelState.IsValid)
            {
                _context.UserInfos.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // =================== EDIT =====================
        public IActionResult Edit(int id)
        {
            var user = _context.UserInfos.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserInfo user)
        {
            if (id != user.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // =================== DELETE =====================
        public IActionResult Delete(int id)
        {
            var user = _context.UserInfos.Find(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.UserInfos.FindAsync(id);
            if (user != null)
            {
                _context.UserInfos.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        //// GET: Hello/Search
        public IActionResult Search()
        {
            //var users = string.IsNullOrEmpty(keyword)
            //    ? _context.UserInfos.ToList()
            //    : _context.UserInfos
            //        .Where(u => u.Name != null && u.Name.Contains(keyword))
            //        .ToList();

            //ViewBag.Keyword = keyword;
            //return View(users);
            return View();
        }
        //search Ajax
        [HttpGet]
        public IActionResult SearchAjax(string keyword)
        {
            var result = _context.UserInfos
                .Where(u => u.Name.Contains(keyword))
                .ToList();

            return Json(result); // Trả về JSON
        }


        //public IActionResult Search()
        //{
        //    return View();
        //}


    }
}
