using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Parking.Controllers
{
    public class PriceTablesController : Controller
    {
        private readonly ParkingContext _context;

        public PriceTablesController(ParkingContext context)
        {
            _context = context;
        }

        // GET: PriceTables
        public async Task<IActionResult> Index()
        {
            var model = new PriceTableViewModel
            {
                NewPrice = new PriceTable(),
                PriceTables = await _context.PriceTables.ToListAsync()
            };
            return View(model);
        }

        // POST: PriceTables/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("StartSurveillance,EndSurveillance,ItialHourlyRate,FinalHourlyRate")] PriceTable priceTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priceTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var model = new PriceTableViewModel
            {
                NewPrice = priceTable,
                PriceTables = await _context.PriceTables.ToListAsync()
            };
            return View("Index", model);
        }
    }
}
