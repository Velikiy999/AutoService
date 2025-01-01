using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoService.Pages_FinancialReports
{
    public class IndexModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public IndexModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<FinancialTransactions> FinancialTransactions { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }

        public async Task OnGetAsync()
        {
            StartDate = DateTime.Now.AddMonths(-1);
            EndDate = DateTime.Now;
            await GetFinancialReportData();
        }

        public async Task OnPostAsync()
        {
            await GetFinancialReportData();
        }

        private async Task GetFinancialReportData()
        {
            FinancialTransactions = await _context.FinancialTransactions
                .Where(ft => ft.Date >= StartDate && ft.Date <= EndDate)
                .Include(ft => ft.Client)
                .ToListAsync();

            TotalIncome = FinancialTransactions
                .Where(ft => ft.Type == "Оплата")
                .Sum(ft => ft.Amount);

            TotalExpenses = FinancialTransactions
                .Where(ft => ft.Type == "Виставлений рахунок")
                .Sum(ft => ft.Amount);
        }
    }
}
