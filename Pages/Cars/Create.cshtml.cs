using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Pages_Cars
{
    public class CreateModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public CreateModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Clients = _context.Clients.ToList();
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; }

        public List<Client> Clients { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Clients = _context.Clients.ToList();
                return Page();
            }

            _context.Cars.Add(Car);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
