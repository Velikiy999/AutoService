using AutoService.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoService.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
         
        public DeleteModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public string UserId { get; set; }
        public string UserEmail {  get; set; }


        async public Task<IActionResult> OnGet(string userId)
        {
            UserId = userId;

            var user = await _userManager.FindByIdAsync(userId);

            UserEmail = user.Email;

            return Page(); 
        }

        public async Task<IActionResult> OnPost(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Не вдалося видалити користувача.");
            }
            var client = _context.Clients.FirstOrDefault(_ => _.Email == user.Email);

            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("ManageRoles");
        }
    }
}
