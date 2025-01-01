using AutoService.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Pages.Admin
{
    public class AssignRoleModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public AssignRoleModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public string UserId { get; set; }
        public List<IdentityRole> Roles { get; set; } = new List<IdentityRole>();

        public void OnGet(string userId)
        {
            UserId = userId;
            Roles = _roleManager.Roles.ToList();
        }

        public async Task<IActionResult> OnPostAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                return BadRequest("Роль не існує.");
            }

            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Не вдалося додати роль.");
            }

            return RedirectToPage();
        }
    }
}
