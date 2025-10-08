using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using _1121726Final.Models;
using _1121726Final.Data;
using Microsoft.EntityFrameworkCore;
namespace _1121726Final.Controllers
{
    public class PartialViewController : Controller
    {

        private readonly CmsContext _context;

        public PartialViewController(CmsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Owner") == null)
            {
                TempData["message"] = "Please Login";
                return RedirectToAction("Login1", "PartialView");
            }
            var group = await _context.TableGroups1121726.Include(g => g.Concert).ToListAsync();
            return View(group);
        }
        public IActionResult Login1()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login1(string Owner)
        {
            if (Owner == null)
            {
                TempData["message"] = "Please enter account";
                return RedirectToAction("Login1", "PartialView");
            }
            var users = await (from p in _context.TableTickets1121726
                               where p.Owner == Owner
                               orderby p.PurchaseDate
                               select p).ToListAsync();

            if (users.Count != 0)
            {
                HttpContext.Session.SetString("Owner", Owner);
                TempData["message"] = "Logged in!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Login failed!";
                return RedirectToAction("Login1", "PartialView");
            }
        }



    }
}

