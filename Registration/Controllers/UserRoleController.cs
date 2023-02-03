using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Registration.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager= roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            IdentityRole role = new IdentityRole();
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                ViewBag.mgs = "This role is aldeady exist";
                //ViewBag.name = name;
                return View();
            }
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Role has been saved successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
