using AutoService.Data;
using AutoService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ApplicationDbContext _context;

        public CreateModel(UserManager<IdentityUser> userManager, IUserStore<IdentityUser> userStore, ApplicationDbContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _context = context;
        }

        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Phone { get; set; }
        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string UserRole { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, Email, CancellationToken.None);

            var result = await _userManager.CreateAsync(user, "123456_Test");

            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                await _userManager.AddToRoleAsync(user, UserRole);

                if (UserRole == "Client")
                {
                    _context.Clients.Add(new Models.Client { Email = Email, Name = Name, UserId = user.Id });
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("ManageRoles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor.");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
