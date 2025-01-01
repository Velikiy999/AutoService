using AutoService.Data;
using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoService.Pages_ServiceRecords
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ServiceRecord ServiceRecord { get; set; }

        [BindProperty]
        public Dictionary<int, int>? SparePartQuantities { get; set; }

        public List<Car> Cars { get; set; }
        public List<SparePart> SpareParts { get; set; }

        public IActionResult OnGet()
        {
            SpareParts = _context.SpareParts.Where(x => x.Quantity > 0).ToList();
            Cars = _context.Cars.Include(c => c.Client).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Cars = _context.Cars.Include(c => c.Client).ToList();
                SpareParts = _context.SpareParts.Where(x => x.Quantity > 0).ToList();
                return Page();
            }

            _context.ServiceRecords.Add(ServiceRecord);
            await _context.SaveChangesAsync();

            var reminderForClient = new Reminder
            {
                ClientId = _context.Cars.Include(_ => _.Client).FirstOrDefault(_ => _.Id == ServiceRecord.CarId).ClientId,
                ReminderDate = ServiceRecord.ServiceDate,
                Message = $"Запланований технічний огляд автомобіля {ServiceRecord.Car.Make} {ServiceRecord.Car.Model} на {ServiceRecord.ServiceDate.ToString("yyyy-MM-dd")}. Не забудьте звернутися до нас для обслуговування."
            };
            _context.Reminders.Add(reminderForClient);

            if (SparePartQuantities != null && SparePartQuantities.Any())
            {
                foreach (var partId in SparePartQuantities.Keys)
                {
                    var sparePart = await _context.SpareParts.FindAsync(partId);
                    if (sparePart != null && SparePartQuantities[partId] > 0)
                    {
                        _context.ServiceRecordSpareParts.Add(new ServiceRecordSparePart
                        {
                            ServiceRecordId = ServiceRecord.Id,
                            SparePartId = sparePart.Id,
                            Quantity = SparePartQuantities[partId]
                        });

                        sparePart.Quantity -= SparePartQuantities[partId];

                        if (sparePart.Quantity == 0)
                        {
                            var reminderForAdmin = new Reminder
                            {
                                ClientId = null,
                                ReminderDate = DateTime.Now.AddDays(1),
                                Message = $"Запасна частина {sparePart.Name} для обслуговування автомобілів закінчилась. Потрібно замовити нові запчастини."
                            };
                            _context.Reminders.Add(reminderForAdmin);
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }

}
