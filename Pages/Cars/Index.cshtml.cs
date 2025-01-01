using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Pages_Cars
{
    public class IndexModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public IndexModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Car> Cars { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Cars.Include(c => c.Client).AsQueryable();

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                query = query.Where(c => c.Make.Contains(SearchQuery) ||
                                         c.Model.Contains(SearchQuery) ||
                                         c.RegistrationNumber.Contains(SearchQuery));
            }

            Cars = await query.ToListAsync();
        }
    }
}
