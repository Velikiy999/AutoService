using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoService.Data;
using AutoService.Models;

namespace AutoService.Pages_FinancialTransactionss
{
    public class EditModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public EditModel(AutoService.Data.ApplicationDbContext context)
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

            var FinancialTransactions =  await _context.FinancialTransactions.FirstOrDefaultAsync(m => m.Id == id);
            if (FinancialTransactions == null)
            {
                return NotFound();
            }
            FinancialTransactions = FinancialTransactions;
           ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FinancialTransactions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancialTransactionsExists(FinancialTransactions.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FinancialTransactionsExists(int id)
        {
            return _context.FinancialTransactions.Any(e => e.Id == id);
        }
    }
}
