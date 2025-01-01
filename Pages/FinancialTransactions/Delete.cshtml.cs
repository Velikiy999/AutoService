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
    public class DeleteModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public DeleteModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var FinancialTransactions = await _context.FinancialTransactions.FindAsync(id);
            if (FinancialTransactions != null)
            {
                FinancialTransactions = FinancialTransactions;
                _context.FinancialTransactions.Remove(FinancialTransactions);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
