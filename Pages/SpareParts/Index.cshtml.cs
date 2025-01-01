using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoService.Data;
using AutoService.Models;

namespace AutoService.Pages_SpareParts
{
    public class IndexModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public IndexModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SparePart> SparePart { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SparePart = await _context.SpareParts.ToListAsync();
        }
    }
}
