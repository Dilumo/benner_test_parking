using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Models;

public class HomeController : Controller
{
    private readonly ParkingContext _context;

    public HomeController(ParkingContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new IndexViewModel
        {
            VehicleRegistration = new VehicleRegistration(),
            VehicleRegistrations = await _context.VehicleRegistrations.ToListAsync()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEntry([Bind("LicensePlate")] VehicleRegistration vehicleRegistration)
    {
        if (ModelState.IsValid)
        {
            vehicleRegistration.Enter = DateTime.Now;
            _context.Add(vehicleRegistration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        var model = new IndexViewModel
        {
            VehicleRegistration = vehicleRegistration,
            VehicleRegistrations = await _context.VehicleRegistrations.ToListAsync()
        };
        return View("Index", model);
    }
}
