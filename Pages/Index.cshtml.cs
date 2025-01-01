using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public IndexModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Client> Clients { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                query = query.Where(c => c.Name.Contains(SearchQuery) || c.Email.Contains(SearchQuery));
            }

            Clients = await query.ToListAsync();
        }
    }
}
