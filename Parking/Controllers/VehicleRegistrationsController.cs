using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parking.Data;
using Parking.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Controllers
{
    public class VehicleRegistrationsController : Controller
    {
        private readonly ParkingContext _context;
        private readonly ILogger<VehicleRegistrationsController> _logger;

        public VehicleRegistrationsController(ParkingContext context, ILogger<VehicleRegistrationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: VehicleRegistrations/Create
        public IActionResult Create()
        {
            return View();
        }


        // GET: VehicleRegistrations
        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                VehicleRegistration = new VehicleRegistration(),
                VehicleRegistrations = await _context.VehicleRegistrations.ToListAsync()
            };

            return View(viewModel);
        }

        // POST: VehicleRegistrations/CreateEntry
        [HttpPost]
        public async Task<IActionResult> CreateEntry([Bind("LicensePlate")] VehicleRegistration vehicleRegistration)
        {
            if (ModelState.IsValid)
            {
                vehicleRegistration.Enter = DateTime.Now;
                _context.Add(vehicleRegistration);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }

		// POST: VehicleRegistrations/CreateExit
		[HttpPost]
		public async Task<IActionResult> CreateExit([Bind("LicensePlate")] VehicleRegistration vehicleRegistration)
		{
			var vehicle = await _context.VehicleRegistrations
							 .Where(v => v.LicensePlate == vehicleRegistration.LicensePlate && v.Exit == null)
							 .FirstOrDefaultAsync();

			if (vehicle == null)
			{
				ModelState.AddModelError(string.Empty, "Veículo não encontrado ou já saiu.");
				return await ReturnIndexViewWithModel();
			}
            var timeExit = DateTime.Now;
			var amountToPay = CalculateAmountToPay(vehicle, timeExit);

			if (amountToPay == -1)
			{
				ModelState.AddModelError(string.Empty, "Tabela de preços não encontrada para a data de entrada do veículo. Por favor, cadastre um período de preço.");
				return await ReturnIndexViewWithModel();
			}

			vehicle.Exit = DateTime.Now;
			vehicle.AmountCharged = amountToPay;

			_context.Update(vehicle);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		private async Task<IActionResult> ReturnIndexViewWithModel()
		{
			var model = new IndexViewModel
			{
				VehicleRegistration = new VehicleRegistration(),
				VehicleRegistrations = await _context.VehicleRegistrations.ToListAsync(),
			};

			return View("~/Views/Home/Index.cshtml", model);
		}

        private decimal CalculateAmountToPay(VehicleRegistration vehicle, DateTime exit)
        {
            var duration = exit - vehicle.Enter;
            var totalMinutes = (int)duration.TotalMinutes;

            var applicablePriceTable = _context.PriceTables
                .Where(pt => pt.StartSurveillance <= vehicle.Enter && pt.EndSurveillance >= vehicle.Enter)
                .FirstOrDefault();

            if (applicablePriceTable == null)
                return -1;

            decimal amountToPay = 0m;

            if (totalMinutes <= 30)
            {
                amountToPay = applicablePriceTable.ItialHourlyRate / 2;
            }
            else
            {
                int totalHours = (totalMinutes - 30) / 60;
                int remainingMinutes = (totalMinutes - 30) % 60;

                amountToPay = applicablePriceTable.ItialHourlyRate + totalHours * applicablePriceTable.FinalHourlyRate;

                if (remainingMinutes > 10)
                {
                    amountToPay += applicablePriceTable.FinalHourlyRate;
                }
            }

            return amountToPay;
        }
    }
}
