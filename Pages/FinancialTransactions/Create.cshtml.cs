using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoService.Pages_FinancialTransactionss
{
    public class CreateModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public CreateModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FinancialTransactions FinancialTransactions { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Clients = await _context.Clients.ToListAsync();
            return Page();
        }

        public List<Client> Clients { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Clients = await _context.Clients.ToListAsync();
                return Page();
            }

            _context.FinancialTransactions.Add(FinancialTransactions);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
