using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Registration.Data;
using Registration.Models;

namespace Registration.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CompanyController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Companies.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _db.Companies.Add(company);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = _db.Companies.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
        //POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _db.Update(company);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Product type has been updated";
                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }
    }
}
