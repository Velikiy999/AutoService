using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoService.Data;
using AutoService.Models;

namespace AutoService.Pages_FinancialTransactionss
{
    public class DetailsModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public DetailsModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public FinancialTransactions FinancialTransactions { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var FinancialTransactions = await _context.FinancialTransactions.FirstOrDefaultAsync(m => m.Id == id);

            if (FinancialTransactions is not null)
            {
                FinancialTransactions = FinancialTransactions;

                return Page();
            }

            return NotFound();
        }
    }
}
