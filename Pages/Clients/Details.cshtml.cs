using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoService.Data;
using AutoService.Models;

namespace AutoService.Pages_Clients
{
    public class DetailsModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public DetailsModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Client Client { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FirstOrDefaultAsync(m => m.Id == id);

            if (client is not null)
            {
                Client = client;

                return Page();
            }

            return NotFound();
        }
    }
}
