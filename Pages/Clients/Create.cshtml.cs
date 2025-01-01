using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoService.Data;
using AutoService.Models;
using Microsoft.AspNetCore.Identity;

namespace AutoService.Pages_Clients
{
    public class CreateModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        public CreateModel(UserManager<IdentityUser> userManager, IUserStore<IdentityUser> userStore, ApplicationDbContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Client Client { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, Client.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, Client.Email, CancellationToken.None);
            
            var result = await _userManager.CreateAsync(user, "123456_Test");
            

            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                await _userManager.AddToRoleAsync(user, "Client");

                _context.Clients.Add(new Models.Client { Email = Client.Email, Name = Client.Name, UserId = user.Id});
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
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
