using AutoService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Pages_ServiceRecords
{
    public class IndexModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(AutoService.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<ServiceRecord> ServiceRecords { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var isClient = User.IsInRole("Client");

            if (!isClient)
            {
                ServiceRecords = await _context.ServiceRecords
                    .Include(sr => sr.Car)
                    .ThenInclude(c => c.Client)
                    .Include(sr => sr.ServiceRecordSpareParts)
                    .ThenInclude(srsp => srsp.SparePart)
                    .ToListAsync();
            }
            else if (userId != null)
            {
                ServiceRecords = await _context.ServiceRecords
                    .Include(sr => sr.Car)
                    .ThenInclude(c => c.Client)
                    .Include(sr => sr.ServiceRecordSpareParts)
                    .ThenInclude(srsp => srsp.SparePart)
                    .Where(sr => sr.Car.ClientId.ToString() == userId)
                    .ToListAsync();
            }
            else
            {
                ServiceRecords = new List<ServiceRecord>();
            }
        }
    }
}
